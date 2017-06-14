using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
// ReSharper disable PossibleLossOfFraction

namespace Assets.Scripts
{
    public class HolographicKeyboardBehavior : MonoBehaviour
    {
        public InputField KeyboardInputField;

        private string _keyboardText;

        protected void Start()
        {
            _keyboardText = string.Empty;
            KeyboardInputField.text = _keyboardText;
        }

        public void NumberButtonClicked(int number)
        {
            _keyboardText += number;
            KeyboardInputField.text = _keyboardText;
        }

        public void BackspaceButtonClicked()
        {
            if (_keyboardText.Length > 0)
            {
                _keyboardText = _keyboardText.Substring(0, _keyboardText.Length - 1);
                KeyboardInputField.text = _keyboardText;
            }
        }

        public void OkButtonClicked()
        {
            if (_keyboardText.Length > 0)
            {
                var pin = int.Parse(_keyboardText);
                ConnectionManager.Instance.ConnectWithPin(pin);
            }
        }
    }
}
