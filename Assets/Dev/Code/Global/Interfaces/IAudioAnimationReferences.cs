using UnityEngine;

namespace Assets.Global
{
    public interface IAudioAnimationReferences
    {
        AudioSource MusicIntroAudioSource { get; }
        AudioSource MusicLoopAudioSource { get; }
        AudioSource StaticLoopAudioSource { get; }

        AudioClip HarborBootAudio { get; }
        AudioClip GlitchSpawnAudio { get; }
        AudioClip GameStartAudio { get; }
        AudioClip GameOverAudio { get; }
    }
}