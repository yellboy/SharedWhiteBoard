using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class WhiteBoardBehaviour : MonoBehaviour
    {
        public void GetLastPicture()
        {
            StartCoroutine(GetImageFromUrl());
        }

        private IEnumerator GetImageFromUrl()
        {
            var www = new WWW(string.Format(Resources.Constants.GetImageUrl, Resources.Constants.ApplicationUrl, ConnectionManager.Instance.ParticipantOrder));
            yield return www;

            gameObject.GetComponent<MeshRenderer>().material.mainTexture = www.texture;
        }
    }
}
