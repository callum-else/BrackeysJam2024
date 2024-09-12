using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Environmental
{
    public class GlitchSpawnModule : MonoBehaviour
    {
        IGlitchEventProcessorModule eventModule;
        private ObjectPool<IGlitchEffectPoolObjModule> pool;
        private GameObject glitchPrefab;

        private void Awake()
        {
            var refs = GetComponent<IGlitchSpawnModuleReferences>();
            glitchPrefab = refs.GlitchPrefab;

            eventModule = GetComponent<IGlitchEventProcessorModule>();

            pool = new ObjectPool<IGlitchEffectPoolObjModule>(
                createFunc: () => 
                { 
                    var obj = Instantiate(glitchPrefab, transform).GetComponent<IGlitchEffectPoolObjModule>();
                    obj.gameObject.SetActive(false);
                    return obj;
                });
        }

        private void OnEnable() => 
            eventModule.GlitchSpawnEvent.AddListener(HandleGlitchSpawnEvent);

        private void OnDisable() =>
            eventModule.GlitchSpawnEvent.RemoveListener(HandleGlitchSpawnEvent);

        public void HandleGlitchSpawnEvent(Vector3 location)
        {
            pool.Get(out var effect);
            effect.EnableForPool(location);
            effect.gameObject.SetActive(true);
        }
    }
}