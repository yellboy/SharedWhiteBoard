using System.Collections;
using HoloToolkit.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class UserOutputManager : Singleton<UserOutputManager>
    {
        public Text OutputText;

        public void ShowOutput(string output)
        {
            StartCoroutine(DisplayText(output));
        }

        private IEnumerator DisplayText(string text)
        {
            OutputText.text = text;

            OutputText.GetComponent<FadingObject>().FadeIn(2.5f);
            yield return new WaitForSeconds(7.5f);
            OutputText.GetComponent<FadingObject>().FadeOut(2.5f);
            yield return new WaitForSeconds(2.5f);

            OutputText.text = string.Empty;
        }
    }
}
