using Assets.Global;
using UnityEngine;

namespace Assets.Ships
{
    public class ShipEventProcessorModule : MonoBehaviour
    {
        private IShipCollisionDetectionModule cdm;
        private IGlobalEventProcessorModule gepm;
        private IShipSettingsModule sm;

        private void Awake()
        {
            cdm = GetComponentInChildren<IShipCollisionDetectionModule>();
            gepm = GetComponent<IGlobalEventProcessorModule>();
            sm = GetComponent<IShipSettingsModule>();
        }

        private void OnEnable() =>
            cdm.OnCollision.AddListener(HandleCollision);

        private void OnDisable() =>
            cdm.OnCollision.RemoveListener(HandleCollision);

        private void HandleCollision(ICollisionModule collision)
        {
            switch (collision.Type)
            {
                case ColliderType.Ship:
                    gepm.OnShipCrashed.Invoke(new ShipCrashedEventArgs()
                    {
                        Location = transform.position + (collision.transform.position - transform.position) * 0.5f,
                        Value = sm.Settings.Value
                    });
                    DisposeSelf();
                    break;

                case ColliderType.Harbor:
                    gepm.OnShipSaved.Invoke(new ShipSavedEventArgs()
                    {
                        Value = sm.Settings.Value
                    });
                    DisposeSelf();
                    break;

                case ColliderType.Glitch:
                    gepm.OnShipGlitched.Invoke(new ShipGlitchedEventArgs()
                    {
                        InstanceID = collision.GetInstanceID(),
                        Value = sm.Settings.Value
                    });
                    DisposeSelf();
                    break;
            }
        }

        private void DisposeSelf()
        {
            Destroy(gameObject);
        }
    }
}