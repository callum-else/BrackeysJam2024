using Assets.PlayerInteraction;
using UnityEngine;

namespace Assets.Ships
{
    public class ShipPathModule : MonoBehaviour, IRecievePrimaryInteraction
    {


        public void GetMouseWorldPosition(Vector3 position)
        {
            Debug.Log($"{transform.position} - {position}");
        }
    }
}