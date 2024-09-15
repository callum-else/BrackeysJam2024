using Assets.Common;
using Assets.Effects;
using DG.Tweening;
using Kino;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Global
{
    public class GlitchEffectAnimationModule : AnimationModule
    {
        private IGlobalEventProcessorModule gepm;
        private IGlobalScoreTrackerModule gscm;

        private IAnalogGlitch analogGlitch;
        private Tweener analogTween;
        private float analogIntensity = 0.2f;

        private IDigitalGlitch digitalGlitch;

        private void Awake()
        {
            gepm = GetComponent<IGlobalEventProcessorModule>();
            gscm = GetComponent<IGlobalScoreTrackerModule>();
            analogGlitch = GetComponentInChildren<IAnalogGlitch>();
            digitalGlitch = GetComponentInChildren<IDigitalGlitch>();
            UpdateAnalogFx();

            gepm.GameOverAnimationEvent.AddListener(HandleGameOverAnimEvent);
        }

        private void OnEnable()
        {
            gscm.OnThresholdPercentUpdated.AddListener(HandleThresholdPercentUpdated);
        }

        private void OnDisable()
        {
            gscm.OnThresholdPercentUpdated.RemoveListener(HandleThresholdPercentUpdated);
        }

        private void HandleThresholdPercentUpdated(float value)
        {
            var ni = Mathf.Clamp01(value + 0.1f);

            if (analogIntensity >= ni) return;
            analogIntensity = ni;

            UpdateAnalogFx();
        }

        private void UpdateAnalogFx()
        {
            analogGlitch.scanLineJitter = analogIntensity * 0.45f;
            analogGlitch.verticalJump = analogIntensity * 0.01f;
            analogGlitch.horizontalShake = analogIntensity * 0.02f;
            analogGlitch.colorDrift = analogIntensity;
        }

        private void HandleGameOverAnimEvent(GlobalGameOverAnimationPhase phase)
        {
            if (phase != GlobalGameOverAnimationPhase.GlitchEffect) return;
            Play(GetDigitalGlitchPlayAnim());
            gepm.GameOverAnimationEvent.RemoveListener(HandleGameOverAnimEvent);
        }
            
        private IEnumerable<AnimationStep> GetDigitalGlitchPlayAnim()
        {
            return new AnimationStep[]
            {
                new AnimationStep(4f, _ =>
                {
                    digitalGlitch.Intensity = 1f;
                    digitalGlitch.FlipIntensity = 1f;
                    digitalGlitch.ColorIntensity = 0.5f;
                    digitalGlitch.ShowGlitch = true;
                }),
                new AnimationStep(1f, _ => digitalGlitch.ShowGlitch = false)
            };
        }
    }
}