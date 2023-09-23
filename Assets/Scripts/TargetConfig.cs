using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI.BoundsControl;
using Microsoft.MixedReality.Toolkit.UI;

public class TargetConfig : MonoBehaviour
{
    public static GameObject SelectedTarget;
    public static GameObject PreviousSelectedTarget;
    public string ZoneConfig = "fine";
    public string SpeedConfig = "v100";
    public string MovementConfig = "MoveL";
    public bool GripperOpened = false;
    public int Order=0;
    public GameObject Interface;
    
    public TargetManager TargetManager;


    private void OnTriggerEnter(Collider other)     //When a collision between the box collider and the hands is detected
    {
        Interface.SetActive(true);
        PreviousSelectedTarget = SelectedTarget;
        SelectedTarget = this.gameObject;
        if(PreviousSelectedTarget != null)  //Disable the possibility to move the deselected target
        {
            PreviousSelectedTarget.GetComponent<BoundsControl>().enabled = false;
            PreviousSelectedTarget.GetComponent<ObjectManipulator>().enabled = false;
        }
        SelectedTarget.GetComponent<BoundsControl>().enabled = true;    //Enable the possibility to move the selected target
        SelectedTarget.GetComponent<ObjectManipulator>().enabled = true;
        TargetManager.ChangeSelectedTarget(this.gameObject);        //Update the target interface values 
        TargetManager.UpdateValues();
    }

}
