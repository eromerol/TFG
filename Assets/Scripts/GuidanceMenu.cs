using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.Examples.Demos;

public class GuidanceMenu : MonoBehaviour
{
    int counter = 0; //counter to know the step you are in
    int maxnumsteps = 20; //max number of guidance steps
    public List<MeshRenderer> meshRenderers; // MeshRenderers list

    //we need to save the original colors for the mesh renderers, in order to do that we upload one button from each menu
    public MeshRenderer TargetButton;
    public MeshRenderer RobotButton;
    public MeshRenderer StationButton;
    public MeshRenderer HandButton;
    public MeshRenderer Orange;

    [SerializeField]
    private TextMeshPro GuidanceText;
    [SerializeField]
    private TextMeshPro GuidanceTitle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (counter)
        {
            case 0:
                GuidanceTitle.text = "";
                GuidanceText.text = "Guidance Hand Menu. Select Next to start";
                meshRenderers[0].material.color = HandButton.material.color;
                meshRenderers[1].material.color = StationButton.material.color;
                meshRenderers[2].material.color = StationButton.material.color;
                meshRenderers[3].material.color = StationButton.material.color;
                meshRenderers[4].material.color = HandButton.material.color;
                meshRenderers[5].material.color = TargetButton.material.color;
                meshRenderers[6].material.color = HandButton.material.color;
                meshRenderers[7].material.color = RobotButton.material.color;
                meshRenderers[8].material.color = RobotButton.material.color;
                meshRenderers[9].material.color = RobotButton.material.color;
                meshRenderers[10].material.color = RobotButton.material.color;
                meshRenderers[11].material.color = RobotButton.material.color;
                break;
            case 1:
                GuidanceTitle.text = "STEP 1";
                GuidanceText.text = "Look at your left palm and select Station Menu.\r\nThen, Select QR Code Robot and look at the QR\r\non the left side of the robot.";
                meshRenderers[0].material.color = Color.red; //Station menu button in hand menu
                meshRenderers[1].material.color = Orange.material.color; //QR Robot in Station Menu
                meshRenderers[2].material.color = StationButton.material.color;
                meshRenderers[3].material.color = StationButton.material.color;
                meshRenderers[4].material.color = HandButton.material.color;
                meshRenderers[5].material.color = TargetButton.material.color;
                meshRenderers[6].material.color = HandButton.material.color;
                meshRenderers[7].material.color = RobotButton.material.color;
                meshRenderers[8].material.color = RobotButton.material.color;
                meshRenderers[9].material.color = RobotButton.material.color;
                meshRenderers[10].material.color = RobotButton.material.color;
                meshRenderers[11].material.color = RobotButton.material.color;
                break;
            case 2:
                GuidanceTitle.text = "STEP 2. OPTIONAL";
                GuidanceText.text = "Select QR Code WorkObject and place the QR\r\nin the desired position";
                meshRenderers[0].material.color = HandButton.material.color; 
                meshRenderers[1].material.color = StationButton.material.color;
                meshRenderers[2].material.color = Orange.material.color; //QR WorkObject in Station Menu
                meshRenderers[3].material.color = StationButton.material.color;
                meshRenderers[4].material.color = HandButton.material.color;
                meshRenderers[5].material.color = TargetButton.material.color;
                meshRenderers[6].material.color = HandButton.material.color;
                meshRenderers[7].material.color = RobotButton.material.color;
                meshRenderers[8].material.color = RobotButton.material.color;
                meshRenderers[9].material.color = RobotButton.material.color;
                meshRenderers[10].material.color = RobotButton.material.color;
                meshRenderers[11].material.color = RobotButton.material.color;
                break;
            case 3:
                GuidanceTitle.text = "STEP 3";
                GuidanceText.text = "Select QR Search Turn Off";
                meshRenderers[0].material.color = HandButton.material.color;
                meshRenderers[1].material.color = StationButton.material.color;
                meshRenderers[2].material.color = StationButton.material.color;
                meshRenderers[3].material.color = Orange.material.color; //Search Turn Off button in StationMenu
                meshRenderers[4].material.color = HandButton.material.color;
                meshRenderers[5].material.color = TargetButton.material.color;
                meshRenderers[6].material.color = HandButton.material.color;
                meshRenderers[7].material.color = RobotButton.material.color;
                meshRenderers[8].material.color = RobotButton.material.color;
                meshRenderers[9].material.color = RobotButton.material.color;
                meshRenderers[10].material.color = RobotButton.material.color;
                meshRenderers[11].material.color = RobotButton.material.color;

                break; 
            case 4:
                GuidanceTitle.text = "STEP 4";
                GuidanceText.text = "Look at your left palm and select Target Menu. \r\n In Target Menu, create a path or modify the existed ones as you want.\r\nFor doubts, press Help button";
                meshRenderers[0].material.color = HandButton.material.color;
                meshRenderers[1].material.color = StationButton.material.color;
                meshRenderers[2].material.color = StationButton.material.color;
                meshRenderers[3].material.color = StationButton.material.color;
                meshRenderers[4].material.color = Color.red; //Target Button in Hand Menu
                meshRenderers[5].material.color = Color.red; //Help button in Target Menu
                meshRenderers[6].material.color = HandButton.material.color;
                meshRenderers[7].material.color = RobotButton.material.color;
                meshRenderers[8].material.color = RobotButton.material.color;
                meshRenderers[9].material.color = RobotButton.material.color;
                meshRenderers[10].material.color = RobotButton.material.color;
                meshRenderers[11].material.color = RobotButton.material.color;
                break;
            case 5:
                GuidanceTitle.text = "STEP 5";
                GuidanceText.text = "Once you have finished with the path. Look at your palm and open Robot Menu.\r\nIn Robot Menu, select Send Targets.\r\nIf one target is not reachable,solve the errors from the targets.";
                meshRenderers[0].material.color = HandButton.material.color;
                meshRenderers[1].material.color = StationButton.material.color;
                meshRenderers[2].material.color = StationButton.material.color;
                meshRenderers[3].material.color = StationButton.material.color;
                meshRenderers[4].material.color = HandButton.material.color;
                meshRenderers[5].material.color = TargetButton.material.color;
                meshRenderers[6].material.color = Color.red; // Robot Button in Hand Menu
                meshRenderers[7].material.color = Color.red; //Send Targets in Robot Menu
                meshRenderers[8].material.color = RobotButton.material.color;
                meshRenderers[9].material.color = RobotButton.material.color;
                meshRenderers[10].material.color = RobotButton.material.color;
                meshRenderers[11].material.color = RobotButton.material.color;
                break;
            case 6:
                GuidanceTitle.text = "STEP 6";
                GuidanceText.text = "To preview the selected path, select Preview Path and, for closing the preview, select Clear Trajectory Preview.";
                meshRenderers[0].material.color = HandButton.material.color;
                meshRenderers[1].material.color = StationButton.material.color;
                meshRenderers[2].material.color = StationButton.material.color;
                meshRenderers[3].material.color = StationButton.material.color;
                meshRenderers[4].material.color = HandButton.material.color;
                meshRenderers[5].material.color = TargetButton.material.color;
                meshRenderers[6].material.color = HandButton.material.color;
                meshRenderers[7].material.color = RobotButton.material.color;
                meshRenderers[8].material.color = Color.red;//Preview Path in Robot Menu
                meshRenderers[9].material.color = Color.red;//Clear Trajectory Preview in Robot Menu
                meshRenderers[10].material.color = RobotButton.material.color;
                meshRenderers[11].material.color = RobotButton.material.color;
                break;
            case 7:
                GuidanceTitle.text = "STEP 7";
                GuidanceText.text = "If you have only one path, select Start Path\r\nIf you have more than one, go to step 8";
                meshRenderers[0].material.color = HandButton.material.color;
                meshRenderers[1].material.color = StationButton.material.color;
                meshRenderers[2].material.color = StationButton.material.color;
                meshRenderers[3].material.color = StationButton.material.color;
                meshRenderers[4].material.color = HandButton.material.color;
                meshRenderers[5].material.color = TargetButton.material.color;
                meshRenderers[6].material.color = HandButton.material.color;
                meshRenderers[7].material.color = RobotButton.material.color;
                meshRenderers[8].material.color = RobotButton.material.color;
                meshRenderers[9].material.color = RobotButton.material.color;
                meshRenderers[10].material.color = Color.red; //Start Path in Robot Menu
                meshRenderers[11].material.color = RobotButton.material.color;
                break;
            case 8:
                GuidanceTitle.text = "STEP 8";
                GuidanceText.text = "If you have more than one path, select Start Process";
                meshRenderers[0].material.color = HandButton.material.color;
                meshRenderers[1].material.color = StationButton.material.color;
                meshRenderers[2].material.color = StationButton.material.color;
                meshRenderers[3].material.color = StationButton.material.color;
                meshRenderers[4].material.color = HandButton.material.color;
                meshRenderers[5].material.color = TargetButton.material.color;
                meshRenderers[6].material.color = HandButton.material.color;
                meshRenderers[7].material.color = RobotButton.material.color;
                meshRenderers[8].material.color = RobotButton.material.color;
                meshRenderers[9].material.color = RobotButton.material.color;
                meshRenderers[10].material.color = RobotButton.material.color;
                meshRenderers[11].material.color = Color.red; //Start Process in Robot Menu
                break;
            default:
                // Código a ejecutar si la expresión no coincide con ninguno de los casos anteriores
                break;
        }
    }

    public void Next() 
    { 
        if (counter < maxnumsteps)
        {
            counter+=1;
        }
    }

    public void Back()
    {
        if (counter > 0)
        {
            counter-=1;
        }
    }
}
