using HoloToolkit.Unity;
using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class WhiteBoardFrameBehaviour : Singleton<WhiteBoardFrameBehaviour> //MonoBehaviour
    {
        private const float MinimumDistance = 1.0f;
        private const float MaximumDistance = 2f;

        public Material RedMaterial;

        public Material GreenMaterial;

        // Use this for initialization
        void Start ()
        {
            gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
        }
	
        // Update is called once per frame
        void Update ()
        {
            gameObject.GetComponent<MeshRenderer>().material = PositionIsGoodForSending ? GreenMaterial : RedMaterial;
        }

        public bool PositionIsGoodForSending
        {
            get { return RotationIsSimilarToMainCamera && DistanceIsGood; }
        }

        private bool DistanceIsGood
        {
            get
            {
                var distance = Vector3.Distance(gameObject.transform.position, Camera.main.transform.position);
                return distance >= MinimumDistance && distance <= MaximumDistance;
            }
        }

        private bool RotationIsSimilarToMainCamera
        {
            get
            {
                return Math.Abs(Camera.main.transform.rotation.x - gameObject.transform.rotation.x) < 0.05 &&
                       Math.Abs(Camera.main.transform.rotation.y - gameObject.transform.rotation.y) < 0.05 &&
                       Math.Abs(Camera.main.transform.rotation.z - gameObject.transform.rotation.z) < 0.05;
            }
        }

        public void UpdateSent()
        {
            
        }
    }
}
