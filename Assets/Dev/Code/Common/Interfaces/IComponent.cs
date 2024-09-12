using UnityEngine;

namespace Assets.Common
{
    public interface IComponent
    {
        Transform transform { get; }
        GameObject gameObject { get; }
        int GetInstanceID();
    }
}