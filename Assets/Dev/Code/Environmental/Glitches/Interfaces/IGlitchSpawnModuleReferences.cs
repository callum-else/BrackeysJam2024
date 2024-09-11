using UnityEngine;

namespace Assets.Environmental
{
    public interface IGlitchSpawnModuleReferences
    {
        Transform ActivePoolParent { get; }
        Transform InactivePoolParent { get; }
        GameObject GlitchPrefab { get; }
    }
}