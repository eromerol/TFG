using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using TMPro;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Experimental.UI;
using Microsoft.MixedReality.Toolkit.UI.BoundsControl;


public class TargetManager : MonoBehaviour
{
#if WINDOWS_UWP
    MixedRealityKeyboard keyboard;
#endif
    public bool PreviewTool = false;
    [SerializeField]
    private GameObject SourceTarget;
    [SerializeField]
    private MainMenu Menu;
    public GameObject SelectedTarget;
    [SerializeField]
    private TextMeshPro PosX;
    [SerializeField]
    private TextMeshPro PosY;
    [SerializeField]
    private TextMeshPro PosZ;
    [SerializeField]
    private TextMeshPro RotRoll;
    [SerializeField]
    private TextMeshPro RotPitch;
    [SerializeField]
    private TextMeshPro RotYaw;
    [SerializeField]
    private TMP_Text MovementText;
    [SerializeField]
    private PinchSlider MovementSlider;
    [SerializeField]
    private TMP_Text SpeedText;
    [SerializeField]
    private PinchSlider SpeedSlider;
    [SerializeField]
    private TMP_Text ZoneText;
    [SerializeField]
    private PinchSlider ZoneSlider;
    [SerializeField]
    private TMP_Text GripperText;
    [SerializeField]
    private PinchSlider GripperSlider;
    [SerializeField]
    private TextMeshPro OrderText;
    [SerializeField]
    private GameObject Interface;

    private TMP_Text SelectedText;

    public bool MovingSliders = false;


    void Start()    
    {
        //Initialize the Keyboard for modifying the target config
#if WINDOWS_UWP
        keyboard = gameObject.AddComponent<MixedRealityKeyboard>();
        keyboard.DisableUIInteractionWhenTyping = true;
#endif
    }

    // Update is called once per frame
    void Update()
    {
        //Update in real time to show what is being write in a Text
#if WINDOWS_UWP
        if (keyboard.Visible && SelectedText != null)
        {
            SelectedText.text = keyboard.Text;
            ChangeTargetConfig();
        }
#endif
        if (MovingSliders)
        {
            ChangeTargetConfig();   //Update the values and text of the interface in real time
        }
    }

    public void AddTarget() //Create a new target in the active path
    {
        GameObject NewTarget = GameObject.Instantiate(SourceTarget, new Vector3(0f,0.5f,-0.8f), Quaternion.Euler(180f,0f,0f),Menu.SelectedPath.transform);
        NewTarget.SetActive(true);
        NewTarget.transform.SetLocalPositionAndRotation(new Vector3(0f, 0.5f, -0.8f), Quaternion.Euler(180f, 0f, 0f));
        SelectedTarget = NewTarget;
        UpdateValues();
    }
    public void ChangeSelectedTarget(GameObject Target)
    {
        SelectedTarget = Target;
    }
    public void UpdateValues()      //Procedure to update all the values of the interface
    {
        if (SelectedTarget != null)
        {
            if (SelectedTarget.transform.localPosition.y < 0.01f)   //Limit the target so that it is not under the table
            {
                SelectedTarget.transform.SetLocalPositionAndRotation(new Vector3(SelectedTarget.transform.localPosition.x,0.01f, SelectedTarget.transform.localPosition.z), SelectedTarget.transform.localRotation);
            }
            //Show the position and rotation of the target
            PosX.text = SelectedTarget.transform.localPosition.x.ToString("F4", CultureInfo.InvariantCulture);
            PosY.text = SelectedTarget.transform.localPosition.y.ToString("F4", CultureInfo.InvariantCulture);
            PosZ.text = SelectedTarget.transform.localPosition.z.ToString("F4", CultureInfo.InvariantCulture);
            RotRoll.text = SelectedTarget.transform.localRotation.eulerAngles.x.ToString("F1", CultureInfo.InvariantCulture);
            RotPitch.text = SelectedTarget.transform.localRotation.eulerAngles.y.ToString("F1", CultureInfo.InvariantCulture);
            RotYaw.text = SelectedTarget.transform.localRotation.eulerAngles.z.ToString("F1", CultureInfo.InvariantCulture);
            
            string message;
            TargetConfig Config = SelectedTarget.GetComponent<TargetConfig>();  //Get the movement information and update the slider and text
            switch(Config.MovementConfig)
            {
                case "MoveJ":
                    MovementSlider.SliderValue = 0f;
                    message = "Joint";
                    break;
                case "MoveL":
                    MovementSlider.SliderValue = 0.5f;
                    message = "Linear";
                    break;
                case "MoveC":
                    MovementSlider.SliderValue = 1f;
                    message = "Circular";
                    break;
                default:
                    MovementSlider.SliderValue = 0.5f;
                    message = "Linear";
                    break;
            }
            MovementText.text = message;

            switch (Config.SpeedConfig)         //Get the speed information and update the slider and text
            {
                case "v5":
                    SpeedSlider.SliderValue = 0f;
                    message = "v5";
                    break;
                case "v10":
                    SpeedSlider.SliderValue = 0.1f;
                    message = "v10";
                    break;
                case "v40":
                    SpeedSlider.SliderValue = 0.2f;
                    message = "v40";
                    break;
                case "v60":
                    SpeedSlider.SliderValue = 0.3f;
                    message = "v60";
                    break;
                case "v80":
                    SpeedSlider.SliderValue = 0.4f;
                    message = "v80";
                    break;
                case "v100":
                    SpeedSlider.SliderValue = 0.5f;
                    message = "v100";
                    break;
                case "v150":
                    SpeedSlider.SliderValue = 0.6f;
                    message = "v150";
                    break;
                case "v200":
                    SpeedSlider.SliderValue = 0.7f;
                    message = "v200";
                    break;
                case "v300":
                    SpeedSlider.SliderValue = 0.8f;
                    message = "v300";
                    break;
                case "v400":
                    SpeedSlider.SliderValue = 0.9f;
                    message = "v400";
                    break;
                case "v500":
                    SpeedSlider.SliderValue = 1f;
                    message = "v500";
                    break;
                default:
                    SpeedSlider.SliderValue = 0.5f;
                    message = "v100";
                    break;
            }

            SpeedText.text = message;

            switch (Config.ZoneConfig)          //Get the zone information and update the slider and text
            {
                case "fine":
                    ZoneSlider.SliderValue = 0f;
                    message = "fine";
                    break;
                case "z0":
                    ZoneSlider.SliderValue = 0.1f;
                    message = "z0";
                    break;
                case "z1":
                    ZoneSlider.SliderValue = 0.2f;
                    message = "z1";
                    break;
                case "z5":
                    ZoneSlider.SliderValue = 0.3f;
                    message = "z5";
                    break;
                case "z10":
                    ZoneSlider.SliderValue = 0.4f;
                    message = "z10";
                    break;
                case "z15":
                    ZoneSlider.SliderValue = 0.5f;
                    message = "z15";
                    break;
                case "z20":
                    ZoneSlider.SliderValue = 0.6f;
                    message = "z20";
                    break;
                case "z30":
                    ZoneSlider.SliderValue = 0.7f;
                    message = "z30";
                    break;
                case "z40":
                    ZoneSlider.SliderValue = 0.8f;
                    message = "z40";
                    break;
                case "z50":
                    ZoneSlider.SliderValue = 0.9f;
                    message = "z50";
                    break;
                case "z60":
                    ZoneSlider.SliderValue = 1f;
                    message = "z60";
                    break;
                default:
                    ZoneSlider.SliderValue = 0f;
                    message = "fine";
                    break;
            }
            ZoneText.text = message;

            if (Config.GripperOpened)           //Get the gripper information and update the slider and text
            {
                GripperSlider.SliderValue = 1f;
                message = "Open";
            }
            else
            {
                GripperSlider.SliderValue = 0f;
                message = "Close";
            }
            GripperText.text = message;

            OrderText.text = Config.Order.ToString();   //Get the order information and update the slider and text
        }
    }

    public void ChangeTargetConfig()        //Modify the target stored data
    {
        if (SelectedTarget != null)
        {
            Vector3 Position = new Vector3(float.Parse(PosX.text, CultureInfo.InvariantCulture), float.Parse(PosY.text, CultureInfo.InvariantCulture), float.Parse(PosZ.text, CultureInfo.InvariantCulture));   //Get the position introduced with the keyboard
            if (Position.y < 0.01f) 
            {
                Position.y = 0.01f; //Limit the target height so that it does not hit the table
            }
            SelectedTarget.transform.SetLocalPositionAndRotation(Position, SelectedTarget.transform.localRotation);
            Vector3 Rotation = new Vector3(float.Parse(RotRoll.text, CultureInfo.InvariantCulture), float.Parse(RotPitch.text, CultureInfo.InvariantCulture), float.Parse(RotYaw.text, CultureInfo.InvariantCulture));
            SelectedTarget.transform.localEulerAngles = Rotation;
            TargetConfig Config = SelectedTarget.GetComponent<TargetConfig>();
            
            switch (MovementSlider.SliderValue)     //Get the Slider value and update the movement data
            {
                case 0f:
                    Config.MovementConfig = "MoveJ";
                    break;
                case 0.5f:
                    Config.MovementConfig = "MoveL";
                    break;
                case 1f:
                    Config.MovementConfig = "MoveC";
                    break;
                default:
                    Config.MovementConfig = "MoveL";
                    break;
            }

            switch (SpeedSlider.SliderValue)    //Get the Slider value and update the speed data
            {
                case 0f:
                    Config.SpeedConfig = "v5";
                    break;
                case 0.1f:
                    Config.SpeedConfig = "v10";
                    break;
                case 0.2f:
                    Config.SpeedConfig = "v40";
                    break;
                case 0.3f:
                    Config.SpeedConfig = "v60";
                    break;
                case 0.4f:
                    Config.SpeedConfig = "v80";
                    break;
                case 0.5f:
                    Config.SpeedConfig = "v100";
                    break;
                case 0.6f:
                    Config.SpeedConfig = "v150";
                    break;
                case 0.7f:
                    Config.SpeedConfig = "v200";
                    break;
                case 0.8f:
                    Config.SpeedConfig = "v300";
                    break;
                case 0.9f:
                    Config.SpeedConfig = "v400";
                    break;
                case 1f:
                    Config.SpeedConfig = "v500";
                    break;
                default:
                    Config.SpeedConfig = "v100";
                    break;
            }

            switch (ZoneSlider.SliderValue)     //Get the Slider value and update the zone data
            {
                case 0f:
                    Config.ZoneConfig = "fine";
                    break;
                case 0.1f:
                    Config.ZoneConfig = "z0";
                    break;
                case 0.2f:
                    Config.ZoneConfig = "z1";
                    break;
                case 0.3f:
                    Config.ZoneConfig = "z5";
                    break;
                case 0.4f:
                    Config.ZoneConfig = "z10";
                    break;
                case 0.5f:
                    Config.ZoneConfig = "z15";
                    break;
                case 0.6f:
                    Config.ZoneConfig = "z20";
                    break;
                case 0.7f:
                    Config.ZoneConfig = "z30";
                    break;
                case 0.8f:
                    Config.ZoneConfig = "z40";
                    break;
                case 0.9f:
                    Config.ZoneConfig = "z50";
                    break;
                case 1f:
                    Config.ZoneConfig = "z60";
                    break;
                default:
                    Config.ZoneConfig = "fine";
                    break;
            }

            switch (GripperSlider.SliderValue)      //Get the Slider value and update the gripper data
            {
                case 0f:
                    Config.GripperOpened = false;
                    break;
                case 1f:
                    Config.GripperOpened = true;
                    break;
                default:
                    Config.GripperOpened = true;
                    break;
            }

            Config.Order = int.Parse(OrderText.text, CultureInfo.InvariantCulture);
            UpdateValues();

        }
    }

    public void DeselectTarget()        //Deselect the target and disable the movement of it
    {
        if(SelectedTarget != null)
        {
            SelectedTarget.GetComponent<BoundsControl>().enabled = false;
            SelectedTarget.GetComponent<ObjectManipulator>().enabled = false;
            Interface.SetActive(false);
        }
        
        SelectedTarget = null;

        
    }

    public void DeleteSelectedTarget()  
    {
        Destroy(SelectedTarget);
    }

    public void DeleteAllTargets()
    {
        foreach(Transform child in Menu.SelectedPath.transform)
        {
            if (child.gameObject.activeSelf)
            {
                Destroy(child.gameObject);
            }
        }
    }

    public void InputKeyboard(TMP_Text Text)    //Open the keyboard to modify the parameter text
    {
        SelectedText = Text;
#if WINDOWS_UWP
        keyboard.ShowKeyboard(SelectedText.text,false);
#endif
 
    }

    public void IncreaseTargetOrder()   //Increase the order data stored in the selected target
    {
        if(SelectedTarget != null)
        {
            TargetConfig Config = SelectedTarget.GetComponent<TargetConfig>();
            Config.Order++;
            UpdateValues();
        }
    }

    public void DecreaseTargetOrder()   //Decrease the order data stored in the selected target
    {
        if (SelectedTarget != null)
        {
            TargetConfig Config = SelectedTarget.GetComponent<TargetConfig>();
            if(Config.Order != 0)
            {
                Config.Order--;
            }
            UpdateValues();
        }
    }
    public void TogglePreview()     //Toggle the state to preview how the tool will approach to the target
    {
        PreviewTool = !PreviewTool;
    }

    public void ChangeSliderStatus(bool Status) //Update if there is a slider being moved
    {
        MovingSliders = Status;
    }

    public void PickOperation()
    {
        GameObject GrabPoint = Menu.FoundPiece.transform.Find("GrabPoint").gameObject;
        AddTarget();
        SelectedTarget.transform.position = new Vector3(GrabPoint.transform.position.x, GrabPoint.transform.position.y + 0.1f, GrabPoint.transform.position.z);
        SelectedTarget.transform.rotation = Quaternion.Euler(180f, 0f, 0f);
        TargetConfig Config = SelectedTarget.GetComponent<TargetConfig>();
        Config.Order = 1;
        Config.GripperOpened = true;

        AddTarget();
        SelectedTarget.transform.position = new Vector3(GrabPoint.transform.position.x, GrabPoint.transform.position.y, GrabPoint.transform.position.z);
        SelectedTarget.transform.rotation = Quaternion.Euler(180f, 0f, 0f);
        Config = SelectedTarget.GetComponent<TargetConfig>();
        Config.Order = 2;
        Config.GripperOpened = false;
        Config.SpeedConfig = "v40";
        Config.ZoneConfig = "fine";
        Config.MovementConfig = "MoveL";

        AddTarget();
        SelectedTarget.transform.position = new Vector3(GrabPoint.transform.position.x, GrabPoint.transform.position.y + 0.15f, GrabPoint.transform.position.z);
        SelectedTarget.transform.rotation = Quaternion.Euler(180f, 0f, 0f);
        Config = SelectedTarget.GetComponent<TargetConfig>();
        Config.Order = 3;
        Config.GripperOpened = false;
        Config.SpeedConfig = "v40";
        Config.ZoneConfig = "fine";
        Config.MovementConfig = "MoveL";


    }

    public void PlaceOperation()
    {
        GameObject GrabPoint = Menu.FoundPiece.transform.Find("GrabPoint").gameObject;
        AddTarget();
        SelectedTarget.transform.position = new Vector3(GrabPoint.transform.position.x, GrabPoint.transform.position.y + 0.1f, GrabPoint.transform.position.z);
        SelectedTarget.transform.rotation = Quaternion.Euler(180f, 0f, 0f);
        TargetConfig Config = SelectedTarget.GetComponent<TargetConfig>();
        Config.Order = 1;
        Config.GripperOpened = false;

        AddTarget();
        SelectedTarget.transform.position = new Vector3(GrabPoint.transform.position.x, GrabPoint.transform.position.y, GrabPoint.transform.position.z);
        SelectedTarget.transform.rotation = Quaternion.Euler(180f, 0f, 0f);
        Config = SelectedTarget.GetComponent<TargetConfig>();
        Config.Order = 2;
        Config.GripperOpened = true;
        Config.SpeedConfig = "v40";
        Config.ZoneConfig = "fine";
        Config.MovementConfig = "MoveL";

        AddTarget();
        SelectedTarget.transform.position = new Vector3(GrabPoint.transform.position.x, GrabPoint.transform.position.y + 0.15f, GrabPoint.transform.position.z);
        SelectedTarget.transform.rotation = Quaternion.Euler(180f, 0f, 0f);
        Config = SelectedTarget.GetComponent<TargetConfig>();
        Config.Order = 3;
        Config.GripperOpened = true;
        Config.SpeedConfig = "v40";
        Config.ZoneConfig = "fine";
        Config.MovementConfig = "MoveL";


    }

}
