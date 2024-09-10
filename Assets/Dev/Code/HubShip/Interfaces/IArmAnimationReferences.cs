using UnityEngine;

namespace Assets.HubShip
{
    public interface IArmAnimationReferences
    {
        Renderer ArmRenderer { get; }
        Renderer BubbleRenderer { get; }
        Material InactiveMaterial { get; }
        Material ActiveMaterial { get; }
    }
}