using UnityEngine;

namespace Assets.Global
{
    public class StageClockModule : MonoBehaviour
    {
        private IGlobalEventProcessorModule gepm;

        private bool ticking;
        private float currTime;
        private int stage;

        private void Awake()
        {
            currTime = 0;
            stage = 0;

            gepm = GetComponent<IGlobalEventProcessorModule>();
        }

        private void OnEnable()
        {
            gepm.IntroAnimationEvent.AddListener(HandleIntroAnimEvent);
            gepm.OnShipSaved.AddListener(HandleShipSaved);
            gepm.OnShipCrashed.AddListener(HandleShipDestroyed);
        }

        private void OnDisable()
        {
            gepm.IntroAnimationEvent.RemoveListener(HandleIntroAnimEvent);
            gepm.OnShipSaved.RemoveListener(HandleShipSaved);
            gepm.OnShipCrashed.RemoveListener(HandleShipDestroyed);
        }

        private void HandleIntroAnimEvent(IIntroAnimationEventArgs args)
        {
            if (args.Phase != GlobalIntroAnimationPhase.BeginClock) return;
            StartClock();
        }

        private void HandleShipSaved(IShipSavedEventArgs args)
        {
            currTime -= args.Value * 0.1f;
        }

        private void HandleShipDestroyed(IShipCrashedEventArgs args)
        {
            currTime += args.Value * 0.1f;
        }

        private void StartClock() =>
            ticking = true;

        private void PauseClock() =>
            ticking = false;

        private void FixedUpdate()
        {
            if (!ticking)
                return;

            currTime += Time.fixedDeltaTime;
            if (currTime / 30 > stage)
            {
                stage++;
                gepm.OnStageChanged.Invoke(stage);
                Debug.Log($"Stage: {stage}");
            }
        }
    }
}