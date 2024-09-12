using UnityEngine;

namespace Assets.Environmental
{
    public class GlitchReferences : MonoBehaviour, IGlitchSpawnModuleReferences
    {
        [SerializeField] private GameObject glitchPrefab;
        public GameObject GlitchPrefab { get => glitchPrefab; }
    }
}