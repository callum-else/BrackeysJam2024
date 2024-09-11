using UnityEngine;

namespace Assets.HubShip
{
    public interface IArmAnimationReferences
    {
        Renderer ArmRenderer { get; }
        Renderer BubbleRenderer { get; }
        Collider BubbleCollider { get; }
        Material InactiveMaterial { get; }
        Material ActiveMaterial { get; }
    }
}