using System;
using UnityEngine;

namespace Assets.Ships
{
    public class ShipPoolObjModule : MonoBehaviour, IShipPoolObjModule
    {
        private IShipPathModule pm;
        private Action<IShipPoolObjModule> releaseMethod;

        private void Awake()
        {
            pm = GetComponent<IShipPathModule>();
        }

        public void ResetObj()
        {
            pm.ResetPath();
        }

        public void ResetObjAndRelease()
        {
            ResetObj();
            releaseMethod.Invoke(this);
        }

        public void InjectRelease(Action<IShipPoolObjModule> release)
        {
            releaseMethod = release;
        }
    }
}