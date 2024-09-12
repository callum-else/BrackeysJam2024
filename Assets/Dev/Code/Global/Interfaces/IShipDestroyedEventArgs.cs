using UnityEngine;

public interface IShipDestroyedEventArgs
{
    Vector3 Location { get; set; }
    int Value { get; set; }
}
