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

        private float maxSpawnDist = 35f;
        private float minSpawnDist = 15f;

        private void Awake()
        {
            gepm = GetComponent<IGlobalEventProcessorModule>();
            glitchPrefab = GetComponent<IGlitchSpawnModuleReferences>().GlitchPrefab;
        }

        private void OnEnable()
        {
            gepm.OnShipCrashed.AddListener(HandleShipCrashed);
            gepm.OnStageChanged.AddListener(HandleStageChanged);
        }

        private void OnDisable()
        {
            gepm.OnShipCrashed.RemoveListener(HandleShipCrashed);
            gepm.OnStageChanged.RemoveListener(HandleStageChanged);
        }
        
        private void HandleStageChanged(int stage)
        {
            if (stage == 0)
                return;

            Vector3 randPos;
            for (int i = 0; i < stage; i++)
            {
                randPos = Random.insideUnitSphere;
                randPos.y = 0;
                SpawnGlitch(randPos.normalized * Random.Range(minSpawnDist, maxSpawnDist));
            }
        }

        private void HandleShipCrashed(IShipCrashedEventArgs args)
        {
            SpawnGlitch(args.Location, args.Value);
        }

        private void SpawnGlitch(Vector3 location, int value = 0)
        {
            var overlap = pool.FirstOrDefault(x => Vector3.Distance(x.transform.position, location) < Mathf.Max(x.transform.localScale.magnitude, 1f));
            if (overlap != null)
            {
                overlap.IncreaseCapturedValue(value);
                return;
            }

            var obj = Instantiate(glitchPrefab, transform).GetComponent<IGlitchEffectPoolObjModule>();
            pool.Add(obj);
            obj.gameObject.SetActive(false);

            obj.EnableForPool(location, value);
            obj.gameObject.SetActive(true);
        }
    }
}