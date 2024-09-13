using UnityEngine;

[CreateAssetMenu(fileName = "New Ship Settings", menuName = "Scriptables/Ships/Ship Settings")]
public class ShipSettings : ScriptableObject, IShipSettings
{
    [SerializeField] private int value;
    public int Value => value;

    [SerializeField] private float speed;
    public float Speed => speed;
}
