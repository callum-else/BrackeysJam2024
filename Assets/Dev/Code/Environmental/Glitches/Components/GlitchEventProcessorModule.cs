using UnityEngine;
using UnityEngine.Events;

namespace Assets.Environmental
{
    public class GlitchEventProcessorModule : MonoBehaviour, IGlitchEventProcessorModule
    {
        private UnityEvent<Vector3> onGlitchSpawn;
        public UnityEvent<Vector3> GlitchSpawnEvent { get => onGlitchSpawn ??= new(); }
    }
}