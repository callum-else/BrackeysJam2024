using Assets.Global;
using UnityEngine;

namespace Assets.HubShip
{
    public class HarborPositionPublishModule : MonoBehaviour
    {
        private IGlobalEventProcessorModule gepm;
        private IHarborEventProcessorModule epm;
        private Transform harborCollider;

        private bool canPublish = false;
        private float nextPublishTime;
        private const float publishDelay = 10f;

        private void Awake()
        {
            gepm = GetComponent<IGlobalEventProcessorModule>();
            epm = GetComponent<IHarborEventProcessorModule>();
            harborCollider = GetComponent<IHarborReferences>().BubbleCollider.transform;
        }

        private void OnEnable() =>
            epm.OnStateChanged.AddListener(HandleOnStateChanged);
        private void OnDisable() =>
            epm.OnStateChanged.RemoveListener(HandleOnStateChanged);

        private void HandleOnStateChanged(HarborState state)
        {
            if (state != HarborState.Active)
                return;

            nextPublishTime = Time.time + Random.value;
            canPublish = true;
        }

        private void FixedUpdate()
        {
            if (!canPublish)
                return;

            if (Time.time < nextPublishTime)
                return;

            //Debug.Log($"Publishing {gameObject.name}");
            gepm.HarborPositionEvent.Invoke(harborCollider.position);
            nextPublishTime += publishDelay;
        }
    }
}