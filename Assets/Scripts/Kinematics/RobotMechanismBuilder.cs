// <copyright file="RobotMechanismBuilder.cs" company="ABB">
// Copyright (c) ABB. All rights reserved.
// </copyright>
namespace RK
{
    using System.Collections.Generic;
    using UnityGLTF;
    using UnityEngine;
    using UnityEngine.UI;
    using System.Collections;
    using Microsoft.MixedReality.Toolkit.UI;
    using System.Threading;
    using System.Threading.Tasks;
    using RobotStudio.Services.RobApi;
    using System;
    //added
    using TMPro;

    public class RobotMechanismBuilder : MonoBehaviour
    {
        public GameObject Tool;
        #region Data
        [SerializeField]
        private GLTFComponent gltf = null;

        [SerializeField]
        private GameObject pointPrefab = null;

        [SerializeField]
        private GameObject highlightQuadPrefab = null;

        private Mechanism selectedMechanism = null;

        private List<Mechanism> allMechanisms = new List<Mechanism>();

        private string taskName = "T_ROB1";
        private bool _Busy = false;
        private int _LoopDelay = 25;
        private int _LoopCount = 0;
        private bool _UpdateJoints = false;
        private Transform GLTFScene; // holds reference to loaded object

        // NEW 
        private GameObject _link6;
        private GameObject _Tool;
        private Matrix4x4 _ToolOffset = Matrix4x4.identity;

        public static bool MechanismLoaded = false;/// Set to true when the load is complete
        private int ModelIndexLast = 0;

        // Sliders value
        [SerializeField] private TextMeshPro slider1;
        [SerializeField] private TextMeshPro slider2;
        [SerializeField] private TextMeshPro slider3;
        [SerializeField] private TextMeshPro slider4;
        [SerializeField] private TextMeshPro slider5;
        [SerializeField] private TextMeshPro slider6;

        // welding gun laser
        [SerializeField] private GameObject laser;

        // collider
        [SerializeField] private GameObject ColliderObject; // this is needed to have a collider in the tool, otherwise it would not be possible to 
                                                            // edit it in editting time

        #endregion

        #region Start and Update
        private void Start()
        {
            this.gltf = this.GetComponent<GLTFComponent>();
            this.gltf.OnKinematicsDataProcessed -= this.OnKinematicsDataProcessed;
            this.gltf.OnKinematicsDataProcessed += this.OnKinematicsDataProcessed;
        }

        private void Update()
        {
            UpdateToolPos();
        }

        #endregion

        private void OnKinematicsDataProcessed(Dictionary<int, (string name, string kinematics, float[] jointValues)> kinematicsData)
        {
            //this.gltf.OnKinematicsDataProcessed -= this.OnKinematicsDataProcessed;

            this.allMechanisms.Clear();

            InstantiatedGLTFObject igltf = this.transform.GetComponentInChildren<InstantiatedGLTFObject>();

            foreach (int kinematicsDataKey in kinematicsData.Keys)
            {
                // "key" is object name to apply mechanism to
                Transform[] childTransforms = igltf.transform.GetComponentsInChildren<Transform>(true);

                var data = kinematicsData[kinematicsDataKey];

                foreach (Transform childTransform in childTransforms)
                {
                    if (childTransform.name == data.name)
                    {
                        var newMechanism = childTransform.gameObject.AddComponent<Mechanism>();
                        newMechanism.BuildMechanismWithData(
                            data.kinematics,
                            data.jointValues,
                            this.pointPrefab,
                            this.highlightQuadPrefab);

                        this.allMechanisms.Add(newMechanism);
                    }
                }
            }

            this.selectedMechanism = this.allMechanisms[0];
            _link6 = GameObject.Find("Link6"); // is it always Link 6
            _Tool = Tool.transform.GetChild(0).GetChild(0).gameObject;  
            _ToolOffset.SetColumn(3, new Vector4(0, 0, 0.09f, 1)); // 100 mm in z



            // welding gun laser
            laser.SetActive(true); // we also have to make it child of the tool
            laser.transform.SetParent(_Tool.transform, false); // false because we want to mantain the local position.
            laser.SetActive(false);

            // collider
            ColliderObject.SetActive(true); // we also have to make it child of the tool
            ColliderObject.transform.SetParent(_Tool.transform, false); // false because we want to mantain the local position.
            ColliderObject.SetActive(false);

            Debug.Log("OnKinematicsDataProcessed end");
            UpdateToolPos();
        }

        public void LoadStation(string filePath)
        {
            gltf.LoadOnCommand(filePath);
            Debug.Log("Model Loaded");
        }

        #region Slider interface
        public void OnSliderValueChangedAxis1(SliderEventData data)
        {
            UpdateMechJointsSlider(0, data.NewValue);
        }
        public void OnSliderValueChangedAxis2(SliderEventData data)
        {
            UpdateMechJointsSlider(1, data.NewValue);
        }
        public void OnSliderValueChangedAxis3(SliderEventData data)
        {
            UpdateMechJointsSlider(2, data.NewValue);
        }
        public void OnSliderValueChangedAxis4(SliderEventData data)
        {
            UpdateMechJointsSlider(3, data.NewValue);
        }
        public void OnSliderValueChangedAxis5(SliderEventData data)
        {
            UpdateMechJointsSlider(4, data.NewValue);
        }
        public void OnSliderValueChangedAxis6(SliderEventData data)
        {
            UpdateMechJointsSlider(5, data.NewValue);
        }
        #endregion

        private void UpdateMechJointsSlider(int AxisIndex, float value)
        {
            if (this.selectedMechanism != null)
            {
                float validatedValue = Mathf.Clamp(value, 0, 1);
                float angle = (float)this.selectedMechanism.minLimits[AxisIndex] + ((float)this.selectedMechanism.maxLimits[AxisIndex] - (float)this.selectedMechanism.minLimits[AxisIndex]) * validatedValue;
                this.selectedMechanism.jointValues[AxisIndex] = angle;
                this.selectedMechanism.CallCalculateTransforms();
                UpdateToolPos();
                int angleDegrees = (int)ToDegrees(angle);

                switch (AxisIndex)
                {
                    case 0: slider1.text = angleDegrees.ToString(); break;
                    case 1: slider2.text = angleDegrees.ToString(); break;
                    case 2: slider3.text = angleDegrees.ToString(); break;
                    case 3: slider4.text = angleDegrees.ToString(); break;
                    case 4: slider5.text = angleDegrees.ToString(); break;
                    case 5: slider6.text = angleDegrees.ToString(); break;
                    default: break;
                }
            }
        }

        public void UpdateMechJoints(int AxisIndex, float value)
        {
            try
            {
                if (this.selectedMechanism != null)
                {
                    this.selectedMechanism.jointValues[AxisIndex] = value;
                    this.selectedMechanism.CallCalculateTransforms();
                    //UpdateToolPos(); 
                }
            }
            catch (Exception ex) { }
        }

        public void UpdateToolPos()
        {
            if ((_link6 != null) && (_Tool != null))
            {
                Quaternion rotation = Quaternion.Euler(0, 0, 180); // extra rotation needed for the tool
                Matrix4x4 mRot = Matrix4x4.Rotate(rotation);

                Matrix4x4 mLink6 = _link6.transform.localToWorldMatrix;
                Matrix4x4 newTransform = mLink6 * _ToolOffset * mRot;
                _Tool.transform.SetPositionAndRotation(new Vector3(newTransform[0, 3], newTransform[1, 3], newTransform[2, 3]), newTransform.rotation); // world space
            }
            //else
            //    Debug.Log("Tool not found");
        }

        private static float ToRadians(float angleInDegree)
        {
            return (angleInDegree * Mathf.PI) / 180.0f;
        }

        private static float ToDegrees(float angleInRadians)
        {
            return (angleInRadians * 180.0f) / Mathf.PI;
        }
        public void ChangeTool(Transform ToolTrans, float Offset)
        {
            _Tool = ToolTrans.gameObject;
            _Tool.SetActive(true);
            _ToolOffset.SetColumn(3, new Vector4(0, 0, Offset, 1)); // 100 mm in z
        }
    }
}