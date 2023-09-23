using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.Windows.WebCam;

public class PhotoCaptureExample : MonoBehaviour
{
    PhotoCapture photoCaptureObject = null;
    Texture2D targetTexture = null;

    // Use this for initialization
    void Start()
    {
        PhotoCapture.CreateAsync(false, OnPhotoCaptureCreated);
    }

    void OnPhotoCaptureCreated(PhotoCapture captureObject)
    {
        photoCaptureObject = captureObject;

        Resolution cameraResolution = PhotoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();
        targetTexture = new Texture2D(cameraResolution.width, cameraResolution.height);

        CameraParameters c = new CameraParameters();
        c.hologramOpacity = 0.0f;
        c.cameraResolutionWidth = cameraResolution.width;
        c.cameraResolutionHeight = cameraResolution.height;
        c.pixelFormat = CapturePixelFormat.BGRA32;

        // Create a PhotoCapture object
        PhotoCapture.CreateAsync(false, delegate (PhotoCapture captureObject) {
            // Activate the camera
            photoCaptureObject.StartPhotoModeAsync(c, delegate (PhotoCapture.PhotoCaptureResult result) {
                // Take a picture
                photoCaptureObject.TakePhotoAsync(OnCapturedPhotoToMemory); ;
            });
        });

    }

    void OnStoppedPhotoMode(PhotoCapture.PhotoCaptureResult result)
    {
        photoCaptureObject.Dispose();
        photoCaptureObject = null;
    }


    void OnCapturedPhotoToMemory(PhotoCapture.PhotoCaptureResult result, PhotoCaptureFrame photoCaptureFrame)
    {
        if (result.success)
        {
            // Create our Texture2D for use and set the correct resolution
            Resolution cameraResolution = PhotoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();
            Texture2D targetTexture = new Texture2D(cameraResolution.width, cameraResolution.height);
            // Copy the raw image data into our target texture
            photoCaptureFrame.UploadImageDataToTexture(targetTexture);
            // Do as we wish with the texture such as apply it to a material, etc.

            if (photoCaptureFrame.hasLocationData)
            {
                photoCaptureFrame.TryGetCameraToWorldMatrix(out Matrix4x4 cameraToWorldMatrix);

                Vector3 position = cameraToWorldMatrix.GetColumn(3) - cameraToWorldMatrix.GetColumn(2);
                Quaternion rotation = Quaternion.LookRotation(-cameraToWorldMatrix.GetColumn(2), cameraToWorldMatrix.GetColumn(1));

                photoCaptureFrame.TryGetProjectionMatrix(Camera.main.nearClipPlane, Camera.main.farClipPlane, out Matrix4x4 projectionMatrix);
            }
        }
        // Clean up
        photoCaptureObject.StopPhotoModeAsync(OnStoppedPhotoMode);
    }

}
