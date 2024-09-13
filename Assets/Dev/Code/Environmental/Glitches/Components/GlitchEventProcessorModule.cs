using UnityEngine;
using UnityEngine.Events;

namespace Assets.Environmental
{
    public class GlitchEventProcessorModule : MonoBehaviour, IGlitchEventProcessorModule
    {
        private UnityEvent<Vector3> glitchSpawnEvent;
        public UnityEvent<Vector3> GlitchSpawnEvent { get => glitchSpawnEvent ??= new(); }
    }
}