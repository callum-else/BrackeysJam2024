using UnityEngine;

namespace Assets.Global
{
    public class AudioProcessorReferences : MonoBehaviour, IVoiceOverReferences, IAudioAnimationReferences, ISfxReferences
    {
        [SerializeField] private AudioSource voiceOverAudioSource;
        public AudioSource VoiceOverAudioSource => voiceOverAudioSource;

        [SerializeField] private AudioClip harborBootAudio;
        public AudioClip HarborBootAudio => harborBootAudio;

        [SerializeField] private AudioClip glitchSpawnAudio;
        public AudioClip GlitchSpawnAudio => glitchSpawnAudio;

        [SerializeField] private AudioClip gameStartAudio;
        public AudioClip GameStartAudio => gameStartAudio;

        [SerializeField] private AudioClip gameOverAudio;
        public AudioClip GameOverAudio => gameOverAudio;

        [Space]

        [SerializeField] private AudioSource musicIntroAudioSource;
        public AudioSource MusicIntroAudioSource => musicIntroAudioSource;

        [SerializeField] private AudioSource musicLoopAudioSource;
        public AudioSource MusicLoopAudioSource => musicLoopAudioSource;
        
        [SerializeField] private AudioSource staticLoopAudioSource;
        public AudioSource StaticLoopAudioSource => staticLoopAudioSource;

        [Space]

        [SerializeField] private AudioSource sfxSource;
        public AudioSource SfxSource => sfxSource;

        [SerializeField] private AudioClip[] glitchFx;
        public AudioClip[] GlitchFx => glitchFx;

        [SerializeField] private AudioClip[] savedFx;
        public AudioClip[] SavedFx => savedFx;
    }
}