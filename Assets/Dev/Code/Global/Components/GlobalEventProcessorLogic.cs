﻿using UnityEngine;
using UnityEngine.Events;

namespace Assets.Global
{
    public static class GlobalEventProcessorLogic
    {
        private static UnityEvent<IShipDestroyedEventArgs> onShipDestroyed;
        public static UnityEvent<IShipDestroyedEventArgs> OnShipDestroyed => onShipDestroyed ??= new();

        private static UnityEvent<IShipSavedEventArgs> onShipSaved;
        public static UnityEvent<IShipSavedEventArgs> OnShipSaved => onShipSaved ??= new();


        private static UnityEvent<int> onStageChanged;
        public static UnityEvent<int> OnStageChanged => onStageChanged ??= new();


        private static UnityEvent<AudioClip> voiceOverEvent;
        public static UnityEvent<AudioClip> VoiceOverEvent => voiceOverEvent ??= new();


        private static UnityEvent<Vector3> harborPositionEvent;
        public static UnityEvent<Vector3> HarborPositionEvent => harborPositionEvent ??= new();
    }
}