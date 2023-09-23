using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Sockets;
using System.Text;
using System.IO;
using UnityEngine;
using RK;
using TMPro;
using UnityGLTF;
using Microsoft.MixedReality.Toolkit.Experimental.UI;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;
using Microsoft.MixedReality.Toolkit.UI;
using Vuforia;


public class MainMenu : MonoBehaviour
{
    //Definition of Variables

    //Keyboard for inputs
#if WINDOWS_UWP
    MixedRealityKeyboard keyboard; 
#endif

    private GameObject Robot;       //Robot GameObject
    private RobotMechanismBuilder RobotMechanism;   //Script to Control the robot
    private int JointNumber;
    public GameObject WorkObject;   //WorkObject GameObject
    public GameObject Station;      //GameObject that Includes the whole station

    
    
    //TOOLS

    public GameObject ToolFolder;   
    public Transform[] Tools;       
    private int Tool_iterator = 0;  
    private GripperMovement GripperController;      
    public TMP_Text ToolUriText;
    public GLTFComponent ToolCAD;
    public GameObject ToolPreview;
    public GLTFComponent ToolPreviewCAD;
    private GameObject ToolPreviewTCP;
    private bool PreviewFirstTime=true;

    //COMMUNICATION

    public string IPAdressVirtual;
    private const int LocalPortVirtual = 7000;
    private TcpClient ClientVirtual;
    private NetworkStream StreamVirtual;
    public string Message;
    public string MessageReceivedVirtual;

    private TcpClient ClientVirtualJoints;
    private NetworkStream StreamVirtualJoints;
    public string JointsReceivedVirtual;

    public string IPAdressReal;
    private const int LocalPortReal = 8000;
    private TcpClient ClientReal;
    private NetworkStream StreamReal;
    public string MessageReceivedReal;

    private TcpClient ClientRealOp;
    private NetworkStream StreamRealOp;
    public string MessageReceivedRealOp;

    //TARGETS

    private const int MaxTargets = 60;
    private List<GameObject> TargetList = new List<GameObject>();
    public GameObject ErrorDisplay;
    public TargetManager TargetManage;
    public List<GameObject> PathList = new List<GameObject>();
    public GameObject SelectedPath;
    public int PathIterator = 0;
    public TMP_Text PathNumber;
    [SerializeField]
    private GameObject PathPauseToggleButton;
    public bool ProcessExecuting = false;
    [SerializeField]
    private GameObject PreviewPath;
    private double TimePreview;
    private bool PreviewPathStatus = false;
    [SerializeField]
    private GameObject PreviewPointPrefab;


    //ROBOT

    private bool UpdateRobot = false;
    public bool UpdateWorkObject = false;
    public bool MoveReal = false;
    public bool StopSim = false;
    public bool PauseSim = false;
    public bool Testing = false;
    private bool RealRobotMoving = false;
    public GLTFComponent RobotCAD;
    public bool OnlyVirtual;
    public bool NextPathAcknowledged = false;
    //6 sliders
    private PinchSlider[] Sliders;
    [SerializeField]
    private GameObject JogMenu;
    public bool JogMode = false;
    public bool JogModeEnd = false;


    //QR
    [SerializeField]
    private QR_Reader QRManager;
    public GameObject Plane;

    //MENUS
    [SerializeField]
    private DirectionalIndicator Indicator;
    [SerializeField]
    public GameObject AdviceRobotMenu;

    //VUFORIA

    public GameObject FoundPiece;
    [SerializeField]
    private GameObject PickPlaceButtons;
    private bool UpdateTetrisButtons;

    //GUIDANCE

    private PhotoCaptureEx Camera;
    [SerializeField]
    private GameObject GuidanceDisplay;
    [SerializeField]
    private GameObject QuadEditor;
    [SerializeField]
    private GameObject QuadDisplay;
    private Texture Image;
    private bool GuidanceTextModifying = false;
    [SerializeField]
    private TextMeshPro GuidanceTextEditor;
    [SerializeField]
    private TextMeshPro GuidanceTextDisplay;
    [SerializeField]
    private GameObject CountDown;
    private TextMeshPro CountDownText;
    [SerializeField]
    private GameObject MainCamera;




    void Start()            //Initialization of main variables
    {
        CountDownText = CountDown.GetComponent<TextMeshPro>();
        Camera = this.GetComponent<PhotoCaptureEx>();

        Robot = GameObject.Find("Robot");       //Robot GameObject
        RobotMechanism = Robot.GetComponent<RobotMechanismBuilder>();       //Script to control the robot

        Plane.SetActive(false);     //Hide the plane of the station


        Tools = new Transform[ToolFolder.transform.childCount];     //Get all tools in array
        for (int i = 0; i < ToolFolder.transform.childCount; i++)
        {

            Tools[i] = ToolFolder.transform.GetChild(i);

        }


        ToolCAD = Tools[0].GetComponent<GLTFComponent>();           //Get the GLTF Importer Script for tool change
        ToolPreviewCAD = ToolPreview.GetComponent<GLTFComponent>(); //Get the GLTF Importer Script of the preview tool
        RobotCAD = Robot.GetComponent<GLTFComponent>();             //Get the GLTF Importer Script for the robot

        GameObject[] PathArray = GameObject.FindGameObjectsWithTag("Path");     //Get all existing paths in the list and hide them
        foreach (GameObject Path in PathArray)
        {
            PathList.Add(Path);
            Path.SetActive(false);
        }

        SelectedPath = PathList[0];     //Show the first path and update numbers in the target menu
        SelectedPath.SetActive(true);
        UpdatePathNumber();

        WorkObject.SetActive(false);    //Hide the WorkObject until QR is found

        Sliders = new PinchSlider[6];   //Get the Sliders of the Jog menu

        for (int i = 0; i <= 5; i++)
        {
            Sliders[i] = JogMenu.transform.Find("J" + (i + 1).ToString() + " Selector").GetComponent<PinchSlider>();
        }

        AdviceRobotMenu.gameObject.SetActive(false);


        //Definition of the keyboard and when it does appear
#if WINDOWS_UWP
        keyboard = gameObject.AddComponent<MixedRealityKeyboard>();
        keyboard.DisableUIInteractionWhenTyping = true;
        keyboard.OnHideKeyboard.AddListener(delegate { LoadToolCAD(); });
#endif



    }

    void Update()
    {
        //If the virtual robot is moving
        if (UpdateRobot)
        {
            if (StopSim)    //Procedure for cancelling the current movement
            {

                if (JointsReceivedVirtual != "Stopped")
                {
                    SendMessageVirtualOp("Stop");
                    UpdateRobot = false;
                    StopSim = false;
                    ClientVirtualJoints.Close();
                }
            }
            else if (PauseSim)  //Procedure for pausing the current movement
            {
                if (JointsReceivedVirtual != "Paused")
                {
                    SendMessageVirtualOp("Pause");
                    UpdateRobot = false;
                    PauseSim = false;
                }
            }
            else
            {   

                //If the movement is for the trajectory preview and 100 ms since the last point placed
                if ((TimePreview + 0.1f < Time.timeAsDouble) && PreviewPathStatus)
                {
                    TimePreview = Time.timeAsDouble;    //Save this time
                    SendMessageVirtualOp("Pos");
                    
                    string[] Values = JointsReceivedVirtual.Split('|'); //Get all the position and rotation of the TCP
                    Pose PreviewLocation;
                    PreviewLocation.position.x = float.Parse(Values[0], CultureInfo.InvariantCulture);
                    PreviewLocation.position.y = float.Parse(Values[1], CultureInfo.InvariantCulture);
                    PreviewLocation.position.z = float.Parse(Values[2], CultureInfo.InvariantCulture);
                    PreviewLocation.rotation.w = float.Parse(Values[3], CultureInfo.InvariantCulture);
                    PreviewLocation.rotation.x = float.Parse(Values[4], CultureInfo.InvariantCulture);
                    PreviewLocation.rotation.y = float.Parse(Values[5], CultureInfo.InvariantCulture);
                    PreviewLocation.rotation.z = float.Parse(Values[6], CultureInfo.InvariantCulture);

                    GameObject Prefab = Instantiate(PreviewPointPrefab, PreviewPath.transform); //Place and instantiate the preview point
                    Prefab.transform.SetLocalPositionAndRotation(new Vector3((PreviewLocation.position.y / 1000f), (PreviewLocation.position.z / 1000f), -(PreviewLocation.position.x / 1000f)),
                        new Quaternion(PreviewLocation.rotation.y, PreviewLocation.rotation.z, -PreviewLocation.rotation.x, -PreviewLocation.rotation.w));
                }
                else
                {
                    SendMessageVirtualOp("Update");     //Get the current joint values of the robot
                    if (JointsReceivedVirtual != "Finished")    //If movement has not finished update the holograms joints and gripper
                    {
                        string[] Values = JointsReceivedVirtual.Split('|');
                        int i;
                        JointNumber = Robot.transform.GetChild(0).transform.GetChild(0).GetComponent<Mechanism>().jointValues.Length;
                        for (i = 0; i < JointNumber; i++)
                        {
                            RobotMechanism.UpdateMechJoints(i, float.Parse(Values[i], CultureInfo.InvariantCulture) * Mathf.PI / 180f);
                        }
                        GripperController = Tools[Tool_iterator].GetComponent<GripperMovement>();
                        if ((float.Parse(Values[JointNumber], CultureInfo.InvariantCulture) == 0f) && GripperController.Open && !GripperController.Moving)
                        {
                            GripperController.ToggleGripper();
                        }
                        else if ((float.Parse(Values[JointNumber], CultureInfo.InvariantCulture) == 1f) && GripperController.Close && !GripperController.Moving)
                        {
                            GripperController.ToggleGripper();
                        }
                    }
                    else
                    {
                        //Movement has finished
                        if (JogMode && !JogModeEnd)     //If user keeps moving the sliders send a new command
                        {
                            string Str = "JJ|";
                            int Joint = -1;
                            for (int i = 0; i < 6; i++)
                            {
                                if (Sliders[i].SliderValue != 0.5f)
                                {
                                    Joint = i;
                                    
                                }
                            }

                            if (Joint != -1)
                            {
                                if (Sliders[Joint].SliderValue < 0.5f)
                                {
                                    Str = Str + "-" + (Joint+1).ToString() + "|";
                                }
                                else
                                {
                                    Str = Str + "+" + (Joint + 1).ToString() + "|";
                                }

                                Str += ((Mathf.Abs(Sliders[Joint].SliderValue-0.5f)*250)).ToString("F4", CultureInfo.InvariantCulture);
                            }
                            else
                            {
                                Str += "00|10";
                            }
                            Str = Str.Replace(',', '.');
                            SendMessageVirtualOp(Str);


                        }
                        else
                        {
                            //Normal Procedure after finishing
                            UpdateRobot = false;
                            PreviewPathStatus = false;
                            


                            if (ProcessExecuting)   //If the robot has to go through all the paths
                            {
                                bool Pausing = false;
                                PathConfig Config = SelectedPath.GetComponent<PathConfig>();
                                if (Config.PauseAfterFinish)
                                {
                                    Pausing = true;
                                }
                                    NextPath();
                                if (SelectedPath != PathList[0])
                                {

                                    SendTargets();
                                    StartSimulation();
                                    
                                    if (Pausing)
                                    {
                                        PauseSimulation();
                                    }
                                    else
                                    {
                                        UpdateGuidanceDisplay();
                                    }
                                    
                                    //Llamar a menu manager tal vez para que aparezca el aviso

                                }
                                else
                                {
                                    ProcessExecuting = false;   //Robot has finished going through all the paths
                                    SendMessageVirtualOp("CC"); //Communication can be closed
                                    ClientVirtualJoints.Close();//Close communication
                                    GuidanceDisplay.SetActive(false);
                                    //Sound for completing the process
                                }

                            }
                            else
                            {
                                

                                if (JogMode)    //If the robot has finished the movements and no more inputs are received
                                {
                                    JogModeEnd = false;
                                    JogMode = false;
                                    SendMessageVirtualOp("CC"); //Communication can be closed
                                }
                                ClientVirtualJoints.Close();    //Close communication
                            }
                        }
                        
                    }
                
                }
            }
            

            
        }
        if (UpdateWorkObject)   //WorkObject position has changed
        {
            //Modify the translation and rotation axis to match with RobotStudio
            Quaternion WorkObjectRotation = WorkObject.transform.localRotation;
            WorkObjectRotation.w = -WorkObjectRotation.w;
            WorkObjectRotation.z = -WorkObjectRotation.z;

            string str = "CW|" + (-WorkObject.transform.localPosition.z * 1000f).ToString("F2", CultureInfo.InvariantCulture) + "|" + (WorkObject.transform.localPosition.x * 1000f).ToString("F2", CultureInfo.InvariantCulture) + "|" + (WorkObject.transform.localPosition.y * 1000f).ToString("F2", CultureInfo.InvariantCulture) +
            "|" + WorkObjectRotation.w.ToString("F4", CultureInfo.InvariantCulture) + "|" + WorkObjectRotation.z.ToString("F4", CultureInfo.InvariantCulture) + "|" + WorkObjectRotation.x.ToString("F4", CultureInfo.InvariantCulture) + "|" + WorkObjectRotation.y.ToString("F4", CultureInfo.InvariantCulture);
            SendMessageVirtual(str);    //Send the message to the virtual controller

            if (!OnlyVirtual)   
            {
                SendMessageReal(str);   //Send to the Real Robot too if available
            }
            
            
            UpdateWorkObject = false;   //WorkObject Updated
        }

        
        if (RealRobotMoving) //If the real robot is moving
        {
            if (StopSim)
            {

                if (MessageReceivedReal != "Stopped")       //Procedure to stop the robot
                {
                    SendMessageRealOp("Stop");
                    RealRobotMoving = false;
                    ClientRealOp.Close();
                    StopSim = false;
                }

            }
            else if (PauseSim)                              //Procedure to pause the robot
            {
                if (MessageReceivedRealOp != "Paused") 
                {
                    SendMessageRealOp("Pause");
                    RealRobotMoving = false;
                    PauseSim = false;
                }
            }
            else
            {

                SendMessageRealOp("Update");    //Get information about the movement
                if (MessageReceivedRealOp == "Finished")        // Movement has finished
                {
                    if (JogMode && !JogModeEnd)     //If user keeps moving the sliders send a new command
                    {
                        string Str = "JJ|";
                        int Joint = -1;
                        for (int i = 0; i < 6; i++)
                        {
                            if (Sliders[i].SliderValue != 0.5f)
                            {
                                Joint = i;

                            }
                        }

                        if (Joint != -1)
                        {
                            if (Sliders[Joint].SliderValue < 0.5f)
                            {
                                Str = Str + "-" + (Joint + 1).ToString() + "|";
                            }
                            else
                            {
                                Str = Str + "+" + (Joint + 1).ToString() + "|";
                            }

                            Str += ((Mathf.Abs(Sliders[Joint].SliderValue - 0.5f) * 250)).ToString("F4", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            Str += "00|10";
                        }
                        Str = Str.Replace(',', '.');

                        SendMessageRealOp(Str);     //Send command


                    }
                    else
                    {
                        //Normal procedure after the movement has finished
                        RealRobotMoving = false;
                        if (ProcessExecuting)   //If the robot has to go through all the paths
                        {
                            bool Pausing = false;
                            PathConfig Config = SelectedPath.GetComponent<PathConfig>();
                            if (Config.PauseAfterFinish)
                            {
                                Pausing = true;
                            }
                            NextPath();
                            if (SelectedPath != PathList[0])
                            {
                                SendTargets();
                                StartSimulation();
                                if (Pausing)
                                {
                                    PauseSimulation();
                                }
                                else
                                {
                                    UpdateGuidanceDisplay();
                                }
                                
                            }
                            else
                            {
                                ProcessExecuting = false;   //All paths have been completed
                                SendMessageRealOp("CC");    //Communications can be closed
                                ClientRealOp.Close();       //Close Communocations
                                GuidanceDisplay.SetActive(false);
                                //Finishing Sound
                            }

                        }
                        else
                        {
                            if (JogMode)                    //If there are no more jog mode inputs
                            {
                                JogModeEnd = false;
                                JogMode = false;
                                SendMessageRealOp("CC");    //Communications can be closed
                            }
                            ClientRealOp.Close();           //Close Communocations
                        }
                    }

                }
            }
        }

        if (NextPathAcknowledged)
        {
            ResumeSimulation();
            NextPathAcknowledged = false;
        }

        if (UpdateTetrisButtons)
        {
            Vector3 Location = FoundPiece.transform.position;
            Location.y += 0.1f;
            PickPlaceButtons.transform.position = Location;
        }

        //Update the text of the space while typing
#if WINDOWS_UWP
        if (keyboard.Visible)
        {
            if(GuidanceTextModifying){
                GuidanceTextEditor.text = keyboard.Text;
                PathConfig Config = SelectedPath.GetComponent<PathConfig>();
                Config.text = GuidanceTextEditor.text;
            }else{
                ToolUriText.text = keyboard.Text;
            }
        }
#endif
        if (TargetManage.SelectedTarget != null && TargetManage.PreviewTool == true)    //If there is one target selected
        {
            //Procedure for transforming the PreviewTool
            if (PreviewFirstTime)   
            {
                ToolPreviewTCP = ToolPreview.transform.GetChild(0).GetChild(0).Find("TCP").gameObject;
                PreviewFirstTime = false;
            }
            if(ToolPreviewTCP != null)
            {
                //Update the Preview tool position and rotation
                ToolPreview.SetActive(true);
                ToolPreviewTCP.transform.SetParent(null, true);
                ToolPreview.transform.SetParent(ToolPreviewTCP.transform, true);
                ToolPreviewTCP.transform.SetPositionAndRotation(TargetManage.SelectedTarget.transform.position, TargetManage.SelectedTarget.transform.rotation);
                ToolPreviewTCP.transform.Translate(new Vector3(0f, -0.01f, 0f), Space.Self);
            }
            else
            {
                ToolPreview.SetActive(false);   
            }
        }
        else if(!PreviewFirstTime && ToolPreview != null)
        {
            ToolPreview.SetActive(false);
        }
    }
        

    async public void LoadRobotAndGripper()     //Load .glb models of the robot and gripper
    {
        WorkObject.SetActive(true);             //Make the WorkObject visible
        if (Tools[0].transform.childCount == 0)     //Avoid creating more than one copy
        {

            await
            ToolCAD.Load();
            PreviewFirstTime = true;
            ToolPreview.SetActive(true);
            await
            ToolPreviewCAD.Load();
            

        }
        if (Robot.transform.childCount == 0)    //Avoid creating more than one copy
        {
            await
            RobotCAD.Load();
        }
        GripperController = Tools[Tool_iterator].GetComponent<GripperMovement>();
        GripperController.FirstTime = true;

    }

    public void ChangeTool()
    {
        if (Tools[Tool_iterator].gameObject.TryGetComponent(out GLTFComponent a))
        {
            RobotMechanism.ChangeTool(Tools[Tool_iterator].GetChild(0).GetChild(0), 0.11f);
        }
        else
        {
            RobotMechanism.ChangeTool(Tools[Tool_iterator], 0.1f);
        }
        
        Tool_iterator ++;
        if (Tool_iterator == ToolFolder.transform.childCount)
        {
            Tool_iterator = 0;
        }
        
    }

    public void MoveToTarget()      //Procedure to move to a target
    {
        if(TargetManage.SelectedTarget != null)     //A target must be selected
        { 

            if (MoveReal)       //Open corresponding coms taking into account the selected robot
            {
                ClientRealOp = new TcpClient();
                ClientRealOp.Client.ReceiveTimeout = 10000;
                ClientRealOp.Connect(IPAdressReal, LocalPortReal);
                StreamRealOp = ClientRealOp.GetStream();
            }
            else
            {
                ClientVirtualJoints = new TcpClient();
                ClientVirtualJoints.Client.ReceiveTimeout = 10000;
                ClientVirtualJoints.Connect(IPAdressVirtual, LocalPortVirtual);
                StreamVirtualJoints = ClientVirtualJoints.GetStream();
            }


            string Str = Target2Str(TargetManage.SelectedTarget);   //Get the string with the target information
            

            if (MoveReal)       //Send the information to the controller SE PODRÍA CAMBIAR Y MEJORAR
            {
                SendMessageRealOp("MT");
                SendMessageRealOp(Str);
                RealRobotMoving = true;
            }
            else
            {
                SendMessageVirtualOp("MT");
                SendMessageVirtualOp(Str);
                UpdateRobot = true;
            }

        }

    }

    public void SendTargets()       //Procedure to send all the target information to both controllers
    {
        if (!ProcessExecuting)      //Communications are open if the robot has to run all the paths
        {
            ClientVirtualJoints = new TcpClient();
            ClientVirtualJoints.Client.ReceiveTimeout = 10000;
            ClientVirtualJoints.Connect(IPAdressVirtual, LocalPortVirtual);
            StreamVirtualJoints = ClientVirtualJoints.GetStream();

            if (!OnlyVirtual)
            {
                ClientRealOp = new TcpClient();
                ClientRealOp.Client.ReceiveTimeout = 10000;
                ClientRealOp.Connect(IPAdressReal, LocalPortReal);
                StreamRealOp = ClientRealOp.GetStream();
            }
        }
        
    
        
        

        TargetList.Clear();     //Clear the target list
        GameObject[] TargetArray = GameObject.FindGameObjectsWithTag("Target");     //Get all the visible targets

        foreach(GameObject Target in TargetArray)       //Add the targets to the list if they active
        {
            TargetConfig Configuration = Target.GetComponent<TargetConfig>();
            if(Configuration.Order > 0)
            {
                TargetList.Add(Target);
            }

        }

        TargetList.Sort(CompareTargets);    //Sort the list taking into account the order set

        if(!(ProcessExecuting && MoveReal))     //If the process is being executed just send it to the running controller otherwise to both of them
        {
            SendMessageVirtualOp("ST");
            SendMessageVirtualOp(TargetList.Count.ToString());
        }
        

        if (!OnlyVirtual && (ProcessExecuting && MoveReal || !ProcessExecuting))
        {
            SendMessageRealOp("ST");
            SendMessageRealOp(TargetList.Count.ToString());
        }
        
        

        for (int i=0;i< TargetList.Count; i++)
        {
            if(!(ProcessExecuting && MoveReal))
            {
                SendMessageVirtualOp(Target2Str(TargetList[i]));
            }
            
            if (!OnlyVirtual && (ProcessExecuting && MoveReal || !ProcessExecuting))
            {
                SendMessageRealOp(Target2Str(TargetList[i]));
            }
            
        }
       

        if(JointsReceivedVirtual == "Error")    //If one of the targets is not reachable
        {
            ErrorDisplay.SetActive(true);
            //Position
        }
        if (!OnlyVirtual)           //Finish communications if the the process is not being executed
        {
            if (!ProcessExecuting)
            {
                ClientRealOp.Close();
            }
        }
        if (!ProcessExecuting)
        {
            ClientVirtualJoints.Close();
        }
    }

    public void StartSimulation()       //Run the path that is loaded in the controllers
    {

        foreach (Transform child in PreviewPath.transform)      //Delete the preview path points
        {
            GameObject.Destroy(child.gameObject);
        }
        if (MoveReal)   //Open communications and send command of the corresponding robot if the process is not being executed
        {
            if (!ProcessExecuting)
            {
                ClientRealOp = new TcpClient();
                ClientRealOp.Client.ReceiveTimeout = 10000;
                ClientRealOp.Connect(IPAdressReal, LocalPortReal);
                StreamRealOp = ClientRealOp.GetStream();
            }
            

            SendMessageRealOp("SS");

            RealRobotMoving = true;
        }
        else
        {
            if (!ProcessExecuting)
            {
                ClientVirtualJoints = new TcpClient();
                ClientVirtualJoints.Client.ReceiveTimeout = 10000;
                ClientVirtualJoints.Connect(IPAdressVirtual, LocalPortVirtual);
                StreamVirtualJoints = ClientVirtualJoints.GetStream();
            }
            SendMessageVirtualOp("SS");

            UpdateRobot = true;
        }

        
        
    }

    public void PreviewSim()        //Procedure to process the path and show a preview
    {

        foreach (Transform child in PreviewPath.transform)      //Delete the previous preview path points
        {
            GameObject.Destroy(child.gameObject);
        }

        if(!MoveReal)       //If the real robot is not selected
        {
            if (!ProcessExecuting)
            {
                ClientVirtualJoints = new TcpClient();
                ClientVirtualJoints.Client.ReceiveTimeout = 10000;
                ClientVirtualJoints.Connect(IPAdressVirtual, LocalPortVirtual);
                StreamVirtualJoints = ClientVirtualJoints.GetStream();
            }
            SendMessageVirtualOp("SP");     //Start preview

            UpdateRobot = true;
            PreviewPathStatus = true;
        }

        TimePreview = Time.timeAsDouble;    //Save time to place the first point

    }

    public void PauseSimulation()       //Triggers the procedure to pause the movement
    {
        PauseSim = true;

    }

    public void ResumeSimulation()      //Triggers the procedure to resume the movement
    {
        if (MoveReal)
        {
            RealRobotMoving = true;
        }
        else
        {
            UpdateRobot = true;
        }
        UpdateGuidanceDisplay();

    }

    public void GoHome()        //Procedure to make the corresponding robot go to Home Position
    {
        if (MoveReal)
        {
            ClientRealOp = new TcpClient();
            ClientRealOp.Client.ReceiveTimeout = 10000;
            ClientRealOp.Connect(IPAdressReal, LocalPortReal);
            StreamRealOp = ClientRealOp.GetStream();

            SendMessageRealOp("GH");

            RealRobotMoving = true;
        }
        else
        {
            ClientVirtualJoints = new TcpClient();
            ClientVirtualJoints.Client.ReceiveTimeout = 10000;
            ClientVirtualJoints.Connect(IPAdressVirtual, LocalPortVirtual);
            StreamVirtualJoints = ClientVirtualJoints.GetStream();

            SendMessageVirtualOp("GH");

            UpdateRobot = true;
        }

        
    }

    private string Target2Str(GameObject TargetObject)  //Procedure to transform the target information to a string taking into account the axis transformations
    {
        string Str;
        TargetConfig Config = TargetObject.GetComponent<TargetConfig>();
        Vector3 Coordinates = TargetObject.transform.localPosition;
        Quaternion TargetOrientation = TargetObject.transform.localRotation;
        TargetOrientation.w = -TargetOrientation.w;
        TargetOrientation.z = -TargetOrientation.z;

        Str = Config.MovementConfig + "|" + (-Coordinates.z * 1000f).ToString("F2") + "|" + (Coordinates.x * 1000f).ToString("F2") + "|" + (Coordinates.y * 1000f).ToString("F2") +
            "|" + TargetOrientation.normalized.w.ToString("F3") + "|" + TargetOrientation.normalized.z.ToString("F3") + "|" + TargetOrientation.normalized.x.ToString("F3") + "|" + TargetOrientation.normalized.y.ToString("F3") +
            "|" + Config.SpeedConfig + "|" + Config.ZoneConfig + "|" + (Config.GripperOpened ? "1" : "0");


        Str = Str.Replace(',', '.');        


        return Str;
    }


    public void PlaceStation()      //Triggers the procedure to search a robot QR
    {
        if (!QRManager.TrackerActive)
        {
            QRManager.ToggleQRTracker();
        }
        QRManager.FindRobotQR();
    }

    public void PlaceWorkObject()   //Triggers the procedure to search a WorkObject QR
    {
        if (!QRManager.TrackerActive)
        {
            QRManager.ToggleQRTracker();
        }
        QRManager.FindWorkObjectQR();
    }

    public void CloseQRSearch()     //Stop the QR tracker
    {
        if (QRManager.TrackerActive)
        {
            QRManager.ToggleQRTracker();
        }
    }

    public void StartProcess()      //Procedure to run all the paths loaded
    {
        if (MoveReal)   //Open the communications to the corresponding robot
        {
            ClientRealOp = new TcpClient();
            ClientRealOp.Client.ReceiveTimeout = 10000;
            ClientRealOp.Connect(IPAdressReal, LocalPortReal);
            StreamRealOp = ClientRealOp.GetStream();
        }
        else
        {
            ClientVirtualJoints = new TcpClient();
            ClientVirtualJoints.Client.ReceiveTimeout = 10000;
            ClientVirtualJoints.Connect(IPAdressVirtual, LocalPortVirtual);
            StreamVirtualJoints = ClientVirtualJoints.GetStream();
        }

        ProcessExecuting = true;    //Set the mode to running all the paths
        SelectedPath.SetActive(false);      //Visualize the first path
        PathIterator = 0;

        SelectedPath = PathList[PathIterator];
        SelectedPath.SetActive(true);
        UpdatePathNumber();

        GuidanceDisplay.SetActive(true);
        UpdateGuidanceDisplay();

        if (MoveReal)   //Do not close the communications in robotstudio after completing a task
        {
            SendMessageRealOp("CO");
        }
        else
        {
            SendMessageVirtualOp("CO");
        }

        SendTargets();      //Send the targets of the active path
        StartSimulation();  //Start moving

    }
    private void SendMessageVirtual(string str)     //TCP Communication with the virtual robot for a single message 
    {
        
        ClientVirtual = new TcpClient();        //Open Communication
        ClientVirtual.Client.ReceiveTimeout = 10000;
        ClientVirtual.Connect(IPAdressVirtual, LocalPortVirtual);

        StreamVirtual = ClientVirtual.GetStream();

        byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(str);     //Convert from string to bytes
        StreamVirtual.Write(bytesToSend, 0, bytesToSend.Length);    //Send the message

        byte[] bytesToRead = new byte[ClientVirtual.ReceiveBufferSize];     //Read the buffer
        int bytesRead = StreamVirtual.Read(bytesToRead, 0, ClientVirtual.ReceiveBufferSize);
        MessageReceivedVirtual = Encoding.ASCII.GetString(bytesToRead, 0, bytesRead);   //Convert from bytes to string

        ClientVirtual.Close();      //Close Communication
 

    }

    private void SendMessageVirtualOp(string str)       //TCP Communication for the exchange of several messages
    {

        byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(str);         //Convert from string to bytes
        StreamVirtualJoints.Write(bytesToSend, 0, bytesToSend.Length);  //Send the message


        byte[] bytesToRead = new byte[ClientVirtualJoints.ReceiveBufferSize];           //Read the buffer
        int bytesRead = StreamVirtualJoints.Read(bytesToRead, 0, ClientVirtualJoints.ReceiveBufferSize);
        JointsReceivedVirtual = Encoding.ASCII.GetString(bytesToRead, 0, bytesRead);    //Convert from bytes to string


    }

    private void SendMessageReal(string str)    //TCP Communication with the real robot for a single message 
    {
        ClientReal = new TcpClient();       //Open Communication
        ClientReal.Client.ReceiveTimeout = 10000;
        ClientReal.Connect(IPAdressReal, LocalPortReal);
        StreamReal = ClientReal.GetStream();

        byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(str);     //Convert from string to bytes
        StreamReal.Write(bytesToSend, 0, bytesToSend.Length);       //Send the message

        byte[] bytesToRead = new byte[ClientReal.ReceiveBufferSize];                //Read the buffer
        int bytesRead = StreamReal.Read(bytesToRead, 0, ClientReal.ReceiveBufferSize);      
        MessageReceivedReal = Encoding.ASCII.GetString(bytesToRead, 0, bytesRead);  //Convert from bytes to string

        ClientReal.Close();     //Close Communication
    }

    private void SendMessageRealOp(string str)      //TCP Communication for the exchange of several messages
    {
        
        byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(str); //Convert from string to bytes
        StreamRealOp.Write(bytesToSend, 0, bytesToSend.Length); //Send the message

        byte[] bytesToRead = new byte[ClientRealOp.ReceiveBufferSize];              //Read the buffer
        int bytesRead = StreamRealOp.Read(bytesToRead, 0, ClientRealOp.ReceiveBufferSize);
        MessageReceivedRealOp = Encoding.ASCII.GetString(bytesToRead, 0, bytesRead);//Convert from bytes to string


    }
    private static int CompareTargets(GameObject x, GameObject y)   //Function to order the targets in the path list
    {
        TargetConfig xConfig, yConfig;
        xConfig = x.GetComponent<TargetConfig>();
        yConfig = y.GetComponent<TargetConfig>();
        return xConfig.Order.CompareTo(yConfig.Order);
    }

    public void ToggleRealRobot()   //Procedure to change between the virtual and real robot
    {
        
        if (MoveReal)
        {   
            Robot.SetActive(true);      //Show the robot and tools
            ToolFolder.SetActive(true);
        }
        else
        {
            Robot.SetActive(false);     //Hide the robot and tools
            ToolFolder.SetActive(false);
        }
        MoveReal = !MoveReal;           //Change state
    }

    public void StopSimulation()    //Triggers the procedure to cancel the movement
    {
        StopSim = true;
        ProcessExecuting = false;
    }
    
    public void ChangeToolUri()     //Delete the existing tools and show the keyboard to modify the URI
    {
        GuidanceTextModifying = false;
#if WINDOWS_UWP
        keyboard.ShowKeyboard(ToolUriText.text,false);
#endif
        if (Tools[0].transform.childCount != 0)
        {
            Destroy(Tools[0].transform.GetChild(0).gameObject);
            Destroy(ToolPreview.transform.GetChild(0).gameObject);
        }

    }
    public void LoadToolCAD()   //Load the .glb models of the tool
    {
        
        if (Testing)
        {
            ToolCAD.GLTFUri = @"C:\Users\bpeir\Borja\TFG\Prueba A\" + ToolUriText.text;
            ToolPreviewCAD.GLTFUri = ToolCAD.GLTFUri;
        }
        else
        {
            ToolCAD.GLTFUri = @"C:\Data\Users\assar_hololens_1@outlook.com\3D Objects\" + ToolUriText.text;
            ToolPreviewCAD.GLTFUri = ToolCAD.GLTFUri;
        }
        
        LoadRobotAndGripper();
        

        
        
    }
    public void PlaceToolOnRobot()  //Set the new tool as the end effector of the robot
    {
        RobotMechanism.ChangeTool(Tools[0].transform.GetChild(0).GetChild(0), 0.09f);
    }

    public void NextPath()  
    {
        SelectedPath.SetActive(false);  //Hide the current path
        PathIterator = PathIterator + 1;    //Make the selected path the following one
        if(PathIterator == PathList.Count)
        {
            PathIterator = 0;
        }
        SelectedPath = PathList[PathIterator];
        SelectedPath.SetActive(true);       //Show the selected path
        UpdatePathNumber();     //Update the menu values
        UpdatePausePath();
        UpdatePathDisplay();
    }

    public void PreviousPath()      
    {
        SelectedPath.SetActive(false);  //Hide the current path
        PathIterator = PathIterator - 1;    //Make the selected path the previous one
        if (PathIterator == -1)
        {
            PathIterator = PathList.Count-1;
        }
        SelectedPath = PathList[PathIterator];
        SelectedPath.SetActive(true);   //Show the selected path
        UpdatePathNumber();     //Update the menu values
        UpdatePausePath();
        UpdatePathDisplay();
    }

    public void UpdatePathNumber()      //Procedure to update the menu values after changing the selected path
    {
        if(PathList.Count != 0)
        {
            PathNumber.text = "Current Path: " + (PathIterator + 1).ToString();
        }
        else
        {
            PathNumber.text = "Create a Path Please";
        }
        
    }

    public void AddPathFirst()      //Create a new path that will be placed in the first place
    {
        if (SelectedPath != null)
        {
            SelectedPath.SetActive(false);
        }
        GameObject NewPath = new GameObject("Path");    //Initialization of the path values
        NewPath.AddComponent<PathConfig>();
        NewPath.transform.SetParent(WorkObject.transform);
        NewPath.tag = "Path";
        NewPath.transform.SetLocalPositionAndRotation(new Vector3(0f, 0f, 0f), Quaternion.identity);
        PathList.Insert(0, NewPath);    //Add the new path to the path list
        PathIterator = 0;           //Select the new path
        SelectedPath = NewPath;
        UpdatePathNumber();         //Update the menu values
        UpdatePausePath();
        UpdatePathDisplay();
    }

    public void AddPathLast()       //Create a new path that will be placed in the last place
    {
        if(SelectedPath != null)
        {
            SelectedPath.SetActive(false);
        }
        GameObject NewPath = new GameObject("Path");        //Initialization of the path values
        NewPath.AddComponent<PathConfig>();
        NewPath.transform.SetParent(WorkObject.transform);
        NewPath.tag = "Path";
        NewPath.transform.SetLocalPositionAndRotation(new Vector3(0f, 0f, 0f), Quaternion.identity);
        PathList.Add(NewPath);  //Add the new path to the path list
        SelectedPath = NewPath;     //Select the new path
        PathIterator = PathList.Count-1;
        UpdatePathNumber();     //Update the menu values
        UpdatePausePath();
        UpdatePathDisplay();
    }

    public void DeleteSelectedPath()    //Procedure to delete the selected path
    {
        PathList.Remove(SelectedPath);
        Destroy(SelectedPath);
        if (PathList.Count != 0)    //Make the first path to be the selected one
        {
            SelectedPath = PathList[0];
            PathIterator = 0;
            SelectedPath.SetActive(true);
            UpdatePausePath();
            UpdatePathDisplay();

        }
        UpdatePathNumber();     //Update the menu values
    }

    public void DeleteAllPaths()    //Procedure to delete all paths
    {
        for(int i = 0; i<PathList.Count; i++)
        {
            SelectedPath = PathList[i];
            Destroy(SelectedPath);
        }

        PathList.Clear();
        
        UpdatePathNumber();
        
    }

    public void ChangeIndicatorMenu(Transform Trans)    //Make the arrow point the GameObject that is sent
    {
        Indicator.gameObject.SetActive(true);
        Indicator.DirectionalTarget = Trans;
    }

    public void DeselectMenu(GameObject Menu)           //If the menu is closed hide the arrow
    {
        if(Menu.transform == Indicator.DirectionalTarget)
        {
            Indicator.gameObject.SetActive(false);
        }
    }

    public void JogJointsStart()        //Trigger for the robot jog mode
    {
        if (!JogMode)
        {
            if (MoveReal)
            {
                
                ClientRealOp = new TcpClient();     //Open the communication
                ClientRealOp.Client.ReceiveTimeout = 10000;
                ClientRealOp.Connect(IPAdressReal, LocalPortReal);
                StreamRealOp = ClientRealOp.GetStream();

                SendMessageRealOp("CO");        //Keep the communication open after finishing the command
                RealRobotMoving = true;

                string Str = "JJ|";     //Prepare the string that will be sent
                int Joint = -1;
                for (int i = 0; i < 6; i++)         //Find which slide is being moved
                {
                    if (Sliders[i].SliderValue != 0.5f)
                    {
                        Joint = i;
                    }
                }

                if (Joint != -1)    //Set the joint and direction
                {
                    if (Sliders[Joint].SliderValue < 0.5f)
                    {
                        Str = Str + "-" + (Joint + 1).ToString() + "|";
                    }
                    else
                    {
                        Str = Str + "+" + (Joint + 1).ToString() + "|";
                    }

                    Str += ((Mathf.Abs(Sliders[Joint].SliderValue - 0.5f) * 250)).ToString("F4", CultureInfo.InvariantCulture); //Set the speed of the TCP

                }
                else
                {
                    Str += "00|10";     //Complete the command for no movement if slider is grabbed but not moved
                }
                Str = Str.Replace(',', '.');

                SendMessageRealOp(Str);     //Send command
            }
            else
            {
                ClientVirtualJoints = new TcpClient();          //Open the communication
                ClientVirtualJoints.Client.ReceiveTimeout = 10000;
                ClientVirtualJoints.Connect(IPAdressVirtual, LocalPortVirtual);
                StreamVirtualJoints = ClientVirtualJoints.GetStream();

                SendMessageVirtualOp("CO");     //Keep the communication open after finishing the command
                UpdateRobot = true;

                string Str = "JJ|";         //Prepare the string that will be sent
                int Joint = -1;
                for (int i = 0; i < 6; i++)     //Find which slide is being moved
                {
                    if (Sliders[i].SliderValue != 0.5f)
                    {
                        Joint = i;
                    }
                }

                if (Joint != -1)        //Set the joint and direction
                {
                    if (Sliders[Joint].SliderValue < 0.5f)
                    {
                        Str = Str + "-" + (Joint + 1).ToString() + "|";
                    }
                    else
                    {
                        Str = Str + "+" + (Joint + 1).ToString() + "|";
                    }

                    Str += ((Mathf.Abs(Sliders[Joint].SliderValue - 0.5f) * 250)).ToString("F4", CultureInfo.InvariantCulture);     //Set the speed of the TCP

                }
                else
                {
                    Str += "00|10";      //Complete the command for no movement if slider is grabbed but not moved
                }
                Str = Str.Replace(',', '.');

                SendMessageVirtualOp(Str);  //Send command
            }
        }
        JogMode = true;
    }

    public void JogJointsStop()     //Triggers the end of the movement in jog mode when is slider is not grabbed
    {
        for (int i = 0; i < 6; i++)
        {
            Sliders[i].SliderValue = 0.5f;
        }
        JogModeEnd = true;
    }

    public void ClearTrajectory()       //Clear the preview trajectory points
    {
        foreach (Transform child in PreviewPath.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void ConfirmNextPath()       //Continue with the following path in the process
    {
        NextPathAcknowledged = true;
    }

    public void TetrisPieceFound(GameObject Piece)
    {
        UpdateTetrisButtons = true;
        FoundPiece = Piece;
        PickPlaceButtons.SetActive(true);
    }

    public void TetrisPieceLost()
    {
        UpdateTetrisButtons = false;
        FoundPiece = null;
        PickPlaceButtons.SetActive(false);
    }

    public void TogglePausePath()
    {
        PathConfig Config = SelectedPath.GetComponent<PathConfig>();
        Config.PauseAfterFinish = !Config.PauseAfterFinish;
        UpdatePausePath();

    }
    public void UpdatePausePath()
    {
        PathConfig Config = SelectedPath.GetComponent<PathConfig>();
        Interactable Inter = PathPauseToggleButton.GetComponent<Interactable>();
        ButtonConfigHelper ButtonConfig = PathPauseToggleButton.GetComponent<ButtonConfigHelper>();
        if (Config.PauseAfterFinish)
        {
            Inter.CurrentDimension = 1;
            ButtonConfig.SetQuadIconByName("IconDone");
        }
        else
        {
            Inter.CurrentDimension = 0;
            ButtonConfig.SetQuadIconByName("IconClose");
        }
    }

    public void ChangeGuidanceText()
    {
        GuidanceTextModifying = true;
#if WINDOWS_UWP
        keyboard.ShowKeyboard(GuidanceTextEditor.text,false);
#endif
    }
    public void ChangePhoto()
    {
        StartCoroutine(ChangeGuidancePhoto());  
    }

    public IEnumerator ChangeGuidancePhoto()
    {
                     
        float normalizedTime = 3f;
        CountDown.SetActive(true);
        while (normalizedTime >= 1f)
        {
            CountDownText.text = normalizedTime.ToString("F0");
            normalizedTime -= Time.deltaTime;
            yield return 0;
        }
        CountDown.SetActive(false);
        Camera.TakePhoto();
        yield return new WaitForSeconds(3f);

        Renderer quadRenderer = QuadEditor.GetComponent<Renderer>() as Renderer;
        Image = quadRenderer.material.mainTexture;
        PathConfig Config = SelectedPath.GetComponent<PathConfig>();
        Config.Image = Image;
        
    }

    public void UpdatePathDisplay()
    {
        PathConfig Config = SelectedPath.GetComponent<PathConfig>();
        GuidanceTextEditor.text = Config.text;
        Renderer quadRenderer = QuadEditor.GetComponent<Renderer>() as Renderer;
        quadRenderer.material.mainTexture = Config.Image;
    }

    public void TurnOnVuforia()
    {
        MainCamera.GetComponent<VuforiaBehaviour>().enabled = true;
        MainCamera.GetComponent<DefaultInitializationErrorHandler>().enabled = true;
        VuforiaApplication.Instance.Initialize();
        
    }

    public void TurnOffVuforia()
    {
        VuforiaApplication.Instance.Deinit();
        MainCamera.GetComponent<VuforiaBehaviour>().enabled = false;
        MainCamera.GetComponent<DefaultInitializationErrorHandler>().enabled = false;
    }

    public void UpdateGuidanceDisplay()
    {
        PathConfig Config = SelectedPath.GetComponent<PathConfig>();
        GuidanceTextDisplay.text = Config.text;
        Renderer quadRenderer = QuadDisplay.GetComponent<Renderer>() as Renderer;
        quadRenderer.material.mainTexture = Config.Image;

    }

    public void ToggleOnlyVirtual()
    {
        OnlyVirtual = !OnlyVirtual;
    }

}
