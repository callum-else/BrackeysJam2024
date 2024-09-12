using Assets.Common;
using UnityEngine;

namespace Assets.PlayerInteraction
{
    public interface IRecievePrimaryInteraction : IComponent
    {
        void OnInteractionStart();
        void GetMouseWorldPosition(Vector3 position);
    }
}