using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class FadingObject : MonoBehaviour
    {
        private bool _fadeIn;
        private bool _fadeOut;
        private float _fadingPerSecond;
        
        private Color _originalColor;

        protected void Start()
        {
            _originalColor = gameObject.GetComponent<Text>().color;
            gameObject.GetComponent<Text>().color = new Color(_originalColor.r, _originalColor.g, _originalColor.b, 0);
        }

        protected void Update()
        {
            var currentColor = gameObject.GetComponent<Text>().color;
            var newColor = currentColor;

            if (_fadeIn)
            {
                newColor = new Color(currentColor.r, currentColor.g, currentColor.b, currentColor.a + _fadingPerSecond * Time.deltaTime);

                if (newColor.a >= _originalColor.a)
                {
                    _fadeIn = false;
                }
            }
            else if (_fadeOut)
            {
                newColor = new Color(currentColor.r, currentColor.g, currentColor.b,
                    currentColor.a - _fadingPerSecond * Time.deltaTime);

                if (newColor.a <= 0)
                {
                    _fadeOut = false;
                }

            }

            gameObject.GetComponent<Text>().color = newColor;
        }

        public void FadeIn(float fadingTimeInSeconds)
        {
            _fadeIn = true;
            _fadeOut = false;

            _fadingPerSecond = _originalColor.a / fadingTimeInSeconds;
        }

        public void FadeOut(float fadingTimeInSeconds)
        {
            _fadeOut = true;
            _fadeIn = false;

            _fadingPerSecond = _originalColor.a / fadingTimeInSeconds;
        }
    }
}
