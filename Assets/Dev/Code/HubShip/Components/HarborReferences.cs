using UnityEngine;

namespace Assets.HubShip
{
    public class HarborReferences : MonoBehaviour, IHarborReferences
    {
        [SerializeField] private Renderer armRenderer;
        public Renderer ArmRenderer { get => armRenderer; }

        [SerializeField] private Renderer bubbleRenderer;
        public Renderer BubbleRenderer { get => bubbleRenderer; }

        [SerializeField] private Collider bubbleCollider;
        public Collider BubbleCollider { get => bubbleCollider; }

        [SerializeField] private Material inactiveMaterial;
        public Material InactiveMaterial { get => inactiveMaterial; }

        [SerializeField] private Material activeMaterial;
        public Material ActiveMaterial { get => activeMaterial; }
    }
}