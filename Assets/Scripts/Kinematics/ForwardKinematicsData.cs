// <copyright file="Mechanism.cs" company="ABB">
// Copyright (c) ABB. All rights reserved.
// </copyright>
namespace RK
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityGLTF;
    using UnityEngine;
    using System.Xml.Linq;
    using System;
    using RobotStudio.API.Internal;
    using ABB.Robotics.Math;
    using UnityEngine.UI;

    [Serializable]
    public class ForwardKinematicsData
    {
        public string Name { get; set; }
        public string Data { get; set; }
    }
}
