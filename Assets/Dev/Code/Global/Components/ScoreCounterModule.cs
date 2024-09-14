using UnityEngine;

namespace Assets.Global
{
    public class ScoreCounterModule : MonoBehaviour
    {
        private IGlobalEventProcessorModule gepm;
        private IGlobalScoreTrackerModule gstm;

        private bool trackStats;
        private float startTime;
        private int soulsSaved;
        private int soulsLost;
        private const int lostSoulThreshold = 200;

        private void Awake()
        {
            gepm = GetComponent<IGlobalEventProcessorModule>();
            gstm = GetComponent<IGlobalScoreTrackerModule>();
        }

        private void OnEnable()
        {
            gepm.OnStageChanged.AddListener(HandleStageChanged);
            gepm.OnShipCrashed.AddListener(HandleOnShipCrashed);
            gepm.OnShipGlitched.AddListener(HandleOnShipGlitched);
            gepm.OnShipSaved.AddListener(HandleOnShipSaved);
        }

        private void OnDisable()
        {
            gepm.OnStageChanged.RemoveListener(HandleStageChanged);
            gepm.OnShipCrashed.RemoveListener(HandleOnShipCrashed);
            gepm.OnShipGlitched.RemoveListener(HandleOnShipGlitched);
            gepm.OnShipSaved.RemoveListener(HandleOnShipSaved);
        }

        private void FixedUpdate()
        {
            if (!trackStats) return;
            gstm.TimeSurvived = Time.fixedTime - startTime;
        }

        private void HandleStageChanged(int stage)
        {
            if (stage != 1) return;
            startTime = Time.fixedTime;
            trackStats = true;
        }

        private void HandleOnShipCrashed(IShipCrashedEventArgs args) => IncrementLostSouls(args.Value);
        private void HandleOnShipGlitched(IShipGlitchedEventArgs args) => IncrementLostSouls(args.Value);

        private void IncrementLostSouls(int value)
        {
            if (!trackStats) return;

            soulsLost += value;
            gstm.OnThresholdPercentUpdated.Invoke(Mathf.Clamp01((float)soulsLost / lostSoulThreshold));
            gstm.OnSoulsLostUpdated.Invoke(soulsLost);
            if (soulsLost >= lostSoulThreshold)
            {
                trackStats = false;
                gepm.GameOverEvent.Invoke(new GameOverEventArgs()
                {
                    TimeSurvived = gstm.TimeSurvived,
                    SoulsSaved = soulsSaved,
                    SoulsLost = soulsLost,
                });
            }
        }

        private void HandleOnShipSaved(IShipSavedEventArgs args)
        {
            if (!trackStats) return;
            soulsSaved += args.Value;
            gstm.OnSoulsSavedUpdated.Invoke(soulsSaved);
        }
    }
}