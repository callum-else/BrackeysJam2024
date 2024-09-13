using UnityEngine.Events;

namespace Assets.Ships
{
    public interface IShipCollisionDetectionModule
    {
        UnityEvent<ICollisionModule> OnCollision { get; }
    }
}