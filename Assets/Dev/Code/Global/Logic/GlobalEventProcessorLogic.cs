using UnityEngine;
using UnityEngine.Events;

namespace Assets.Global
{
    public static class GlobalEventProcessorLogic
    {
        private static UnityEvent<IShipCrashedEventArgs> onShipCrashed;
        public static UnityEvent<IShipCrashedEventArgs> OnShipCrashed => onShipCrashed ??= new();

        private static UnityEvent<IShipSavedEventArgs> onShipSaved;
        public static UnityEvent<IShipSavedEventArgs> OnShipSaved => onShipSaved ??= new();

        private static UnityEvent<IShipGlitchedEventArgs> onShipGlitched;
        public static UnityEvent<IShipGlitchedEventArgs> OnShipGlitched => onShipGlitched ??= new();


        private static UnityEvent<int> onStageChanged;
        public static UnityEvent<int> OnStageChanged => onStageChanged ??= new();


        private static UnityEvent<AudioClip> voiceOverEvent;
        public static UnityEvent<AudioClip> VoiceOverEvent => voiceOverEvent ??= new();


        private static UnityEvent<Vector3> harborPositionEvent;
        public static UnityEvent<Vector3> HarborPositionEvent => harborPositionEvent ??= new();

        private static UnityEvent<IGameOverEventArgs> gameOverEvent;
        public static UnityEvent<IGameOverEventArgs> GameOverEvent => gameOverEvent ??= new();

        private static UnityEvent<IIntroAnimationEventArgs> introAnimationEvent;
        public static UnityEvent<IIntroAnimationEventArgs> IntroAnimationEvent => introAnimationEvent ??= new();

        private static UnityEvent<GlobalGameOverAnimationPhase> gameOverAnimationEvent;
        public static UnityEvent<GlobalGameOverAnimationPhase> GameOverAnimationEvent => gameOverAnimationEvent ??= new();
    }
}