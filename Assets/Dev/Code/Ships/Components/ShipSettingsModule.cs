using UnityEngine;

public class ShipSettingsModule : MonoBehaviour, IShipSettingsModule
{
    [SerializeField] private ShipSettings settings;
    public IShipSettings Settings => settings;
}
