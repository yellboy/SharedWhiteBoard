using System;
using System.Collections;
using UnityEngine;

namespace Assets.Interfaces
{
    public interface IHttpRequestService
    {
        void Post(string url, byte[] postData);

        string GetStringResult(string url);

        IEnumerator GetImageResult(string url, Action<object> callback);
    }
}
