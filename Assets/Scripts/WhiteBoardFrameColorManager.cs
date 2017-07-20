using System;
using System.Collections;
using HoloToolkit.Unity;
using UnityEngine;

namespace Assets.Scripts
{
    public class WhiteBoardFrameColorManager : Singleton<WhiteBoardFrameColorManager>
    {
        public Material RedMaterial;
        public Material BlueMaterial;
        public Material GreenMaterial;

        public GameObject WhiteBoardFrame;

        private const float MinimumDistance = 1.0f;
        private const float MaximumDistance = 2f;
        private const float RotationDeviationTolerance = 0.03f;

        private bool _changingFrameColorLocked;

        void Update()
        {
            if (!_changingFrameColorLocked)
            {
                ChangeFrameMaterial(PositionIsGoodForSending ? GreenMaterial : RedMaterial);
            }
        }

        private bool PositionIsGoodForSending
        {
            get
            {
                return RotationIsSimilarToMainCamera && DistanceIsGood;
            }
        }

        private bool DistanceIsGood
        {
            get
            {
                var distance = Vector3.Distance(WhiteBoardFrame.transform.position, Camera.main.transform.position);
                return distance >= MinimumDistance && distance <= MaximumDistance;
            }
        }

        private bool RotationIsSimilarToMainCamera
        {
            get
            {
                return Math.Abs(Camera.main.transform.rotation.x - WhiteBoardFrame.transform.rotation.x) < RotationDeviationTolerance &&
                       Math.Abs(Camera.main.transform.rotation.y - WhiteBoardFrame.transform.rotation.y) < RotationDeviationTolerance &&
                       Math.Abs(Camera.main.transform.rotation.z - WhiteBoardFrame.transform.rotation.z) < RotationDeviationTolerance;
            }
        }

        public void UpdateSent()
        {
            StartCoroutine(ShowBlueFrame());
        }

        public IEnumerator ShowBlueFrame()
        {
            _changingFrameColorLocked = true;
            ChangeFrameMaterial(BlueMaterial);
            yield return new WaitForSeconds(5f);
            _changingFrameColorLocked = false;
        }

        private void ChangeFrameMaterial(Material frameMaterial)
        {
            WhiteBoardFrame.GetComponent<MeshRenderer>().material = frameMaterial;
        }
    }
}
