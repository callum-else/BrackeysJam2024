using Assets.Global;
using UnityEngine;

namespace Assets.Ships
{
    public class ShipNavigationModule : MonoBehaviour
    {
        private IShipPathModule pathModule;
        private IGlobalEventProcessorModule globalEventProcessor;
        private Vector3? harborPos;

        private void Awake()
        {
            pathModule = GetComponent<IShipPathModule>();
            globalEventProcessor = GetComponent<IGlobalEventProcessorModule>();
        }

        private void OnEnable() =>
            globalEventProcessor.HarborPositionEvent.AddListener(CalculateClosestHarbor);

        private void OnDisable() =>
            globalEventProcessor.HarborPositionEvent.RemoveListener(CalculateClosestHarbor);

        private void CalculateClosestHarbor(Vector3 pos)
        {
            if (harborPos.HasValue && Vector3.Distance(harborPos.Value, transform.position) < Vector3.Distance(pos, transform.position))
                return;

            harborPos = pos;
        }
    }
}