using Assets.Common;

public interface ICollisionModule : IComponent
{
    ColliderType Type { get; }
}
