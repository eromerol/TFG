using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Microsoft.MixedReality.SampleQRCodes;
using Microsoft.MixedReality.QR;
using Microsoft.MixedReality.OpenXR;
using System;


public class QR_tracking_Tool : MonoBehaviour
{
    // Read the qrs and places the virutal button on the real tool
    // when that button is pressed it communicates to DeployTargets to create the new target on the tcp of the tool
    
    [SerializeField]
    private QRCodesManager qrCodesManager;          // QR codes manager
    [SerializeField]
    private GameObject qrVisualizer;

    private bool _QRTrackingEnabled = false;    // if the QR tracking is enabled or not
    private bool _QR_PointerToolFound = false;      //If the QR for the tool has been found
    private System.Collections.Generic.IList<Microsoft.MixedReality.QR.QRCode> qrCodesList = null;
    private SpatialGraphNode node = null;
    private Microsoft.MixedReality.QR.QRCode code = null;

    // Placing the Button and the new Target
    [SerializeField]
    private GameObject targetPlacingButton;
    [SerializeField]
    private GameObject QRTargetPlacingButton;
    [SerializeField]
    private GameObject offsetTargetPlacingButton;
    [SerializeField]
    private GameObject invOffsetTargetPlacingButton;
    [SerializeField]
    private GameObject offsetTargetHelper;
    [SerializeField]
    private GameObject targetHelper;
    [SerializeField]
    private GameObject invOffsetTarget;
    [SerializeField]
    private GameObject offsetTarget;
    [SerializeField]
    private GameObject deploy;

    // Variables to read the qrs
    bool inv = false;

    Vector3 auxPosePos;
    Quaternion auxPoseRot;

    long lastTimeStamp = 0;
    long aux;
    long auxStop = 80;

    long lastTimeStampInv = 0;
    long auxInv;

    int qrState = 0; // 0 is when they are not reading, 1 is when tool is reading and 2 when toolInv is reading
    bool toolReading = false;
    bool toolInvReading = false;


    public void Update()
    {
        if (_QRTrackingEnabled)     //Read the QR code dynamically when the button is toggled
        {
            updateQRtracking();
        }
    }

    //Function the button calls to track the QR
    public void ActivationQRCodeTracking(bool active)
    {
        if (active)  // antes no estaba activado y al pulsar el botón se activa
        {
            if (qrCodesManager != null)     //check the manager is set
            {
                qrVisualizer.GetComponent<QRCodesVisualizer>().enabled = false;
                qrCodesManager.StartQRTracking();
                Debug.Log("Enabled QR_Code Tracking");               
            }
        }
        else
        {
            qrVisualizer.GetComponent<QRCodesVisualizer>().enabled = true;
            targetPlacingButton.SetActive(false);
            qrCodesManager.StopQRTracking();
            Debug.Log("Disabled QR_Code Tracking");
        }
        _QRTrackingEnabled = active;
    }

    // Reads dynamically the qr codes and places the virutal button 
    private void updateQRtracking()
    {
        if (qrCodesManager != null)
        {
            node = null;
            code = null;
            qrCodesList = qrCodesManager.GetList();

            code = TryGetQrCode(qrCodesList, "Tool", true); //righ handed qr
            if (code != null)
            {
                if (lastTimeStamp != code.SystemRelativeLastDetectedTime.Ticks && qrState != 2 )
                {
                    node = SpatialGraphNode.FromStaticNodeId(code.SpatialGraphNodeId);
                    inv = false;

                    lastTimeStamp = code.SystemRelativeLastDetectedTime.Ticks;
                    aux = 0;
                    qrState = 1;    // QR reading 
                    toolReading = true;
                }
                else
                {
                    aux = aux + 1;
                    if( aux <= auxStop && qrState != 2)
                    {
                        // threshold for the reading of the qr because it has glithching
                        qrState = 1;
                        toolReading = true;
                    }
                    else
                    {
                        // No codes reading
                        qrState = 0;
                        toolReading = false;
                    }
                }
            }

            code = TryGetQrCode(qrCodesList, "Toolinv", true); //left handed qr (inverse)
            if ( code != null)
            {
                if (lastTimeStampInv != code.SystemRelativeLastDetectedTime.Ticks && qrState != 1 )
                {
                    node = SpatialGraphNode.FromStaticNodeId(code.SpatialGraphNodeId);
                    inv = true;

                    lastTimeStampInv = code.SystemRelativeLastDetectedTime.Ticks;
                    auxInv = 0;
                    qrState = 2;    // inverse QR reading
                    toolInvReading = true;
                }
                else
                {
                    auxInv = auxInv + 1;
                    if (auxInv <= auxStop && qrState != 1 )
                    {
                        // threshold for the reading of the qr because it has glithching
                        qrState = 2;
                        toolInvReading = true;
                    }
                    else
                    {
                        //no codes reading
                        qrState = 0;
                        toolInvReading = false;
                    }
                }
            }

            if( !toolReading && !toolInvReading)
            {
                targetPlacingButton.SetActive(false);
            }

            //Place the node and setting active the virtual button
            if (node != null && node.TryLocate(FrameTime.OnUpdate, out Pose pose))
            {
                Matrix4x4 m = Matrix4x4.LookAt(pose.position, pose.position + Vector3.up, pose.right);
                //Vector3 offset = new Vector3(x, y, z);     //new Vector3(0.2f, 0.1f, -0.05f);

                QRTargetPlacingButton.transform.SetPositionAndRotation(pose.position, pose.rotation); //set to last qr code location
                if (inv)
                {
                    targetPlacingButton.transform.SetPositionAndRotation(invOffsetTargetPlacingButton.transform.position, invOffsetTargetPlacingButton.transform.rotation);
                    targetPlacingButton.SetActive(true);
                }
                else
                {
                    targetPlacingButton.transform.SetPositionAndRotation(offsetTargetPlacingButton.transform.position, offsetTargetPlacingButton.transform.rotation);
                    targetPlacingButton.SetActive(true);
                }

                //targetPlacingButton.transform.up = -pose.up;
                auxPosePos = pose.position;
                auxPoseRot = pose.rotation;
            }
        }
        else
        {
            Debug.Log("Could not place");
            qrCodesList.Clear();
        }
    }

    // Addin the target with DeployTarget but calculating here the offset in the coordinate system of the QR
    /*public void addTargetWithTool()
    {
        GameObject activePiece = GameObject.FindGameObjectWithTag("WeldPiece");
        if( activePiece != null)
        {
            offsetTargetHelper.transform.SetParent(activePiece.transform.GetChild(3));
            offsetTargetHelper.transform.SetPositionAndRotation(auxPosePos, auxPoseRot);

            if (inv)
            {
                targetHelper.transform.SetPositionAndRotation(invOffsetTarget.transform.position, invOffsetTarget.transform.rotation);
                deploy.GetComponent<DeployTargets>().addTargetRealTool(targetHelper.transform.position, targetHelper.transform.rotation * Quaternion.Euler(0, 90, 90));
            }
            else
            {
                targetHelper.transform.SetPositionAndRotation(offsetTarget.transform.position, offsetTarget.transform.rotation);
                deploy.GetComponent<DeployTargets>().addTargetRealTool(targetHelper.transform.position, targetHelper.transform.rotation * Quaternion.Euler(0, -90, 90));
            }
        }        
    }

    //Pacing the pointer on the QR
    private bool TryPlacePointer(System.Collections.Generic.IList<Microsoft.MixedReality.QR.QRCode> list, string PointerNameEnd, Transform Pointer)
    {
        bool result = false;
        Microsoft.MixedReality.QR.QRCode code = TryGetQrCode(list, PointerNameEnd, true);
        if ((code != null))
        {
            SpatialGraphNode node = SpatialGraphNode.FromStaticNodeId(code.SpatialGraphNodeId);
            if (node != null && node.TryLocate(FrameTime.OnUpdate, out Pose pose))
            {
                if (Pointer != null)
                {
                    //Debug.Log("Placed: " + Pointer.name);
                    Pointer.SetPositionAndRotation(pose.position, pose.rotation); //set to last qr code location
                    result = true;
                }
            }
        }
        return result;
    }*/

    private Microsoft.MixedReality.QR.QRCode TryGetQrCode(System.Collections.Generic.IList<Microsoft.MixedReality.QR.QRCode> list, string TestString, bool EndsWith = false)
    {
        DateTimeOffset timeStampLast = new DateTimeOffset();
        Microsoft.MixedReality.QR.QRCode result = null;
        foreach (Microsoft.MixedReality.QR.QRCode code in list)
        {
            string[] stSplit = code.Data.Split('_');
            string stringPart = code.Data.Trim();
            if (stSplit.Length > 1)
                stringPart = EndsWith ? stSplit[stSplit.Length - 1].Trim() : stSplit[0].Trim();
            if (TestString.ToLower() == stringPart.ToLower())
            {
                //  PrintText("timeStampLast " + timeStampLast.Ticks.ToString());
                DateTimeOffset timeStamp = code.LastDetectedTime;
                //  PrintText("timeStamp " + timeStamp.Ticks.ToString());
                if (timeStamp.Ticks > timeStampLast.Ticks)
                {
                    result = code;
                    timeStampLast = timeStamp;
                }
            }
        }
        return result;
    }

    private Microsoft.MixedReality.QR.QRCode TryGetQrCode(System.Collections.Generic.IList<Microsoft.MixedReality.QR.QRCode> list, string String1, string String2)
    {
        DateTimeOffset timeStampLast = new DateTimeOffset();
        Microsoft.MixedReality.QR.QRCode result = null;
        foreach (Microsoft.MixedReality.QR.QRCode code in list)
        {
            string[] stSplit = code.Data.Split('_');
            if ((stSplit.Length > 1) && (stSplit[0].ToLower() == String1.ToLower()) && (stSplit[1].ToLower() == String2.ToLower()))
            {
                DateTimeOffset timeStamp = code.LastDetectedTime;
                if (timeStamp.Ticks > timeStampLast.Ticks)
                {
                    result = code;
                    timeStampLast = timeStamp;
                    //Debug.Log("Found " + code.Data);
                }
            }
        }
        return result;
    }
}
