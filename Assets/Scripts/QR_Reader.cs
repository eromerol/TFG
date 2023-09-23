using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.SampleQRCodes;
using Microsoft.MixedReality.QR;
using Microsoft.MixedReality.OpenXR;
using System;
using System.Text;
using TMPro;
using RK;
using Microsoft.MixedReality.Toolkit.UI.BoundsControl;
using Microsoft.MixedReality.Toolkit.UI;
using Vuforia;




public class QR_Reader : MonoBehaviour
{
    [SerializeField]
    private QRCodesManager QrCodesManager;
    [SerializeField]
    private QRCodesVisualizer QrVisualizer;


    public MainMenu Menu;
    [SerializeField]
    private TargetManager TargetManage;
    [SerializeField]
    private GameObject TargetInterface;

    private System.Collections.Generic.IList<Microsoft.MixedReality.QR.QRCode> QrCodesList = null;
    private SpatialGraphNode node = null;
    private Microsoft.MixedReality.QR.QRCode code = null;
    public bool TrackerActive = false;

    public bool QRConfirmation = false;
    private string QRToSearch = "";
    public bool StartSearching = false;
    private bool Universal = false;

    public GameObject Button;
    public RobotMechanismBuilder RobotMechanism;
    public GameObject PathPrefab_14_1;
    public GameObject PathPrefab_14_4;
    public GameObject PathPrefab_3456_3;
    public GameObject PathPrefab_3456_4;
    public GameObject PathPrefab_3456_5;
    public GameObject Piece;

    [SerializeField]
    private GameObject MainCamera;
    private bool VuforiaDisabled = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (StartSearching)     //If the application is searching for a specific QR
        {
            
            Pose QRPose = GetQRPose(QRToSearch, Universal, out bool Found); //Get the position of the QR that meets the requirements
            if (Found)
            {
                
                Button.SetActive(true);     //Show and place the button to ask for confirmation
                PlaceButton(QRPose);
                
            }
            if (QRConfirmation)     //If the button is pressed
            {
                if (QRToSearch == "Robot")      //Firts word of the QR is "Robot"
                {
                    Menu.Plane.SetActive(true);     //Initialize the station
                    Menu.Station.transform.position = QRPose.position;
                    Menu.Station.transform.rotation = QRPose.rotation;
                    Menu.Station.transform.Rotate(new Vector3(90f, 0f, 0f), Space.Self);        //Correct the orientation of the robot model
                    Vector3 Rot = Menu.Station.transform.rotation.eulerAngles;
                    Menu.Station.transform.rotation = Quaternion.Euler(0f, Rot.y, 0f);          //Make the robot be on the table plane
                    Menu.Station.transform.Translate(new Vector3(0.17f, -0.01f, 0.017f), Space.Self);

                    string[] StrSplit = code.Data.Split("_");   //Get the second word of the QR which will be the name of the .glb file
                    if(StrSplit[1] == "CBR15000")
                    {
                        StrSplit[1] = "CRB15000";
                    }
                    Menu.RobotCAD.GLTFUri = @"C:\Data\Users\assar_hololens_1@outlook.com\3D Objects\" + StrSplit[1] + ".glb";   //Change the URI
                    if (Menu.Testing)
                    {
                        Menu.RobotCAD.GLTFUri = @"C:\Users\bpeir\Borja\TFG\Prueba A\CRB15000.glb";
                    }
                    if (Menu.Tools[0].transform.childCount != 0)
                    {
                        Destroy(Menu.Tools[0].transform.GetChild(0));
                        Destroy(Menu.ToolPreview.transform.GetChild(0));
                    }
                    Menu.ToolCAD.GLTFUri = @"C:\Data\Users\assar_hololens_1@outlook.com\3D Objects\" + Menu.ToolUriText.text;   //Change the URI of the tools
                    if (Menu.Testing)
                    {
                        Menu.ToolCAD.GLTFUri = @"C:\Users\bpeir\Borja\TFG\Prueba A\" + Menu.ToolUriText.text;
                    }
                    Menu.ToolPreviewCAD.GLTFUri = Menu.ToolCAD.GLTFUri;
                    Menu.LoadRobotAndGripper();         //Load the 3D models


                }
                else if (QRToSearch == "WorkObject")        //First word of the QR is WorkObject
                {
                    Menu.WorkObject.transform.position = QRPose.position;   //Place the WorkObject
                    Menu.WorkObject.transform.rotation = QRPose.rotation;
                    Menu.WorkObject.transform.Rotate(new Vector3(90f, 0f, 0f), Space.Self);
                    Vector3 Rot = Menu.WorkObject.transform.rotation.eulerAngles;
                    Menu.WorkObject.transform.rotation = Quaternion.Euler(0f, Rot.y, 0f);
                    
                    Menu.UpdateWorkObject = true;       //Triggers the communication with the robot controllers to update the values
                    
                    if (code.Data == "WorkObject_14")   //If QR code has an specific data load a prefabricated path
                    {
                        
                        GameObject Path = Instantiate(PathPrefab_14_4, new Vector3(0f, 0f, 0f), Quaternion.identity, Menu.WorkObject.transform);
                        Path.transform.SetLocalPositionAndRotation(new Vector3(0f, 0f, 0f), Quaternion.identity);
                        Path.SetActive(true);
                        foreach(Transform child in Path.transform)  //Configuration of each target
                        {
                            TargetConfig Config = child.GetComponent<TargetConfig>();
                            Config.TargetManager = TargetManage;
                            Config.Interface = TargetInterface;
                            BoundsControl Bounds = child.GetComponent<BoundsControl>();
                            Bounds.RotateStopped.AddListener(delegate { TargetManage.UpdateValues(); });
                            Bounds.TranslateStopped.AddListener(delegate { TargetManage.UpdateValues(); });
                            Bounds.ScaleStopped.AddListener(delegate { TargetManage.UpdateValues(); });
                            ObjectManipulator Manipulator = child.GetComponent<ObjectManipulator>();
                            Manipulator.OnManipulationEnded.AddListener(delegate { TargetManage.UpdateValues(); });
                        }

                        if (Menu.SelectedPath != null)      //Make the new path the selected one to visualize it
                        {
                            Menu.SelectedPath.SetActive(false);
                        }
                            
                        Menu.PathList.Add(Path);        //Add the path in the last position
                        Menu.SelectedPath = Path;
                        Menu.PathIterator = Menu.PathList.Count - 1;
                        Menu.UpdatePathNumber();        //Update the menu values


                        Path = Instantiate(PathPrefab_14_1, new Vector3(0f, 0f, 0f), Quaternion.identity, Menu.WorkObject.transform);
                        Path.transform.SetLocalPositionAndRotation(new Vector3(0f, 0f, 0f), Quaternion.identity);
                        Path.SetActive(true);
                        foreach (Transform child in Path.transform)  //Configuration of each target
                        {
                            TargetConfig Config = child.GetComponent<TargetConfig>();
                            Config.TargetManager = TargetManage;
                            Config.Interface = TargetInterface;
                            BoundsControl Bounds = child.GetComponent<BoundsControl>();
                            Bounds.RotateStopped.AddListener(delegate { TargetManage.UpdateValues(); });
                            Bounds.TranslateStopped.AddListener(delegate { TargetManage.UpdateValues(); });
                            Bounds.ScaleStopped.AddListener(delegate { TargetManage.UpdateValues(); });
                            ObjectManipulator Manipulator = child.GetComponent<ObjectManipulator>();
                            Manipulator.OnManipulationEnded.AddListener(delegate { TargetManage.UpdateValues(); });
                        }

                        if (Menu.SelectedPath != null)      //Make the new path the selected one to visualize it
                        {
                            Menu.SelectedPath.SetActive(false);
                        }

                        Menu.PathList.Add(Path);        //Add the path in the last position
                        Menu.SelectedPath = Path;
                        Menu.PathIterator = Menu.PathList.Count - 1;
                        Menu.UpdatePathNumber();        //Update the menu values


                    }

                    if (code.Data == "WorkObject_3456")   //If QR code has an specific data load a prefabricated path
                    {

                        GameObject Path = Instantiate(PathPrefab_3456_3, new Vector3(0f, 0f, 0f), Quaternion.identity, Menu.WorkObject.transform);
                        Path.transform.SetLocalPositionAndRotation(new Vector3(0f, 0f, 0f), Quaternion.identity);
                        Path.SetActive(true);
                        foreach (Transform child in Path.transform)  //Configuration of each target
                        {
                            TargetConfig Config = child.GetComponent<TargetConfig>();
                            Config.TargetManager = TargetManage;
                            Config.Interface = TargetInterface;
                            BoundsControl Bounds = child.GetComponent<BoundsControl>();
                            Bounds.RotateStopped.AddListener(delegate { TargetManage.UpdateValues(); });
                            Bounds.TranslateStopped.AddListener(delegate { TargetManage.UpdateValues(); });
                            Bounds.ScaleStopped.AddListener(delegate { TargetManage.UpdateValues(); });
                            ObjectManipulator Manipulator = child.GetComponent<ObjectManipulator>();
                            Manipulator.OnManipulationEnded.AddListener(delegate { TargetManage.UpdateValues(); });
                        }

                        if (Menu.SelectedPath != null)      //Make the new path the selected one to visualize it
                        {
                            Menu.SelectedPath.SetActive(false);
                        }

                        Menu.PathList.Add(Path);        //Add the path in the last position
                        Menu.SelectedPath = Path;
                        Menu.PathIterator = Menu.PathList.Count - 1;
                        Menu.UpdatePathNumber();        //Update the menu values


                        Path = Instantiate(PathPrefab_3456_4, new Vector3(0f, 0f, 0f), Quaternion.identity, Menu.WorkObject.transform);
                        Path.transform.SetLocalPositionAndRotation(new Vector3(0f, 0f, 0f), Quaternion.identity);
                        Path.SetActive(true);
                        foreach (Transform child in Path.transform)  //Configuration of each target
                        {
                            TargetConfig Config = child.GetComponent<TargetConfig>();
                            Config.TargetManager = TargetManage;
                            Config.Interface = TargetInterface;
                            BoundsControl Bounds = child.GetComponent<BoundsControl>();
                            Bounds.RotateStopped.AddListener(delegate { TargetManage.UpdateValues(); });
                            Bounds.TranslateStopped.AddListener(delegate { TargetManage.UpdateValues(); });
                            Bounds.ScaleStopped.AddListener(delegate { TargetManage.UpdateValues(); });
                            ObjectManipulator Manipulator = child.GetComponent<ObjectManipulator>();
                            Manipulator.OnManipulationEnded.AddListener(delegate { TargetManage.UpdateValues(); });
                        }

                        if (Menu.SelectedPath != null)      //Make the new path the selected one to visualize it
                        {
                            Menu.SelectedPath.SetActive(false);
                        }

                        Menu.PathList.Add(Path);        //Add the path in the last position
                        Menu.SelectedPath = Path;
                        Menu.PathIterator = Menu.PathList.Count - 1;
                        Menu.UpdatePathNumber();        //Update the menu values

                        Path = Instantiate(PathPrefab_3456_5, new Vector3(0f, 0f, 0f), Quaternion.identity, Menu.WorkObject.transform);
                        Path.transform.SetLocalPositionAndRotation(new Vector3(0f, 0f, 0f), Quaternion.identity);
                        Path.SetActive(true);
                        foreach (Transform child in Path.transform)  //Configuration of each target
                        {
                            TargetConfig Config = child.GetComponent<TargetConfig>();
                            Config.TargetManager = TargetManage;
                            Config.Interface = TargetInterface;
                            BoundsControl Bounds = child.GetComponent<BoundsControl>();
                            Bounds.RotateStopped.AddListener(delegate { TargetManage.UpdateValues(); });
                            Bounds.TranslateStopped.AddListener(delegate { TargetManage.UpdateValues(); });
                            Bounds.ScaleStopped.AddListener(delegate { TargetManage.UpdateValues(); });
                            ObjectManipulator Manipulator = child.GetComponent<ObjectManipulator>();
                            Manipulator.OnManipulationEnded.AddListener(delegate { TargetManage.UpdateValues(); });
                        }

                        if (Menu.SelectedPath != null)      //Make the new path the selected one to visualize it
                        {
                            Menu.SelectedPath.SetActive(false);
                        }

                        Menu.PathList.Add(Path);        //Add the path in the last position
                        Menu.SelectedPath = Path;
                        Menu.PathIterator = Menu.PathList.Count - 1;
                        Menu.UpdatePathNumber();        //Update the menu values


                    }
                }
                

                QRConfirmation = false;     //Button confirmation acknowledged
            }


        }
    }

    public void ToggleQRTracker()       //Turn on and off the QR code tracker
    {
        TrackerActive = !TrackerActive; 
        if (TrackerActive)
        {
            QrCodesManager.StartQRTracking();
        }
        else
        {
            QrCodesManager.StopQRTracking();
            Button.SetActive(false);
            StartSearching = false;
            QRConfirmation = false;
        }
    }

    public void FindRobotQR()       //Triggers the search of a QR that the first word is "Robot"
    {
        QRToSearch = "Robot";
        QRConfirmation = false;
        Universal = true;
        StartSearching = true;
        
    }

    public void FindWorkObjectQR()      //Triggers the search of a QR that the first word is "WorkObject"
    {
        QRToSearch = "WorkObject";
        QRConfirmation = false;
        Universal = true;
        StartSearching = true;

    }

    public void PlaceButton(Pose pose)      //Place the button with an offset to the given pose
    {
        Pose ButtonPosition;

        ButtonPosition.position.x = pose.position.x;
        ButtonPosition.position.y = pose.position.y;
        ButtonPosition.position.z = pose.position.z;

        ButtonPosition.rotation = pose.rotation;

        //Cambiar Rotation Y añadir Translate y rotate.

        Button.transform.SetPositionAndRotation(ButtonPosition.position,ButtonPosition.rotation);
        Button.transform.Rotate(new Vector3(-180f, 0f, 0f));    
        Button.transform.Translate(new Vector3(0.04f, -0.15f, -0.1f),Space.Self);
    }

    public void ButtonConfirmation()    //Function to call when the button is pressed
    {
        QRConfirmation = true;
    }
    public Pose GetQRPose(string str, bool UniversalElement, out bool Found)        //Procedure to retrieve the position of a QR code if found
    {
        Pose Result = new Pose();
        code = null;
        QrCodesList = QrCodesManager.GetList();         //Get the internal QR code list of the HoloLens 2
        code = TryGetQrCode(QrCodesList, str, UniversalElement);    //Search for the given QR code information

        if (code != null)   //If a QR code is found
        {
            node = SpatialGraphNode.FromStaticNodeId(code.SpatialGraphNodeId);  //Retrieve the position and rotation if it can be located
            if (node != null && node.TryLocate(FrameTime.OnUpdate, out Pose pose))
            {
                Result = pose;
                Found = true;
            }
            else
            {
                Found = false;
            }
        }
        else
        {
            Found = false;
        }

        return Result;
    }
    private Microsoft.MixedReality.QR.QRCode TryGetQrCode(System.Collections.Generic.IList<Microsoft.MixedReality.QR.QRCode> list, string str, bool UniversalElement)       //Procedure to search for a QR code in the internal list
    {
        DateTimeOffset timeStampLast = new DateTimeOffset();
        Microsoft.MixedReality.QR.QRCode result = null;
        string[] stSplit = {""};
        foreach (Microsoft.MixedReality.QR.QRCode code in list)     //Analyze all the QR codes in the internal list
        {
            
            if (UniversalElement)       //If searching for a non unique QR code, analyze the first word
            {
                stSplit = code.Data.Split('_');
                
            }
            if ((str.ToLower() == code.Data.ToLower()) || (stSplit[0].ToLower() == str.ToLower() && UniversalElement))  //If there is a coincidence
            {
                //  PrintText("timeStampLast " + timeStampLast.Ticks.ToString());
                DateTimeOffset timeStamp = code.LastDetectedTime;
                //  PrintText("timeStamp " + timeStamp.Ticks.ToString());
                if (timeStamp.Ticks > timeStampLast.Ticks)
                {
                    result = code;      //Return the code that meets the requirements
                    timeStampLast = timeStamp;
                }
            }
        }
        return result;
    }

}
