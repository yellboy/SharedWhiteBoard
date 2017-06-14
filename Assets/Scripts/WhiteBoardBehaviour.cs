using System.Collections;
using System.Linq.Expressions;
using Assets.Interfaces;
using UnityEngine;
using UnityEngine.Analytics;

namespace Assets.Scripts
{
    public class WhiteBoardBehaviour : MonoBehaviour
    {
        private const string GetLastImageUrl = "https://docs.unity3d.com/uploads/Main/ShadowIntro.png";

        private IHttpRequestService _httpRequestService;

        private void Awake()
        {
            _httpRequestService = Registration.Instance.Resolve<IHttpRequestService>();
        }

        public void GetLastPicture()
        {
            StartCoroutine(_httpRequestService.GetImageResult(GetLastImageUrl, ApplyResultToMainTexture));

        }

        private void ApplyResultToMainTexture(object result)
        {
            gameObject.GetComponent<MeshRenderer>().material.mainTexture = (Texture2D)result;
        }
    }
}
