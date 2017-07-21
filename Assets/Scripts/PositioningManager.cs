using HoloToolkit.Unity;
using HoloToolkit.Unity.SpatialMapping;
using UnityEngine;

namespace Assets.Scripts
{
    public class PositioningManager : Singleton<PositioningManager>
    {
        public GameObject WhiteBoard;

        public bool PositioningInProgress { get; private set; }

        private void Start()
        {
            PositioningInProgress = true;
        }

        public void Done()
        {
            SwitchPositioningEnabled(false);
            PositioningInProgress = false;
        }

        public void Position()
        {
            SwitchPositioningEnabled(true);
            PositioningInProgress = true;
        }

        private void SwitchPositioningEnabled(bool positioningEnabled)
        {
            SpatialMappingManager.Instance.gameObject.SetActive(positioningEnabled);
            SpatialUnderstanding.Instance.gameObject.SetActive(positioningEnabled);

            var tapToPlace = WhiteBoard.GetComponent<TapToPlace>();
            if (tapToPlace != null)
            {
                tapToPlace.enabled = positioningEnabled;
            }
        }
    }
}
