using System;
using System.Collections;
using Assets.Interfaces;
using UnityEngine;

namespace Assets.Services
{
    public class HttpRequestService : IHttpRequestService
    {
        public void Post(string url, byte[] postData)
        {
            new WWW(url, postData);
        }

        public string GetStringResult(string url)
        {
            var www = new WWW(url);

            return www.text;
        }

        public IEnumerator GetImageResult(string url, Action<object> callback)
        {
            var www = new WWW(url);

            yield return www;

            var texture = new Texture2D(512, 512, TextureFormat.DXT1, false);
            www.LoadImageIntoTexture(texture);

            callback(texture);
        }
    }
}
