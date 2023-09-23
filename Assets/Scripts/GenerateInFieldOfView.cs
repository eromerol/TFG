using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;

public class GenerateInFieldOfView : MonoBehaviour
{
    void Start()
    {
        // Obtén la posición de la cámara principal (cámara de las Hololens)
        Vector3 cameraPosition = CameraCache.Main.transform.position;

        // Obtén la dirección de la cámara principal (mirando hacia adelante)
        Vector3 cameraDirection = CameraCache.Main.transform.forward;

        // Defina una distancia de visualización deseada para el menú (puede ajustarla según sus necesidades)
        float desiredDistance = 0.8f;

        // Calcule la posición del menú en función de la posición y dirección de la cámara
        transform.position = cameraPosition + (cameraDirection * desiredDistance);
    }

}

