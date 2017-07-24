using System.Collections;
using Assets.Enums;
using HoloToolkit.Unity;
using UnityEngine;

namespace Assets.Scripts
{
    public class ConnectionManager : Singleton<ConnectionManager>
    {
        public int? Pin { get; private set; }

        public GameObject UserInputObjects;
        public UserOutputManager UserOutputManager;

        public ParticipantOrder ParticipantOrder { get; private set; }

        public bool Connected
        {
            get { return Pin.HasValue; }
        }

        void Start()
        {
            SwitchInputObjectsActiveState(false);
        }

        public void Connect()
        {
            SwitchInputObjectsActiveState(true);
        }

        public void StartSession()
        {
            StartCoroutine(GetPin());
        }

        private IEnumerator GetPin()
        {
            var www = new WWW(string.Format(Resources.Constants.StartSessionUrl, Resources.Constants.ApplicationUrl));

            yield return www;

            Pin = int.Parse(www.text);

            SwitchInputObjectsActiveState(false);
            ShowPin();

            ParticipantOrder = ParticipantOrder.A;
        }

        public void ConnectWithPin(int pin)
        {
            Pin = pin;
            StartCoroutine(ConnectWithPin());
        }

        private IEnumerator ConnectWithPin()
        {
            var www = new WWW(string.Format(Resources.Constants.ConnectToExistingSessionUrl, Resources.Constants.ApplicationUrl, Pin));

            yield return www;

            SwitchInputObjectsActiveState(false);
            UserOutputManager.ShowOutput("Connected");

            ParticipantOrder = ParticipantOrder.B;
        }

        public void ShowPin()
        {
            UserOutputManager.ShowOutput(Pin.HasValue ? string.Format("Session pin is {0}", Pin) : "No pin received");
        }

        private void SwitchInputObjectsActiveState(bool activeState)
        {
            foreach (Transform child in UserInputObjects.transform)
            {
                child.gameObject.SetActive(activeState);
            }
        }
    }
}
