using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Events;
using Microsoft.MixedReality.Toolkit.UI;
using TMPro;
using RK;

public class JointSetter : MonoBehaviour
{

    public GameObject PrefabSlide;
    public GameObject SlideFolder;
    public GameObject Importer;

    private RobotMechanismBuilder Robot;
    [HideInInspector]
    public int JointNumber;
    [HideInInspector]
    public PinchSlider[] slides;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    //Initialize slides
    public void SetUp()
    {
        int i;
        float j = 0.14f;

        Robot = Importer.GetComponent<RobotMechanismBuilder>();
        JointNumber = Importer.transform.GetChild(0).transform.GetChild(0).GetComponent<Mechanism>().jointValues.Length;

        slides = new PinchSlider[JointNumber];

        for (i = 0; i < JointNumber; i++)
        {
            GameObject slideConf = Instantiate(PrefabSlide, new Vector3(transform.position.x, transform.position.y + j, transform.position.z), Quaternion.identity, SlideFolder.transform);
            slideConf.name = "Joint " + (i + 1).ToString();
            slides[i] = slideConf.GetComponent<PinchSlider>();
            slides[i].OnValueUpdated.AddListener(delegate { SetPosition(); });
            GameObject childTick = slideConf.transform.Find("TickMarks").gameObject;
            GameObject childText = childTick.transform.Find("Joint").gameObject;
            TextMeshPro Text = childText.GetComponent<TextMeshPro>();
            Text.SetText(slideConf.name);
            j = j - 0.05f;
        }
    }

    //It is called when a slide changes its value
    public void SetPosition()
    {
        float Position = 0f;
        int i;
        for (i = 0; i < JointNumber; i++)
        {
            Position = (slides[i].SliderValue - 0.5f) * 2f * Mathf.PI;
            Robot.UpdateMechJoints(i, Position);
        }
        

    }

    //Update the position of the joints each time the slide changes
    public void UpdateJoints(string JointString)
    {
        string[] Values = JointString.Split('|');
        int i;
        for (i = 0; i < JointNumber; i++)
        {
            Robot.UpdateMechJoints(i, float.Parse(Values[i], CultureInfo.InvariantCulture)*Mathf.PI/180f);
        }
    }
}
