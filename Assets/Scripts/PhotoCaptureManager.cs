using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using HoloToolkit.Unity;
using UnityEngine;
using UnityEngine.VR.WSA.WebCam;

namespace Assets.Scripts
{
    [SuppressMessage("ReSharper", "PossibleLossOfFraction")]
    public class PhotoCaptureManager : Singleton<PhotoCaptureManager>
    {
        System.Threading.Timer _timer;
        private PhotoCapture _capturedPhotoObject;
        private Texture2D _targetTexture;
        private string _filePath;

        public UserOutputManager UserOutputManager;

        public GameObject WhiteBoard;

        private void Start()
        {
            int secondsInterval = 10;
            _timer = new System.Threading.Timer(Tick, null, 0, secondsInterval * 1000);
        }

        private void Tick(object state)
        {
            if (WhiteBoardFrameBehaviour.Instance.PositionIsGoodForSending)
            {
                CapturePhoto();
            }
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

                ShowText(string.Format("Uploading photo to {0}", ParametrizedImageUploadUrl));

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

                //_httpRequestService.Post(PhotoUploadUrl, picture);

                StartCoroutine(UploadPhoto(picture));

                ShowText(string.Format("Photo uploaded to {0}", ParametrizedImageUploadUrl));
                _capturedPhotoObject.StopPhotoModeAsync(OnPhotoModeStopped);
            }
        }

        private IEnumerator UploadPhoto(byte[] picture)
        {
            var www = new WWW(ParametrizedImageUploadUrl, picture);

            yield return www;
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

        private static string ParametrizedImageUploadUrl
        {
            get { return string.Format(Resources.Constants.GetImageUrl, Resources.Constants.ApplicationUrl, ConnectionManager.Instance.ParticipantOrder); }
        }
    }
}
