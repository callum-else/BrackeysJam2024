using DG.Tweening;
using UnityEngine;

namespace Assets.Global
{
    public class AudioAnimationModule : MonoBehaviour
    {
        private IGlobalEventProcessorModule gepm;
        private IVoiceOverAudioQueueModule voaqm;

        private AudioSource musicIntroSource;
        private AudioSource musicLoopSource;
        private AudioSource staticLoopSource;

        private AudioClip harborBootAudio;
        private AudioClip glitchSpawnAudio;
        private AudioClip gameStartAudio;
        private AudioClip gameOverAudio;

        private void Awake()
        {
            gepm = GetComponent<IGlobalEventProcessorModule>();
            voaqm = GetComponent<IVoiceOverAudioQueueModule>();
            
            var refs = GetComponent<IAudioAnimationReferences>();
            musicIntroSource = refs.MusicIntroAudioSource;
            musicLoopSource = refs.MusicLoopAudioSource;
            staticLoopSource = refs.StaticLoopAudioSource;
            harborBootAudio = refs.HarborBootAudio;
            glitchSpawnAudio = refs.GlitchSpawnAudio;
            gameStartAudio = refs.GameStartAudio;
            gameOverAudio = refs.GameOverAudio;
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
                    voaqm.Enqueue(harborBootAudio);
                    break;

                case GlobalIntroAnimationPhase.GlitchSpawn:
                    voaqm.Enqueue(glitchSpawnAudio);
                    break;

                case GlobalIntroAnimationPhase.BeginClock:
                    voaqm.Enqueue(gameStartAudio);
                    break;
            }
        }

        private void HandleGameOverAnimEvent(GlobalGameOverAnimationPhase phase)
        {
            switch (phase)
            {
                case GlobalGameOverAnimationPhase.SpawnGlitches:
                    musicLoopSource.DOPitch(-3, 8f).OnComplete(() => musicLoopSource.Stop());
                    staticLoopSource.Play();
                    break;

                case GlobalGameOverAnimationPhase.ShowGameOverScreen:
                    voaqm.Enqueue(gameOverAudio);
                    break;
            }
        }
    }
}