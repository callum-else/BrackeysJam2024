using UnityEngine;

namespace Assets.Ships
{
    public interface IShipPoolReferences
    {
        GameObject HexaShipPrefab { get; }
        GameObject IcosaShipPrefab { get; }
        GameObject OctaShipPrefab { get; }
        GameObject TetraShipPrefab { get; }
    }
}