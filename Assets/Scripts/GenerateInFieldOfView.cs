using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;

public class GenerateInFieldOfView : MonoBehaviour
{
    void Start()
    {
        // Obt�n la posici�n de la c�mara principal (c�mara de las Hololens)
        Vector3 cameraPosition = CameraCache.Main.transform.position;

        // Obt�n la direcci�n de la c�mara principal (mirando hacia adelante)
        Vector3 cameraDirection = CameraCache.Main.transform.forward;

        // Defina una distancia de visualizaci�n deseada para el men� (puede ajustarla seg�n sus necesidades)
        float desiredDistance = 0.8f;

        // Calcule la posici�n del men� en funci�n de la posici�n y direcci�n de la c�mara
        transform.position = cameraPosition + (cameraDirection * desiredDistance);
    }

}

