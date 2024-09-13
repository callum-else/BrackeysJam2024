using Assets.Global;
using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Environmental
{
    public class GlitchSpawnModule : MonoBehaviour
    {
        private IGlobalEventProcessorModule gepm;
        private ObjectPool<IGlitchEffectPoolObjModule> pool;
        private GameObject glitchPrefab;

        private void Awake()
        {
            var refs = GetComponent<IGlitchSpawnModuleReferences>();
            glitchPrefab = refs.GlitchPrefab;

            gepm = GetComponent<IGlobalEventProcessorModule>();

            pool = new ObjectPool<IGlitchEffectPoolObjModule>(
                createFunc: () => 
                { 
                    var obj = Instantiate(glitchPrefab, transform).GetComponent<IGlitchEffectPoolObjModule>();
                    obj.gameObject.SetActive(false);
                    return obj;
                });
        }

        private void OnEnable() => 
            gepm.OnShipCrashed.AddListener(HandleGlitchSpawnEvent);

        private void OnDisable() =>
            gepm.OnShipCrashed.RemoveListener(HandleGlitchSpawnEvent);

        public void HandleGlitchSpawnEvent(IShipCrashedEventArgs args)
        {
            pool.Get(out var effect);
            effect.EnableForPool(args.Location);
            effect.gameObject.SetActive(true);
        }
    }
}