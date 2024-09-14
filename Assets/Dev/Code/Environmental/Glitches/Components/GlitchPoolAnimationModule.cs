using Assets.Environmental;
using Assets.Global;
using System.Collections;
using UnityEngine;

public class GlitchPoolAnimationModule : MonoBehaviour
{
    private IGlobalEventProcessorModule gepm;
    private IGlitchPoolSpawnModule gpsm;

    private void Awake()
    {
        gepm = GetComponent<IGlobalEventProcessorModule>();
        gpsm = GetComponent<IGlitchPoolSpawnModule>();
    }

    private void OnEnable()
    {
        gepm.GameOverEvent.AddListener(HandleGameOverEvent);
    }

    private void OnDisable()
    {
        gepm.GameOverEvent.RemoveListener(HandleGameOverEvent);
    }

    private void HandleGameOverEvent(IGameOverEventArgs args)
    {
        StartCoroutine(SpawnObjectsExponentially());
    }

    private IEnumerator SpawnObjectsExponentially()
    {
        for (float i = 2.5f; i > 0; i -= 0.15f)
        {
            var exp = Mathf.Exp(i) * 0.1f;
            var spawnCount = Mathf.Max(Mathf.RoundToInt((1 - exp) * 10), 1);
            for (int sp = 0; sp < spawnCount; sp++)
                gpsm.SpawnRandomGlitch(Random.Range(0, spawnCount * 50), minDistOverride: 0, maxDistOverride: 50);
            yield return new WaitForSeconds(exp);
        }
    }
}
