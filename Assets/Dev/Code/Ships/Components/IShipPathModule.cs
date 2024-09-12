using System.Collections.Generic;
using UnityEngine;

namespace Assets.Ships
{
    public interface IShipPathModule
    {
        List<Vector3> Path { get; }
    }
}