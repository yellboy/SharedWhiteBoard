using System.Collections;
using System.Linq.Expressions;
using Assets.Interfaces;
using UnityEngine;
using UnityEngine.Analytics;

namespace Assets.Scripts
{
    public class WhiteBoardBehaviour : MonoBehaviour
    {
        private const string GetLastImageUrl = "http://distributedwhiteboard.azurewebsites.net/ImageApi/Image";

        private IHttpRequestService _httpRequestService;

        private void Awake()
        {
            _httpRequestService = Registration.Instance.Resolve<IHttpRequestService>();
        }

        public void GetLastPicture()
        {
            //StartCoroutine(_httpRequestService.GetImageResult(GetLastImageUrl, ApplyResultToMainTexture));
            StartCoroutine(GetImageFromUrl());

        }

        private IEnumerator GetImageFromUrl()
        {
            var www = new WWW(GetLastImageUrl);

            yield return www;

            gameObject.GetComponent<MeshRenderer>().material.mainTexture = www.texture;
        }

        private void ApplyResultToMainTexture(object result)
        {
            gameObject.GetComponent<MeshRenderer>().material.mainTexture = (Texture2D)result;
        }
    }
}
