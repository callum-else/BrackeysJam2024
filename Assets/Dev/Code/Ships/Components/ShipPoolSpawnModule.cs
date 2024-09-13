using Assets.Global;
using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Ships
{
    public class ShipPoolSpawnModule : MonoBehaviour
    {
        private IGlobalEventProcessorModule gepm;

        private ObjectPool<IShipPoolObjModule>[] pools;

        private int stage = 0;
        private float spawnSecs = 4.5f;
        private float nextSpawnTime;
        private const int spawnDist = 60;

        private void Awake()
        {
            gepm = GetComponent<IGlobalEventProcessorModule>();

            var refs = GetComponent<IShipPoolReferences>();

            pools = new ObjectPool<IShipPoolObjModule>[]
            {
                SetupPool(refs.TetraShipPrefab, 0),
                SetupPool(refs.HexaShipPrefab, 1),
                SetupPool(refs.OctaShipPrefab, 2),
                SetupPool(refs.IcosaShipPrefab, 3)
            };
        }

        private void OnEnable()
        {
            gepm.OnStageChanged.AddListener(HandleStageChanged);
        }

        private void OnDisable()
        {
            gepm.OnStageChanged.RemoveListener(HandleStageChanged);
        }

        private void FixedUpdate()
        {
            if (stage == 0) return;
            if (Time.time < nextSpawnTime) return;

            pools[Random.Range(0, pools.Length)].Get();
            nextSpawnTime = Time.time + Mathf.Max(spawnSecs - (0.5f * stage), 0.75f);
            Debug.Log($"{nextSpawnTime} - {Time.time}");
        }

        private void HandleStageChanged(int stage)
        {
            this.stage = stage;
        }

        private ObjectPool<IShipPoolObjModule> SetupPool(GameObject prefab, int idx)
        {
            return new ObjectPool<IShipPoolObjModule>(
                createFunc: () => SpawnShip(prefab, idx),
                actionOnGet: GetPooledShip,
                actionOnRelease: ReleasePooledShip);
        }

        private void ReleasePooledShip(IShipPoolObjModule ship)
        {
            ship.gameObject.SetActive(false);
        }

        private void GetPooledShip(IShipPoolObjModule ship)
        {
            var randPos = Random.insideUnitSphere;
            randPos.y = 0;
            
            ship.transform.position = randPos.normalized * spawnDist;
            ship.gameObject.SetActive(true);
        }

        private IShipPoolObjModule SpawnShip(GameObject prefab, int idx)
        {
            var obj = Instantiate(prefab, transform).GetComponent<IShipPoolObjModule>();
            obj.InjectRelease(pools[idx].Release);
            obj.gameObject.SetActive(false);
            return obj;
        }
    }
}