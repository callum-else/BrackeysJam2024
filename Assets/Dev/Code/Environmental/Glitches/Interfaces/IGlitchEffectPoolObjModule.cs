using Assets.Common;
using UnityEngine;

namespace Assets.Environmental
{
    public interface IGlitchEffectPoolObjModule : IComponent
    {
        void EnableForPool(Vector3 location, int capturedValue);
        void DisableForPool();
        void IncreaseCapturedValue(int value);
    }
}