using System.Collections.Generic;
using UnityEngine;

namespace Assets.Global
{
    public class VoiceOverAudioQueueModule : MonoBehaviour, IVoiceOverAudioQueueModule
    {
        private IGlobalEventProcessorModule gepm;
        private AudioSource audioSource;
        private Queue<AudioClip> queue = new();

        private void Awake()
        {
            gepm = GetComponent<IGlobalEventProcessorModule>();
            audioSource = GetComponent<IVoiceOverReferences>().VoiceOverAudioSource;
        }

        private void OnEnable()
        {
            gepm.VoiceOverEvent.AddListener(HandleVoiceOverRequest);
        }

        private void OnDisable()
        {
            gepm.VoiceOverEvent.RemoveListener(HandleVoiceOverRequest);
        }

        private void HandleVoiceOverRequest(AudioClip clip) =>
            Enqueue(clip);

        public void Enqueue(AudioClip clip) =>
            queue.Enqueue(clip);

        private void FixedUpdate()
        {
            if (audioSource.isPlaying)
                return;

            if (queue.Count == 0)
                return;

            audioSource.PlayOneShot(queue.Dequeue());
        }
    }
}