using UnityEngine;

public class CollisionModule : MonoBehaviour, ICollisionModule
{
    [SerializeField] private ColliderType type;
    public ColliderType Type => type;
}
