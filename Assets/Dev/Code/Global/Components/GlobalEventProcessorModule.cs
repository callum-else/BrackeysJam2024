using UnityEngine;
using UnityEngine.Events;

namespace Assets.Global
{
    public class GlobalEventProcessorModule : MonoBehaviour, IGlobalEventProcessorModule
    {
        public UnityEvent<IShipDestroyedEventArgs> OnShipDestroyed => GlobalEventProcessorLogic.OnShipDestroyed;
        public UnityEvent<IShipSavedEventArgs> OnShipSaved => GlobalEventProcessorLogic.OnShipSaved;
        public UnityEvent<int> OnStageChanged => GlobalEventProcessorLogic.OnStageChanged;
        public UnityEvent<AudioClip> VoiceOverEvent => GlobalEventProcessorLogic.VoiceOverEvent;
    }
}