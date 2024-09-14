using Assets.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Global
{

    public enum GlobalIntroAnimationPhase
    {
        Intro,
        HarborBootSequence1,
        HarborBootSequence2,
        GlitchSpawn,
        BeginClock,
    }

    public enum GlobalGameOverAnimationPhase
    {
        SpawnGlitches,
        GlitchEffect,
        ShowGameOverScreen
    }

    public class GlobalGameAnimationModule : AnimationModule
    {
        private IGlobalEventProcessorModule gepm;

        private void Awake()
        {
            gepm = GetComponent<IGlobalEventProcessorModule>();
            Play(GetIntroAnim());
        }

        private void OnEnable()
        {
            gepm.GameOverEvent.AddListener(HandleGameOverEvent);
        }

        private void OnDisable()
        {
            gepm.GameOverEvent.RemoveListener(HandleGameOverEvent);
        }

        private void HandleGameOverEvent(IGameOverEventArgs args)
        {
            Play(GetGameOverAnim());
        }

        private IEnumerable<AnimationStep> GetIntroAnim()
        {
            return new AnimationStep[]
            {
                new AnimationStep(20f, _ => gepm.IntroAnimationEvent.Invoke(new IntroAnimationEventArgs(GlobalIntroAnimationPhase.Intro, AudioSettings.dspTime + 1))),
                new AnimationStep(6f, _ => gepm.IntroAnimationEvent.Invoke(new IntroAnimationEventArgs(GlobalIntroAnimationPhase.HarborBootSequence1, 0))),
                new AnimationStep(6f, _ => gepm.IntroAnimationEvent.Invoke(new IntroAnimationEventArgs(GlobalIntroAnimationPhase.HarborBootSequence2, 0))),
                new AnimationStep(8f, _ => gepm.IntroAnimationEvent.Invoke(new IntroAnimationEventArgs(GlobalIntroAnimationPhase.BeginClock, 0))),
                new AnimationStep(0f, _ => gepm.IntroAnimationEvent.Invoke(new IntroAnimationEventArgs(GlobalIntroAnimationPhase.GlitchSpawn, 0))),
            };
        }

        private IEnumerable<AnimationStep> GetGameOverAnim()
        {
            return new AnimationStep[]
            {
                new AnimationStep(6f, _ => gepm.GameOverAnimationEvent.Invoke(GlobalGameOverAnimationPhase.SpawnGlitches)),
                new AnimationStep(3f, _ => gepm.GameOverAnimationEvent.Invoke(GlobalGameOverAnimationPhase.GlitchEffect)),
                new AnimationStep(1f, _ => gepm.GameOverAnimationEvent.Invoke(GlobalGameOverAnimationPhase.ShowGameOverScreen)),
            };
        }
    }
}