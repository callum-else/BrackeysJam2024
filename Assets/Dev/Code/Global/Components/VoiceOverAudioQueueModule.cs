using Assets.Global;
using System.Collections.Generic;
using UnityEngine;

public class VoiceOverAudioQueueModule : MonoBehaviour
{
    private IGlobalEventProcessorModule eventProcessor;
    private AudioSource audioSource;
    private Queue<AudioClip> queue = new();

    private void Awake()
    {
        eventProcessor = GetComponent<IGlobalEventProcessorModule>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        eventProcessor.VoiceOverEvent.AddListener(HandleVoiceOverRequest);
    }

    private void OnDisable()
    {
        eventProcessor.VoiceOverEvent.RemoveListener(HandleVoiceOverRequest);
    }

    private void HandleVoiceOverRequest(AudioClip clip)
    {
        queue.Enqueue(clip);
    }

    private void FixedUpdate()
    {
        if (audioSource.isPlaying)
            return;

        if (queue.Count == 0)
            return;

        audioSource.PlayOneShot(queue.Dequeue());
    }
}
