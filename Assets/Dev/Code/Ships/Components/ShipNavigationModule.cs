using Assets.Global;
using TreeEditor;
using UnityEngine;

namespace Assets.Ships
{
    public class ShipNavigationModule : MonoBehaviour
    {
        private IShipPathModule pathModule;
        private IGlobalEventProcessorModule globalEventProcessor;
        private Vector3? harborPos;
        private Vector3? pathPos;
        private Vector3 target;
        private Vector3 direction;
        private float speed = 2f;

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

        private Vector3 GetCurrentTarget()
        {
            if (pathModule.Path.Count > 0 && (!pathPos.HasValue || Vector3.Distance(transform.position, pathPos.Value) < 0.25f))
                pathPos = pathModule.GetNext();

            return pathPos ?? harborPos ?? Vector3.zero;
        }

        private void FixedUpdate()
        {
            target = GetCurrentTarget();
            direction = Vector3.Lerp(direction, (target - transform.position).normalized, 0.25f);

            transform.position += direction * Time.fixedDeltaTime * speed;
            transform.LookAt(transform.position + direction);
        }
    }
}