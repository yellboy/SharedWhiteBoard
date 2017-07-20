using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class WhiteBoardBehaviour : MonoBehaviour
    {
        System.Threading.Timer _timer;

        private void Start()
        {
            int secondsInterval = 10;
            _timer = new System.Threading.Timer(Tick, null, 0, secondsInterval * 1000);
        }

        private void Tick(object state)
        {
            GetLastPicture();
        }

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
