using DG.Tweening;
using UnityEngine;

namespace Assets.Global
{
    public class AudioAnimationModule : MonoBehaviour
    {
        private IGlobalEventProcessorModule gepm;

        [SerializeField] private AudioSource musicIntroSource;
        [SerializeField] private AudioSource musicLoopSource;
        [Space]
        [SerializeField] private AudioClip harborBootAudio;
        [SerializeField] private AudioClip glitchSpawnAudio;
        [SerializeField] private AudioClip gameStartAudio;
        [SerializeField] private AudioClip gameOverAudio;

        private void Awake()
        {
            gepm = GetComponent<IGlobalEventProcessorModule>();
        }

        private void OnEnable()
        {
            gepm.IntroAnimationEvent.AddListener(HandleIntroAnimEvent);
            gepm.GameOverAnimationEvent.AddListener(HandleGameOverAnimEvent);
        }

        private void OnDisable()
        {
            gepm.IntroAnimationEvent.RemoveListener(HandleIntroAnimEvent);
            gepm.GameOverAnimationEvent.RemoveListener(HandleGameOverAnimEvent);
        }

        private void HandleIntroAnimEvent(IIntroAnimationEventArgs args)
        {
            switch (args.Phase)
            {
                case GlobalIntroAnimationPhase.Intro:
                    musicIntroSource.PlayScheduled(args.StartTime);
                    musicLoopSource.PlayScheduled(args.StartTime + musicIntroSource.clip.length);
                    Debug.Log(musicIntroSource.clip.length);
                    break;

                case GlobalIntroAnimationPhase.HarborBootSequence1:
                    gepm.VoiceOverEvent.Invoke(harborBootAudio);
                    break;

                case GlobalIntroAnimationPhase.GlitchSpawn:
                    gepm.VoiceOverEvent.Invoke(glitchSpawnAudio);
                    break;

                case GlobalIntroAnimationPhase.BeginClock:
                    gepm.VoiceOverEvent.Invoke(gameStartAudio);
                    break;
            }
        }

        private void HandleGameOverAnimEvent(GlobalGameOverAnimationPhase phase)
        {
            switch (phase)
            {
                case GlobalGameOverAnimationPhase.SpawnGlitches:
                    musicLoopSource.DOPitch(-3, 6f).OnComplete(() => musicLoopSource.Stop());
                    break;

                case GlobalGameOverAnimationPhase.ShowGameOverScreen:
                    gepm.VoiceOverEvent.Invoke(gameOverAudio);
                    break;
            }
        }
    }
}