using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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
            var url = string.Format(Resources.Constants.GetImageUrl, Resources.Constants.ApplicationUrl, ConnectionManager.Instance.ParticipantOrder);

            var www = new WWW(url);

            yield return www;

            gameObject.GetComponent<MeshRenderer>().material.mainTexture = www.texture;
        }

        public void GetDarkAreas()
        {
            StartCoroutine(GetDarkAreasFromUrl());
        }

        private IEnumerator GetDarkAreasFromUrl()
        {
            var url = "localhost:6364/ImageApi/Image/A/DarkAreas";

            var www = new WWW(url);

            yield return www;

            gameObject.GetComponent<MeshRenderer>().material.mainTexture = www.texture;
        }
    }
}
