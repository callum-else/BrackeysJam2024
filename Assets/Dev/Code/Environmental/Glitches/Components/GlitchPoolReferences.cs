using UnityEngine;

namespace Assets.Environmental
{
    public class GlitchPoolReferences : MonoBehaviour, IGlitchSpawnModuleReferences
    {
        [SerializeField] private GameObject glitchPrefab;
        public GameObject GlitchPrefab { get => glitchPrefab; }
    }
}