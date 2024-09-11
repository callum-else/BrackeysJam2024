using Assets.Environmental;
using UnityEngine;

public class GlitchSpawnTestModule : MonoBehaviour
{
    private float GetRandomPos()
    {
        return Random.value * 10 * (Random.value > 0.5 ? -1 : 1);
    }

    private void Start()
    {
        var epm = GetComponent<IGlitchEventProcessorModule>();
        for (int i = 0; i < 10; i++)
        {
            epm.GlitchSpawnEvent.Invoke(new Vector3(GetRandomPos(), 0, GetRandomPos()));
        }
    }
}
