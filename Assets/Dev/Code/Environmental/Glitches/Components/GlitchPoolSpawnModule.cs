using Assets.Global;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Environmental
{
    public class GlitchPoolSpawnModule : MonoBehaviour, IGlitchPoolSpawnModule
    {
        private IGlobalEventProcessorModule gepm;
        private List<IGlitchEffectPoolObjModule> pool = new();
        private GameObject glitchPrefab;

        private float maxSpawnDist = 30f;
        private float minSpawnDist = 15f;
        private const int spawnPerStage = 2;

        private void Awake()
        {
            gepm = GetComponent<IGlobalEventProcessorModule>();
            glitchPrefab = GetComponent<IGlitchSpawnModuleReferences>().GlitchPrefab;

            gepm.IntroAnimationEvent.AddListener(HandleIntroAnimEvent);
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

        private void HandleIntroAnimEvent(IIntroAnimationEventArgs args)
        {
            if (args.Phase != GlobalIntroAnimationPhase.GlitchSpawn) return;
            SpawnGlitchesForStage();
            gepm.IntroAnimationEvent.RemoveListener(HandleIntroAnimEvent);
        }

        private void HandleStageChanged(int stage)
        {
            if (stage < 2) return;
            SpawnGlitchesForStage();
        }

        private void SpawnGlitchesForStage()
        {
            for (int i = 0; i < spawnPerStage; i++)
                SpawnRandomGlitch(Random.Range(0, 10));
        }

        public void SpawnRandomGlitch(int value = 0, float? minDistOverride = null, float? maxDistOverride = null)
        {
            var randPos = Random.insideUnitSphere;
            randPos.y = 0;
            SpawnGlitch(randPos.normalized * Random.Range(minDistOverride ?? minSpawnDist, maxDistOverride ?? maxSpawnDist), value);
        }

        private void HandleShipCrashed(IShipCrashedEventArgs args)
        {
            SpawnGlitch(args.Location, args.Value);
        }

        private void SpawnGlitch(Vector3 location, int value = 0)
        {
            var overlap = pool.FirstOrDefault(x => Vector3.Distance(x.transform.position, location) < Mathf.Max(x.transform.localScale.magnitude, 2f));
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