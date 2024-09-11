using UnityEngine;

namespace Assets.Environmental
{
    public class GlitchReferences : MonoBehaviour, IGlitchSpawnModuleReferences
    {
        [SerializeField] private Transform activePoolParent;
        public Transform ActivePoolParent { get => activePoolParent; }

        [SerializeField] private Transform inactivePoolParent;
        public Transform InactivePoolParent { get => inactivePoolParent; }

        [SerializeField] private GameObject glitchPrefab;
        public GameObject GlitchPrefab { get => glitchPrefab; }
    }
}