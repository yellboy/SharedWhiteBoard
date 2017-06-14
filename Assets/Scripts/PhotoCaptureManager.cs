using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Assets.Interfaces;
using HoloToolkit.Unity;
using UnityEngine;
using UnityEngine.VR.WSA.WebCam;

namespace Assets.Scripts
{
    [SuppressMessage("ReSharper", "PossibleLossOfFraction")]
    public class PhotoCaptureManager : Singleton<PhotoCaptureManager>
    {
        private const string PhotoUploadUrl =
            @"https://sharedwhiteboard.azurewebsites.net/api/HttpTriggerCSharp1?code=/ETH9aWwijjNKCw6De9orgOkyV4r0dS3Wlk3Vs8qwyavJa//VUSzgQ==";
        private PhotoCapture _capturedPhotoObject;
        private Texture2D _targetTexture;
        private string _filePath;
        private IHttpRequestService _httpRequestService;

        public UserOutputManager UserOutputManager;

        public GameObject WhiteBoard;

        protected override void Awake()
        {
            base.Awake();

            _httpRequestService = Registration.Instance.Resolve<IHttpRequestService>();
        }

        public void CapturePhoto()
        {
            PhotoCapture.CreateAsync(false, OnPhotoCaptureCreated);
        }

        private void OnPhotoCaptureCreated(PhotoCapture captureobject)
        {
            //ShowText("Photo Capture Created");

            _capturedPhotoObject = captureobject;
            var cameraResolution = PhotoCapture.SupportedResolutions.OrderByDescending(res => res.width * res.height).First();

            _targetTexture = new Texture2D(cameraResolution.width, cameraResolution.height);

            var cameraParameters = new CameraParameters
            {
                hologramOpacity = 0.0f,
                cameraResolutionWidth = cameraResolution.width,
                cameraResolutionHeight = cameraResolution.height,
                pixelFormat = CapturePixelFormat.BGRA32
            };

            captureobject.StartPhotoModeAsync(cameraParameters, OnPhotoModeStarted);
        }

        private void OnPhotoModeStarted(PhotoCapture.PhotoCaptureResult result)
        {
            if (result.success)
            {
                var fileName = string.Format("PhotoCapture_{0}.jpg", Time.time);
                _filePath = System.IO.Path.Combine(Application.persistentDataPath, fileName);

                //ShowText(string.Format("Uploading photo to {0}", PhotoUploadUrl));

                _capturedPhotoObject.TakePhotoAsync(OnPhotoCapturedToMemory);
                //_capturedPhotoObject.TakePhotoAsync(_filePath, PhotoCaptureFileOutputFormat.JPG, OnCapturedToDisk);
            }
        }

        private void OnPhotoCapturedToMemory(PhotoCapture.PhotoCaptureResult result, PhotoCaptureFrame photoCaptureFrame)
        {
            if (result.success)
            {
                photoCaptureFrame.UploadImageDataToTexture(_targetTexture);

                var picture = _targetTexture.EncodeToJPG();

                _httpRequestService.Post(PhotoUploadUrl, picture);

                ShowText(string.Format("Photo uploaded to {0}", PhotoUploadUrl));
                _capturedPhotoObject.StopPhotoModeAsync(OnPhotoModeStopped);
            }
        }

        private void OnCapturedToDisk(PhotoCapture.PhotoCaptureResult result)
        {
            if (result.success)
            {
                ShowText(string.Format("Photo saved to {0}", _filePath));
                _capturedPhotoObject.StopPhotoModeAsync(OnPhotoModeStopped);
            }
        }

        private void OnPhotoModeStopped(PhotoCapture.PhotoCaptureResult result)
        {
            _capturedPhotoObject.Dispose();
            _capturedPhotoObject = null;
        }

        private void ShowText(string text)
        {
            UserOutputManager.ShowOutput(text);
        }
    }
}
