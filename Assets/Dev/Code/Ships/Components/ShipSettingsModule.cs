using UnityEngine;

namespace Assets.Ships
{
    public class ShipSettingsModule : MonoBehaviour, IShipSettingsModule
    {
        [SerializeField] private ShipSettings settings;
        public IShipSettings Settings => settings;
    }
}