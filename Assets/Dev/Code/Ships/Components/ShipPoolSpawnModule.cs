using Assets.Global;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Ships
{
    public class ShipPoolSpawnModule : MonoBehaviour
    {
        private IGlobalEventProcessorModule gepm;

        private ObjectPool<IShipPoolObjModule>[] pools;
        private List<int[]> spawnRates;
        private const int tetraShipIdx = 0, hexaShipIdx = 1, octaShipIdx = 2, icosaShipIdx = 3;

        private bool spawn;
        private int stage = 0;
        private float spawnSecs = 4f;
        private float nextSpawnTime;
        private const int spawnDist = 60;

        private void Awake()
        {
            gepm = GetComponent<IGlobalEventProcessorModule>();

            var refs = GetComponent<IShipPoolReferences>();

            pools = new ObjectPool<IShipPoolObjModule>[]
            {
                SetupPool(refs.TetraShipPrefab, tetraShipIdx),
                SetupPool(refs.HexaShipPrefab, hexaShipIdx),
                SetupPool(refs.OctaShipPrefab, octaShipIdx),
                SetupPool(refs.IcosaShipPrefab, icosaShipIdx)
            };

            spawnRates = new List<int[]>(8)
            {
                new int[] { tetraShipIdx },
                GenerateSpawnRateArray(new (int, int)[] { (tetraShipIdx, 6), (hexaShipIdx, 4) }),
                GenerateSpawnRateArray(new (int, int)[] { (tetraShipIdx, 5), (hexaShipIdx, 3), (octaShipIdx, 3) }),
                GenerateSpawnRateArray(new (int, int)[] { (tetraShipIdx, 5), (hexaShipIdx, 3), (octaShipIdx, 3), (icosaShipIdx, 2) }),
                GenerateSpawnRateArray(new (int, int)[] { (tetraShipIdx, 5), (hexaShipIdx, 3), (octaShipIdx, 3), (icosaShipIdx, 3) })
            };

            spawn = true; // Set as part of tutorial phase eventually
        }

        private void OnEnable()
        {
            gepm.OnStageChanged.AddListener(HandleStageChanged);
            gepm.GameOverEvent.AddListener(HandleGameOverEvent);
        }

        private void OnDisable()
        {
            gepm.OnStageChanged.RemoveListener(HandleStageChanged);
            gepm.GameOverEvent.RemoveListener(HandleGameOverEvent);
        }

        private void FixedUpdate()
        {
            if (!spawn) return;
            if (stage == 0) return;
            if (Time.time < nextSpawnTime) return;

            var idxArr = spawnRates[Mathf.Min(stage - 1, spawnRates.Count - 1)];
            pools[idxArr[Random.Range(0, idxArr.Length)]].Get();
            nextSpawnTime = Time.time + Mathf.Max(spawnSecs - (0.5f * stage), 1f);
        }

        private int[] GenerateSpawnRateArray((int idx, int count)[] chances)
        {
            var count = chances.Sum(x => x.count);
            var arr = new int[count];
            var currIdx = 0;

            foreach (var chance in chances)
            {
                for (int i = 0; i < chance.count; i++)
                    arr[currIdx + i] = chance.idx;
                currIdx += chance.count;
            }

            return arr;
        }

        private void HandleStageChanged(int stage)
        {
            this.stage = stage;
        }

        private void HandleGameOverEvent(IGameOverEventArgs args)
        {
            spawn = false;
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
            ship.ResetObj();
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