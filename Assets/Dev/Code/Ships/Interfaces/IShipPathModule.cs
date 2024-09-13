using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Ships
{
    public interface IShipPathModule
    {
        List<Vector3> Path { get; }
        UnityEvent PathStartedEvent { get; }
        Vector3? GetNext();
    }
}