using UnityEngine;

namespace Assets.Global
{
    public interface IVoiceOverAudioQueueModule
    {
        void Enqueue(AudioClip clip);
    }
}