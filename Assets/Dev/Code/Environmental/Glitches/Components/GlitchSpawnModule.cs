using System.Collections.Generic;
using UnityEngine;

namespace Assets.Environmental
{
    public class GlitchSpawnModule : MonoBehaviour
    {
        IGlitchEventProcessorModule eventModule;
        private Transform active;
        private Transform inactive;
        private GameObject glitchPrefab;

        private void Awake()
        {
            var refs = GetComponent<IGlitchSpawnModuleReferences>();
            active = refs.ActivePoolParent;
            inactive = refs.InactivePoolParent;
            glitchPrefab = refs.GlitchPrefab;

            eventModule = GetComponent<IGlitchEventProcessorModule>();
        }

        private void OnEnable() => 
            eventModule.GlitchSpawnEvent.AddListener(HandleGlitchSpawnEvent);

        private void OnDisable() =>
            eventModule.GlitchSpawnEvent.RemoveListener(HandleGlitchSpawnEvent);

        public void HandleGlitchSpawnEvent(Vector3 location)
        {
            if (inactive.childCount != 0)
            {
                var poolObj = inactive.GetChild(0);
                poolObj.GetComponent<IGlitchEffectPoolObjModule>().ResetForPool();
                poolObj.parent = active;
                poolObj.position = location;
                poolObj.gameObject.SetActive(true);
                return;
            }
            
            glitchPrefab.transform.position = location;
            Instantiate(glitchPrefab, active);
        }
    }
}