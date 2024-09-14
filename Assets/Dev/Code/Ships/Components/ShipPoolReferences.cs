using UnityEngine;

namespace Assets.Ships
{
    public class ShipPoolReferences : MonoBehaviour, IShipPoolReferences
    {
        [SerializeField] private GameObject tetraShipPrefab;
        public GameObject TetraShipPrefab => tetraShipPrefab;

        [SerializeField] private GameObject hexaShipPrefab;
        public GameObject HexaShipPrefab => hexaShipPrefab;

        [SerializeField] private GameObject octaShipPrefab;
        public GameObject OctaShipPrefab => octaShipPrefab;

        [SerializeField] private GameObject icosaShipPrefab;
        public GameObject IcosaShipPrefab => icosaShipPrefab;
    }
}