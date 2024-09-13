using Assets.Common;
using System;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

namespace Assets.Ships
{
    public interface IShipPoolObjModule : IComponent
    {
        public void ResetObj();
        public void InjectRelease(Action<IShipPoolObjModule> release);
    }
}