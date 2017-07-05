using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class RotationTextBehaviour : MonoBehaviour
    {
        public GameObject WhiteBoard;

        public Text WhiteBoardText;
        public Text CameraText;

        // Update is called once per frame
        void Update ()
        {
            CameraText.text = string.Format("Camera Pos ({0},{1},{2}), Rot ({3},{4},{5})",
                Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z,
                Camera.main.transform.rotation.x, Camera.main.transform.rotation.y, Camera.main.transform.rotation.z);

            WhiteBoardText.text = string.Format("WhiteBoard Pos ({0},{1},{2}), Rot ({3},{4},{5})",
                WhiteBoard.transform.position.x, WhiteBoard.transform.position.y, WhiteBoard.transform.position.z,
                WhiteBoard.transform.rotation.x, WhiteBoard.transform.rotation.y, WhiteBoard.transform.rotation.z);
        }
    }
}
