using Assets.Global;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Ships
{
    public class ShipNavigationModule : MonoBehaviour
    {
        private IShipPathModule pm;
        private IShipSettingsModule sm;
        private IGlobalEventProcessorModule gepm;
        private Rigidbody rb;

        private Vector3? harborPos;
        private Vector3? pathPos;
        private Vector3 target;
        private Vector3 direction;

        private void Awake()
        {
            pm = GetComponent<IShipPathModule>();
            sm = GetComponent<IShipSettingsModule>();
            gepm = GetComponent<IGlobalEventProcessorModule>();
            rb = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            gepm.HarborPositionEvent.AddListener(CalculateClosestHarbor);
            pm.PathStartedEvent.AddListener(HandlePathStarted);
        }

        private void OnDisable()
        {
            gepm.HarborPositionEvent.RemoveListener(CalculateClosestHarbor);
            pm.PathStartedEvent.RemoveListener(HandlePathStarted);
        }

        private void HandlePathStarted()
        {
            pathPos = null;
        }

        private void CalculateClosestHarbor(Vector3 pos)
        {
            if (harborPos.HasValue && Vector3.Distance(harborPos.Value, transform.position) < Vector3.Distance(pos, transform.position))
                return;

            harborPos = pos;
        }

        private Vector3 GetCurrentTarget()
        {
            if (pm.Path.Count > 0 && (!pathPos.HasValue || Vector3.Distance(transform.position, pathPos.Value) < 0.25f))
                pathPos = pm.GetNext();

            return pathPos ?? harborPos ?? Vector3.zero;
        }

        private void FixedUpdate()
        {
            target = GetCurrentTarget();
            direction = Vector3.Lerp(direction, (target - transform.position).normalized, 0.25f);

            transform.LookAt(transform.position + direction);
            rb.position += direction * Time.fixedDeltaTime * sm.Settings.Speed;
        }
    }
}