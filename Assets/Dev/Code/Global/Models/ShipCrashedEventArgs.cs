using UnityEngine;

namespace Assets.Global
{
    public class ShipCrashedEventArgs : IShipCrashedEventArgs
    {
        public Vector3 Location { get; set; }
        public int Value { get; set; }
    }
}