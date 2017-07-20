using HoloToolkit.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class WhiteBoardFrameSizeManager : Singleton<WhiteBoardFrameSizeManager>
    {
        private const double WhiteBoardFrameOffset = 0.02f;

        public GameObject WhiteBoardFrame;

        public GameObject WidthInputObjects;
        public GameObject HeightInputObjects;

        public InputField HeightInput;
        public InputField WidthInput;

        private void Start()
        {
            SwitchInputObjectsActiveState(WidthInputObjects, false);
            SwitchInputObjectsActiveState(HeightInputObjects, false);
        }

        private void SetWidth(float width = 0.75f)
        {
            var currentScale = WhiteBoardFrame.transform.localScale;
            WhiteBoardFrame.transform.localScale = new Vector3((float) (width + WhiteBoardFrameOffset), currentScale.y, currentScale.z);
        }

        private void SetHeight(float height = 1.15f)
        {
            var currentScale = WhiteBoardFrame.transform.localScale;
            WhiteBoardFrame.transform.localScale = new Vector3(currentScale.x, (float)(height + WhiteBoardFrameOffset), currentScale.z);
        }

        public void StartResizing()
        {
            SwitchInputObjectsActiveState(WidthInputObjects, true);
        }

        private void SwitchInputObjectsActiveState(GameObject inputObjects, bool activeState)
        {
            foreach (Transform child in inputObjects.transform)
            {
                child.gameObject.SetActive(activeState);
            }
        }

        public void WidthEntered()
        {
            var text = WidthInput.text;
            if (text != string.Empty)
            {
                var widthInCentimeters = float.Parse(text);
                SetWidth(widthInCentimeters / 100);
            }
            else
            {
                SetDefaultWidth();
            }

            SwitchInputObjectsActiveState(WidthInputObjects, false);
            SwitchInputObjectsActiveState(HeightInputObjects, true);
        }

        private void SetDefaultWidth()
        {
            SetWidth();
        }

        public void HeightEntered()
        {
            var text = HeightInput.text;
            if (text != string.Empty)
            {
                var heightInCentimeters = float.Parse(text);
                SetHeight(heightInCentimeters / 100);
            }
            else
            {
                SetDefaultHeight();
            }

            SwitchInputObjectsActiveState(HeightInputObjects, false);
        }

        private void SetDefaultHeight()
        {
            SetHeight();
        }
    }
}
