using Assets.Global;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Environmental
{
    public class GlitchSpawnModule : MonoBehaviour
    {
        private IGlobalEventProcessorModule gepm;
        private List<IGlitchEffectPoolObjModule> pool = new();
        private GameObject glitchPrefab;

        private void Awake()
        {
            gepm = GetComponent<IGlobalEventProcessorModule>();
            glitchPrefab = GetComponent<IGlitchSpawnModuleReferences>().GlitchPrefab;
        }

        private void OnEnable() =>
            gepm.OnShipCrashed.AddListener(HandleGlitchSpawnEvent);

        private void OnDisable() =>
            gepm.OnShipCrashed.RemoveListener(HandleGlitchSpawnEvent);
        

        public void HandleGlitchSpawnEvent(IShipCrashedEventArgs args)
        {
            var overlap = pool.FirstOrDefault(x => Vector3.Distance(x.transform.position, args.Location) < Mathf.Max(x.transform.localScale.magnitude, 1f));
            if (overlap != null)
            {
                overlap.IncreaseCapturedValue(args.Value);
                return;
            }

            var obj = Instantiate(glitchPrefab, transform).GetComponent<IGlitchEffectPoolObjModule>();
            pool.Add(obj);
            obj.gameObject.SetActive(false);

            obj.EnableForPool(args.Location, args.Value);
            obj.gameObject.SetActive(true);
        }
    }
}