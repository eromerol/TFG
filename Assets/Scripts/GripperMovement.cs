using System;
using System.Collections;
using UnityEngine;

public class GripperMovement : MonoBehaviour
{
    //Define the housing part and the fingers
    private Transform finger1;
    private Transform finger2;
    private Transform housing;

    // Define the distance to move the fingers
    private float fingerMoveDistance = 0.006f;

    // Define la tolerancia para la comparación de posiciones
    float tolerance = 0.0008f;

    // Define the original finger positions
    private Vector3 finger1ClosedPos;
    private Vector3 finger2ClosedPos;

    // Define the speed at which the fingers move
    private float gripperSpeed = 0.01f;

    // Control variables
    public bool Close = true;
    public bool Moving = false;
    public bool Open = false;

    // First time confirmation
    public bool FirstTime = true;
    private bool UpdateGripper = false;
    void Start()
    {
    }
    
    // Update is called once per frame
    void Update()
    {
        if (UpdateGripper && Moving)
        {
            float step = gripperSpeed * Time.deltaTime;

            // Move the fingers towards each other
            if (Close == false)
            {
                Vector3 finger1TargetPos = finger1.localPosition - Vector3.right * step;
                Vector3 finger2TargetPos = finger2.localPosition + Vector3.right * step;
                finger1.localPosition = Vector3.MoveTowards(finger1.localPosition, finger1TargetPos, 0.001f);
                finger2.localPosition = Vector3.MoveTowards(finger2.localPosition, finger2TargetPos, 0.001f);

                // Compara las posiciones actuales de los dedos con sus posiciones originales
                float finger1Distance = Vector3.Distance(finger1.localPosition, finger1ClosedPos);
                float finger2Distance = Vector3.Distance(finger2.localPosition, finger2ClosedPos);

                if (finger1Distance <= tolerance && finger2Distance <= tolerance)
                {
                    Moving = false;
                    Open = false;
                    Close = true;
                }
            }
            // Move the fingers away from each other
            else
            {
                Vector3 finger1TargetPosL = finger1.localPosition - Vector3.left * step;
                Vector3 finger2TargetPosL = finger2.localPosition + Vector3.left * step;
                finger1.localPosition = Vector3.MoveTowards(finger1.localPosition, finger1TargetPosL, 0.001f);
                finger2.localPosition = Vector3.MoveTowards(finger2.localPosition, finger2TargetPosL, 0.001f);

                if (Vector3.Distance(finger1.localPosition, finger1ClosedPos) >= fingerMoveDistance && Vector3.Distance(finger2.localPosition, finger2ClosedPos) >= fingerMoveDistance) 
                {
                    Close = false;
                    Moving = false;
                    Open = true;
                }
            }
        }
    }


    public void ToggleGripper()
    {
        if (FirstTime)
        {
            // retrieve finger objects and housing object from script owner
            housing = transform.GetChild(0).GetChild(0).GetChild(0);
            finger1 = transform.GetChild(0).GetChild(0).GetChild(1).GetChild(1);
            finger2 = transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0);
            finger1ClosedPos = finger1.localPosition;
            finger2ClosedPos = finger2.localPosition;
            FirstTime = false;
            UpdateGripper = true;
        }

        if (Open==true && Close==false && Moving==false)
        {
            Moving = true;
            Close = false;
            Open = false;
        }
        else if (Close == true && Open == false && Moving == false)
        {
            Moving = true;
            Close = true;
            Open = false;
        }


    }
}