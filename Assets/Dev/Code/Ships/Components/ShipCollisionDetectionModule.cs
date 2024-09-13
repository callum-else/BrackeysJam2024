using UnityEngine;
using UnityEngine.Events;

namespace Assets.Ships
{
    public class ShipCollisionDetectionModule : MonoBehaviour, IShipCollisionDetectionModule
    {
        private UnityEvent<ICollisionModule> onCollision;
        public UnityEvent<ICollisionModule> OnCollision => onCollision ??= new();

        private void OnCollisionEnter(Collision c)
        {
            if (!c.collider.TryGetComponent<ICollisionModule>(out var col))
                return;

            OnCollision.Invoke(col);
        }
    }
}