using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RK;

public class RobotJointSet : MonoBehaviour
{
    public RobotMechanismBuilder Robot;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Change()
    {

        Robot.UpdateMechJoints(0, 0.5f);

    }
}


