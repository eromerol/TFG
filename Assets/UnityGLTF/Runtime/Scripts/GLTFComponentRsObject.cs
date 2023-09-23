using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityGLTF.Loader;
using WrapMode = UnityEngine.WrapMode;


namespace UnityGLTF
{
    /// <summary>
    /// Component to load a GLTF scene with
    /// </summary>
    public class GLTFComponentRsObject : MonoBehaviour, IProgress<ImportProgress>, IProgress<AnimationBuildProgress>
    {
        public string GLTFUri = null;
        public bool Multithreaded = true;
        public bool UseStream = false;
        public bool AppendStreamingAssets = true;
        public bool PlayAnimationOnLoad = true;
        public ImporterFactory Factory = null;
        public static string SelectedFileURL = "";
        public static bool AnimationProcessed = false;
        public static bool Reset = false;
        public static bool LoadObject = false;
        private bool loaded = false;
        public IEnumerable<Animation> Animations { get; private set; }

        [SerializeField]
        private bool loadOnStart = true;

        [SerializeField] private bool MaterialsOnly = false;

        [SerializeField] private int RetryCount = 10;
        [SerializeField] private float RetryTimeout = 2.0f;
        private int numRetries = 0;

        private bool hasAnimationsToProcess = false;

        public void SetUrl(string SelectedFilePath)
        {
            SelectedFileURL = SelectedFilePath;
            LoadObject = false;
            loaded = false;
            UseStream = true;
        }

        public bool HasAnimationsToProcess
        {
            get
            {
                return this.hasAnimationsToProcess;
            }
        }

        public int MaximumLod = 300;
        public int Timeout = 8;
        public GLTFSceneImporter.ColliderType Collider = GLTFSceneImporter.ColliderType.None;
        public GameObject LastLoadedScene { get; private set; } = null;

        [SerializeField]
        private Shader shaderOverride = null;

        public ImportStatistics Statistics;

        /// <summary>
        /// Asset generator
        /// </summary>
        public string AssetGenerator = null;

        /// <summary>
        /// Asset version
        /// </summary>
        public string AssetVersion = null;

        /// <summary>
        /// Forward kinematics data
        /// </summary>
        private Dictionary<int, (string name, string kinematics, float[] jointValues)> kinematicsData = new Dictionary<int, (string name, string kinematics, float[] jointValues)>();

        /// <summary>
        /// SafeMove data
        /// </summary>
        private Dictionary<int, string> safeMoveData = new Dictionary<int, string>();

        #region "Events"
        event Action<float> onLoadingProgress;
        event Action<float> onAnimationProcessingProgress;
        event Action onAnimationProcessed;
        event Action<Dictionary<int, (string name, string kinematics, float[] jointValues)>> onKinematicsDataProcessed;
        event Action<Dictionary<int, string>> onSafeMoveDataProcessed;

        public event Action<float> OnLoadingProgress
        {
            add
            {
                this.onLoadingProgress += value;
            }

            remove
            {
                this.onLoadingProgress -= value;
            }
        }

        public event Action<float> OnAnimationProcessingProgress
        {
            add
            {
                this.onAnimationProcessingProgress += value;
            }

            remove
            {
                this.onAnimationProcessingProgress -= value;
            }
        }

        public event Action OnAnimationProcessed
        {
            add
            {
                this.onAnimationProcessed += value;
            }

            remove
            {
                this.onAnimationProcessed -= value;
            }
        }

        public event Action<Dictionary<int, (string name, string kinematics, float[] jointValues)>> OnKinematicsDataProcessed
        {
            add
            {
                this.onKinematicsDataProcessed += value;
            }
            remove
            {
                this.onKinematicsDataProcessed -= value;
            }
        }

        public event Action<Dictionary<int, string>> OnSafeMoveDataProcessed
        {
            add
            {
                this.onSafeMoveDataProcessed += value;
            }
            remove
            {
                this.onSafeMoveDataProcessed -= value;
            }
        }
        #endregion

        private async void Start()
        {
            if (!loadOnStart) return;

            try
            {
                await Load();
            }
#if WINDOWS_UWP
			catch (Exception ex)
#else
            catch (HttpRequestException ex)
#endif
            {
                if (numRetries++ >= RetryCount)
                    throw;

                Debug.LogError($"Load failed: {ex.Message}");
                Debug.LogWarning("Load failed, retrying");
                await Task.Delay((int)(RetryTimeout * 1000));
                Start();
            }
        }

        void Update()
        {
            //if (LoadObject)
            //{
            //    LoadObject = false;
            //    LoadOnCommand();
            //}
        }
        private GLTFSceneImporter sceneImporter = null;

        #region Added code

        public async void LoadOnCommand(string SelectedFilePath)
        {
           // if (loaded) return;
            SelectedFileURL = SelectedFilePath;
            LoadObject = false;
            loaded = false;
            UseStream = true;
            this.onAnimationProcessed -= GLTFComponentTeachTool_onAnimationProcessed;
            this.onAnimationProcessed += GLTFComponentTeachTool_onAnimationProcessed;
            AnimationProcessed = false;
            try
            {
                await Load();
                loaded = true;
            }
#if WINDOWS_UWP
			catch (Exception ex)
#else
            catch (HttpRequestException ex)
#endif
            {
                //if (numRetries++ >= RetryCount)
                //    throw;

                //Debug.LogError($"Load failed: {ex.Message}");
                //Debug.LogWarning("Load failed, retrying");
                //await Task.Delay((int)(RetryTimeout * 1000));
                //LoadOnCommand();
            }
        }

        private void GLTFComponentTeachTool_onAnimationProcessed()
        {
            this.onAnimationProcessed -= GLTFComponentTeachTool_onAnimationProcessed;
            AnimationProcessed = true;
        }

        #endregion
        public async Task Load(Action<GameObject, ExceptionDispatchInfo> onLoadComplete = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var importOptions = new ImportOptions
            {
                AsyncCoroutineHelper = gameObject.GetComponent<AsyncCoroutineHelper>() ?? gameObject.AddComponent<AsyncCoroutineHelper>(),
                ThrowOnLowMemory = false
            };

            try
            {
                Factory = Factory ?? ScriptableObject.CreateInstance<DefaultImporterFactory>();
                if (UseStream)
                {
                    string fullPath = string.Empty;
                    if (SelectedFileURL.Length > 0)
                    {
                        // added code
                        fullPath = SelectedFileURL;
                        GLTFUri = Path.GetFileName(fullPath);
                        Debug.Log("Loading: " + GLTFUri);
                    }
                    else if (AppendStreamingAssets)
                    {
                        // Path.Combine treats paths that start with the separator character
                        // as absolute paths, ignoring the first path passed in. This removes
                        // that character to properly handle a filename written with it.
                        fullPath = Path.Combine(Application.streamingAssetsPath, GLTFUri.TrimStart(new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar }));
                    }
                    else
                    {
                        fullPath = GLTFUri;
                    }
                    string directoryPath = URIHelper.GetDirectoryName(fullPath);
                    importOptions.DataLoader = new FileLoader(directoryPath);

                    this.sceneImporter = Factory.CreateSceneImporter(
                        Path.GetFileName(GLTFUri),
                        importOptions
                        );
                }
                else
                {
                    string directoryPath = URIHelper.GetDirectoryName(GLTFUri);
                    importOptions.DataLoader = new WebRequestLoader(directoryPath);

                    sceneImporter = Factory.CreateSceneImporter(
                        URIHelper.GetFileFromUri(new Uri(GLTFUri)),
                        importOptions
                        );

                }

                sceneImporter.SceneParent = gameObject.transform;
                sceneImporter.Collider = Collider;
                sceneImporter.MaximumLod = MaximumLod;
                sceneImporter.Timeout = Timeout;
                sceneImporter.IsMultithreaded = Multithreaded;
                sceneImporter.CustomShaderName = shaderOverride ? shaderOverride.name : null;

                if (MaterialsOnly)
                {
                    var mat = await sceneImporter.LoadMaterialAsync(0);
                    var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.SetParent(gameObject.transform);
                    var renderer = cube.GetComponent<Renderer>();
                    renderer.sharedMaterial = mat;
                }
                else
                {
                    await sceneImporter.LoadSceneAsync(-1, true, onLoadComplete, cancellationToken, this);
                }

                this.AssetGenerator = sceneImporter.Root?.Asset?.Generator;
                this.AssetVersion = sceneImporter.Root?.Asset?.Version;

                this.kinematicsData.Clear();
                this.safeMoveData.Clear();

                var rootNodesCount = sceneImporter.Root?.Nodes?.Count;

                for (int i = 0; i < rootNodesCount; i++)
                {
                    //var kinematicsData = sceneImporter.Root?.Nodes[i]?.Extras?.Value<string>("ABB_ro_int_kinematics");

                    //if (string.IsNullOrEmpty(kinematicsData) == false)
                    //{
                    //    var jointValuesArray = sceneImporter.Root?.Nodes[i]?.Extras?.Value<JArray>("ABB_ro_int_jointvalues");
                    //    float[] jointValues = null;

                    //    if (jointValuesArray != null && jointValuesArray.Count > 0)
                    //    {
                    //        var count = jointValuesArray.Count;
                    //        jointValues = new float[count];

                    //        for (int j = 0; j < count; j++)
                    //        {
                    //            jointValues[j] = jointValuesArray.Value<float>(j);
                    //        }
                    //    }

                    //    this.kinematicsData.Add(i, (sceneImporter.Root?.Nodes[i].Name, kinematicsData, jointValues));
                    //}

                    //var nodeTag = sceneImporter.Root?.Nodes[i]?.Extras?.Value<string>("ABB_ro_int_tag");

                    //if (string.IsNullOrEmpty(nodeTag) == false)
                    //{
                    //    if (string.Compare(nodeTag, "safemove_zone", true) == 0 ||
                    //        string.Compare(nodeTag, "safemove_geometry", true) == 0)
                    //    {
                    //        var nodeName = sceneImporter.Root?.Nodes[i].Name;

                    //        if (string.IsNullOrEmpty(nodeName) == true)
                    //        {
                    //            nodeName = $"{GLTFSceneImporter.DefaultNodeName}{i}";
                    //        }

                    //        this.safeMoveData.Add(i, nodeName);
                    //    }
                    //}
                }

            //    this.onKinematicsDataProcessed?.Invoke(this.kinematicsData);
            //    this.onSafeMoveDataProcessed?.Invoke(this.safeMoveData);

                // Override the shaders on all materials if a shader is provided
                if (shaderOverride != null)
                {
                    Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
                    foreach (Renderer renderer in renderers)
                    {
                        renderer.sharedMaterial.shader = shaderOverride;
                    }
                }

                Statistics = sceneImporter.Statistics;
                LastLoadedScene = sceneImporter.LastLoadedScene;

               // this.hasAnimationsToProcess = true;
               // this.StartCoroutine(this.ProcessAnimations());
                this.onAnimationProcessed?.Invoke();
            }
#if UNITY_EDITOR
            catch (Exception ex)
            {
                Debug.LogError($"Exception: {ex.Message}");
            }
#endif            
            finally
            {
                if (importOptions.DataLoader != null)
                {
                    importOptions.DataLoader = null;
                }
            }
        }

        private IEnumerator ProcessAnimations()
        {
            yield return new WaitForSeconds(0.1f);

            // create the AnimationClip that will contain animation data
            Animation animation = this.sceneImporter.LastLoadedScene.AddComponent<Animation>();

            yield return new WaitForSeconds(0.1f);

            if (this.sceneImporter.Root?.Animations != null && this.sceneImporter.Root?.Animations.Count > 0)
            {

                //Debug.LogFormat("Number of animations to process: {0}", _gltfRoot.Animations.Count);

                for (int i = 0; i < this.sceneImporter.Root?.Animations.Count; ++i)
                {
                    AnimationClip clip = null;

                    Action<AnimationClip> clipAction = (c) =>
                    {
                        clip = c;
                    };

                    yield return this.sceneImporter.ConstructClip(
                        this.sceneImporter.LastLoadedScene.transform, i, default(CancellationToken), clipAction, this).AsCoroutine();

                    clip.wrapMode = WrapMode.Loop;

                    //Debug.Log("### before add clip");

                    animation.AddClip(clip, clip.name);

                    //Debug.Log("### after add clip");

                    if (i == 0)
                    {
                        animation.clip = clip;
                    }
                }
            }

            yield return null;

            Animations = this.sceneImporter.LastLoadedScene.GetComponents<Animation>();

            if (PlayAnimationOnLoad && Animations.Any())
            {
                Animations.FirstOrDefault().Play();
            }

            this.hasAnimationsToProcess = false;

            this.sceneImporter?.Dispose();
            this.sceneImporter = null;

            this.onAnimationProcessed?.Invoke();

            yield return null;
        }

        void IProgress<ImportProgress>.Report(ImportProgress value)
        {
            this.onLoadingProgress?.Invoke(value.Progress);
        }

        void IProgress<AnimationBuildProgress>.Report(AnimationBuildProgress value)
        {
            this.onAnimationProcessingProgress?.Invoke(value.Progress);
        }

    }
}
