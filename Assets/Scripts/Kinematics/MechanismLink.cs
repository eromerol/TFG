// <copyright file="MechanismLinkSelectable.cs" company="ABB">
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

    public class MechanismLink : MonoBehaviour
    {
        private Color fillColor = new Color(44.0f / 255.0f, 32.0f / 255.0f, 212.0f / 255.0f, 50.0f / 255.0f);

        private Color outlineColor = new Color(88.0f / 255.0f, 32.0f / 255.0f, 212.0f / 255.0f, 168.0f / 255.0f);

        private Color highlightColor = new Color(109.0f / 255.0f, 81.0f / 255.0f, 166.0f / 197.0f, 133.0f / 255.0f);

        private UnityEngine.Vector3 boundsExtent = new UnityEngine.Vector3(8.0f, 8.0f, 8.0f);

        public void SetOutline()
        {
            this.HighlightOn();
        }

        public void HideOutline()
        {
            this.HighlightOff();
        }

        private (Renderer r, Color[] c)[] originalColors = null;

        [SerializeField]
        private float ENLIGHTEN_FACTOR_R = 0.85f;

        [SerializeField]
        private float ENLIGHTEN_FACTOR_G = 0.8f;

        [SerializeField]
        private float ENLIGHTEN_FACTOR_B = 1.2f;

        private void HighlightOn()
        {
            var renderers = this.GetComponentsInChildren<Renderer>(true);
            this.originalColors = new (Renderer r, Color[] c)[renderers.Length];

            for (int i = 0; i < renderers.Length; i++)
            {
                var renderer = renderers[i];
                var colors = new Color[renderer.materials.Length];

                for (int j = 0; j < renderer.materials.Length; j++)
                {
                    colors[j] = renderer.materials[j].GetColor("_Color");

                    var newColor = new Color(
                        colors[j].r * ENLIGHTEN_FACTOR_R,
                        colors[j].g * ENLIGHTEN_FACTOR_G,
                        colors[j].b * ENLIGHTEN_FACTOR_B
                    );

                    renderer.materials[j].SetColor("_Color", newColor);
                }

                this.originalColors[i] = (renderer, colors);
            }
        }

        private void HighlightOff()
        {
            if (this.originalColors == null)
            {
                return;
            }

            var renderers = this.GetComponentsInChildren<Renderer>(true);

            for (int i = 0; i < renderers.Length; i++)
            {
                var renderer = renderers[i];

                for (int j = 0; j < renderer.materials.Length; j++)
                {
                    renderer.materials[j].SetColor("_Color", this.originalColors[i].c[j]);
                }
            }

            this.originalColors = null;
        }

    }
}
