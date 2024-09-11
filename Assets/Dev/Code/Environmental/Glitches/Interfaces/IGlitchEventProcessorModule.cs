using UnityEngine;
using UnityEngine.Events;

namespace Assets.Environmental
{
    public interface IGlitchEventProcessorModule
    {
        UnityEvent<Vector3> GlitchSpawnEvent { get; }
    }
}