// <copyright file="Mechanism.cs" company="ABB">
// Copyright (c) ABB. All rights reserved.
// </copyright>
namespace RK
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityGLTF;
    using UnityEngine;
    using System.Xml.Linq;
    using System;
    using RobotStudio.API.Internal;
    using ABB.Robotics.Math;

    public enum JointType
    {
        /// <summary>
        /// Prismatic (linear) joint type
        /// </summary>
        Prismatic,

        /// <summary>
        /// Revolute (rotational) joint type
        /// </summary>
        Revolute
    }

    public class Mechanism : MonoBehaviour
    {
        [SerializeField]
        private ForwardKinematicSolver forwardKinematicSolver = null;

        /// <summary>
        /// List of transforms of mechanism links
        /// </summary>
        /// <typeparam name="Transform">Transform object</typeparam>
        /// <returns>List of Transform objects</returns>
        [SerializeField]
        private List<Transform> links = new List<Transform>();

        [SerializeField]
        private List<Transform> joints = new List<Transform>();

        public double[] jointValues;

        public JointType[] jointTypes;

        public double[] minLimits;

        public double[] maxLimits;

        private Coroutine buildMechanismCoroutine = null;

        private GameObject axisPrefab = null;
        private GameObject highlightQuadPrefab = null;
        private GameObject highlightQuad = null;

        /// <summary>
        /// Determines if model rotation was added to compensate coordinate systems change
        /// </summary>
        private bool modelRotationAdjusted = false;

        public int JointsCount
        {
            get
            {
                return this.joints.Count;
            }
        }

        /// <summary>
        /// Starts building mechanism
        /// </summary>
        /// <param name="kinematicsData">Forward kinematics XML data</param>
        /// <param name="jointValues">Joint values data</param>
        /// <param name="axisPrefab">Joint values data</param>
        /// <param name="highlightQuadPrefab">Joint values data</param>
        public void BuildMechanismWithData(string kinematicsData, float[] jointValues, GameObject axisPrefab = null, GameObject highlightQuadPrefab = null)
        {
            if (this.buildMechanismCoroutine == null)
            {
                this.axisPrefab = axisPrefab;
                this.highlightQuadPrefab = highlightQuadPrefab;

                this.buildMechanismCoroutine =
                    this.StartCoroutine(this.BuildMechanism(kinematicsData, jointValues));
            }
        }

        public int GetJointIndex(string linkName)
        {
            var jointInfos = this.forwardKinematicSolver.GetJointInfos();

            var linkIndex = -1;
            var linksCount = this.links.Count;

            for (int i = 0; i < linksCount; i++)
            {
                var link = this.links[i];

                if (string.Compare(linkName, link.name, true) == 0)
                {
                    if (i <= jointInfos.Length)
                    {
                        linkIndex = i;
                    }
                }
            }

#if UNITY_EDITOR
            Debug.Log($"[Mechanism] GetJointIndex: {linkName} => linkIndex={linkIndex}");
#endif

            if (linkIndex > -1)
            {
                for (int i = 0; i < jointInfos.Length; i++)
                {
                    var jointInfo = jointInfos[i];

                    if (jointInfo.DHParameters[0].Link == linkIndex && i < this.jointValues.Length)
                    {
#if UNITY_EDITOR
                        Debug.Log($"[Mechanism] GetJointIndex: jointIndex = {i}");
#endif
                        return i;
                    }
                }
            }

            return -1;
        }

        public Transform GetFirstJoint()
        {
            if (this.joints.Count >= 2)
            {
                return this.joints[0];
            }

            return null;
        }

        public MechanismLink GetMechanismActiveLink(int index)
        {
            var jointInfos = this.forwardKinematicSolver.GetJointInfos();
            var activeLinkNumber = jointInfos[index].DHParameters[0].Link;

            if (activeLinkNumber > 0 && activeLinkNumber <= this.joints.Count)
            {
                var link = this.joints[activeLinkNumber - 1];
                return link.GetComponentInParent<MechanismLink>();
            }

            return null;
        }

        public void CallCalculateTransforms()
        {
            this.StartCoroutine(this.CalculateTransforms());
        }

        /// <summary>
        /// Shows outline on mechanism link element
        /// </summary>
        /// <param name="mechanismLink">Mechanism link object</param>
        public void ShowOutline(MechanismLink mechanismLink)
        {
            if (mechanismLink == null)
            {
                return;
            }

            mechanismLink.SetOutline();
        }

        /// <summary>
        /// Hides all outlines in mechanism 
        /// </summary>
        public void HideOutlines()
        {
            if (this.links == null || this.links.Count == 0)
            {
                return;
            }

            foreach (var link in this.links)
            {
                MechanismLink mechanismLink = link.GetComponent<MechanismLink>();

                if (mechanismLink != null)
                {
                    mechanismLink.HideOutline();
                }
            }
        }

        public void ShowHideHighlight(bool show)
        {
            this.highlightQuad?.SetActive(show);
        }

        private IEnumerator BuildMechanism(string kinematicsData, float[] jointValues)
        {
            yield return this.StartCoroutine(this.BuildForwardKinematicSolver(kinematicsData, jointValues));
            yield return this.StartCoroutine(this.CollectLinks());
            yield return this.StartCoroutine(this.BuildJoints());

            yield return null;
        }

        private IEnumerator BuildForwardKinematicSolver(string kinematicsData, float[] jointValues)
        {
            XElement element = null;

            try
            {
                element = XElement.Parse(kinematicsData);
            }
            catch (Exception ex)
            {
#if UNITY_EDITOR
                Debug.Log($"Exception thrown when parsing kinematcs data: {ex.Message}");
#endif
                yield break;
            }

            try
            {
                this.forwardKinematicSolver = ForwardKinematicSolver.Deserialize(element);
            }
            catch (Exception ex)
            {
#if UNITY_EDITOR
                Debug.Log($"Exception thrown when deserializing forward kinematic solver: {ex.Message}");
#endif
                yield break;
            }

            this.jointValues = new double[this.forwardKinematicSolver.NumActiveJoints];
            this.jointTypes = new JointType[this.forwardKinematicSolver.NumActiveJoints];

            if (jointValues != null && jointValues.Length == this.jointValues.Length)
            {
                for (int i = 0; i < jointValues.Length; i++)
                {
                    this.jointValues[i] = jointValues[i];
                }
            }

            this.minLimits = new double[this.forwardKinematicSolver.NumActiveJoints];
            this.maxLimits = new double[this.forwardKinematicSolver.NumActiveJoints];

            this.forwardKinematicSolver.GetJointLimits(
                true,
                this.jointValues,
                null,
                null,
                out this.minLimits,
                out this.maxLimits);

            yield return null;

            for (int i = 0; i < jointValues.Length; i++)
            {
                var ji = this.forwardKinematicSolver.GetJointInfos()[i];
                this.jointTypes[i] = ji.IsRevoluteJoint ? JointType.Revolute : JointType.Prismatic;
            }

            yield return null;
        }

        private IEnumerator CollectLinks()
        {
            this.links.Clear();

            var childCount = this.transform.childCount;
            var fillColor = new Color(44.0f / 255.0f, 32.0f / 255.0f, 212.0f / 255.0f, 50.0f / 255.0f);

            for (int i = 0; i < childCount; i++)
            {
                Transform child = this.transform.GetChild(i);
                child.gameObject.AddComponent<MechanismLink>();

                this.links.Add(child);

                // add colliders ???
                var renderers = child.GetComponentsInChildren<Renderer>();

                foreach (var renderer in renderers)
                {
                    renderer.gameObject.AddComponent<MeshCollider>();
                    yield return null;
                }

                yield return null;
            }

            // instantiate hightlight quad
            if (this.highlightQuadPrefab != null && this.links.Count > 0)
            {
                // first link
                this.highlightQuad =
                    GameObject.Instantiate(this.highlightQuadPrefab, this.links[0], false);

                this.highlightQuad.transform.localRotation =
                    UnityEngine.Quaternion.Euler(0.0f, 180.0f, 180.0f);

                var scale = 1.0f; ;

                var renderer = this.links[0].GetComponentInChildren<MeshRenderer>();
                if (renderer != null)
                {
                    var bounds = renderer.bounds;
                    scale = Mathf.Max(
                        bounds.extents.x * 3.0f,
                        bounds.extents.y * 3.0f) / this.links[0].lossyScale.x;
                }

                this.highlightQuad.transform.localScale =
                    new UnityEngine.Vector3(
                        scale,
                        scale,
                        1.0f);

                this.highlightQuad.SetActive(false);
            }

            yield return null;
        }

        private IEnumerator BuildJoints()
        {
            this.joints.Clear();

            var linksCount = this.links.Count;

            for (int i = 1; i < linksCount; i++)
            {
                var linkPosition = this.links[i];

                var joint = new GameObject($"J{i + 1}");
                joint.transform.parent = linkPosition;
                joint.transform.SetPositionAndRotation(linkPosition.position, linkPosition.rotation);

                if (this.axisPrefab != null)
                {
                    var axisGameObject = GameObject.Instantiate(this.axisPrefab, joint.transform, false);
                    axisGameObject.name = $"J{i + 1} Axis";
                }

                this.joints.Add(joint.transform);
            }

            yield return null;
        }

        private IEnumerator CalculateTransforms()
        {
            // #if UNITY_EDITOR
            //             Debug.Log($"[Mechanism] CalculateTransforms: mechanism name {this.name}");
            // #endif

            Matrix4[] m = new Matrix4[this.forwardKinematicSolver.NumLinks];

            this.forwardKinematicSolver.ComputeLinkTransforms(this.jointValues, m);

            yield return null;

            // recalculate joint limits to not allow "impossible" poses of robot
            this.forwardKinematicSolver.GetJointLimits(
                false,
                this.jointValues,
                null,
                null,
                out this.minLimits,
                out this.maxLimits);

            // adjust local rotation of parent gameobject to keep object position in new coordinate system
            if (this.modelRotationAdjusted == false)
            {
                this.transform.localRotation *=
                    UnityEngine.Quaternion.Euler(270.0f, 180.0f, 0.0f);
                this.modelRotationAdjusted = true;
            }

            for (int i = 0; i < this.links.Count; i++)
            {
                Matrix4 m4 = m[i];
                Transform link = this.links[i].transform;

                link.localPosition = Matrix4toUnityPos(m4);
                link.localRotation = Matrix4toUnityRot(m4);
            }

            yield return null;
        }

        private UnityEngine.Vector3 Matrix4toUnityPos(Matrix4 m4)
        {
            //swap Y and Z coordinates
            return new UnityEngine.Vector3(
                (float)m4.Translation.x,
                (float)m4.Translation.z,
                (float)m4.Translation.y);
        }
        private UnityEngine.Quaternion Matrix4toUnityRot(Matrix4 m4)
        {
            //To orient MODEL correctly in UNITY : local rotation X -90 deg and Y 180
            //Note : Euler angle order supposed to be Z Y X order but X Y Z order only works..
            var mRot = new Matrix4(
                ABB.Robotics.Math.Vector3.ZeroVector,
                new ABB.Robotics.Math.Vector3(-Math.PI / 2, Math.PI, 0));
            var mOut = m4.Multiply(mRot);

            //ABB.Quaternion.Q1 <=> UnityEngine.Quaternion.W
            //ABB.Quaternion.Q2 <=> UnityEngine.Quaternion.X
            //ABB.Quaternion.Q3 <=> UnityEngine.Quaternion.Y
            //ABB.Quaternion.Q4 <=> UnityEngine.Quaternion.Z

            //Swap Y and Z and negate X,Y,Z
            return new UnityEngine.Quaternion(
                (float)-mOut.Quaternion.q2,
                (float)-mOut.Quaternion.q4,
                (float)-mOut.Quaternion.q3,
                (float)mOut.Quaternion.q1);
        }
    }
}
