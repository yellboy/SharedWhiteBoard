using Assets.Interfaces;
using HoloToolkit.Unity;
using UnityEngine;

namespace Assets.Scripts
{
    public class ConnectionManager : Singleton<ConnectionManager>
    {
        private const string StartSessionUrl = "start";
        private const string ConnectWithPinUrl = "pin{0}";

        private IHttpRequestService _httpRequestService;
        private int? _pin;

        public GameObject UserInputObjects;
        public UserOutputManager UserOutputManager;

        protected override void Awake()
        {
            base.Awake();

            _httpRequestService = Registration.Instance.Resolve<IHttpRequestService>();
        }

        //void Start()
        //{
        //    SwitchInputObjectsActiveState(false);
        //}

        public void Connect()
        {
            SwitchInputObjectsActiveState(true);
        }

        public void StartSession()
        {
            //var pinAsString = _httpRequestService.GetStringResult(StartSessionUrl);
            //_pin = int.Parse(pinAsString);
            _pin = 111111;
            SwitchInputObjectsActiveState(false);
            ShowPin();
        }

        public void ConnectWithPin(int pin)
        {
            _pin = pin;
            //_httpRequestService.GetStringResult(string.Format(ConnectWithPinUrl, _pin));
            SwitchInputObjectsActiveState(false);
            UserOutputManager.ShowOutput("Connected");
        }

        public void ShowPin()
        {
            UserOutputManager.ShowOutput(_pin.HasValue ? string.Format("Session pin is {0}", _pin) : "No pin received");
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
