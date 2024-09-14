using UnityEngine;
using UnityEngine.Events;

namespace Assets.Global
{
    public class GlobalEventProcessorModule : MonoBehaviour, IGlobalEventProcessorModule
    {
        public UnityEvent<IShipCrashedEventArgs> OnShipCrashed => GlobalEventProcessorLogic.OnShipCrashed;
        public UnityEvent<IShipSavedEventArgs> OnShipSaved => GlobalEventProcessorLogic.OnShipSaved;
        public UnityEvent<IShipGlitchedEventArgs> OnShipGlitched => GlobalEventProcessorLogic.OnShipGlitched;
        public UnityEvent<int> OnStageChanged => GlobalEventProcessorLogic.OnStageChanged;
        public UnityEvent<AudioClip> VoiceOverEvent => GlobalEventProcessorLogic.VoiceOverEvent;
        public UnityEvent<Vector3> HarborPositionEvent => GlobalEventProcessorLogic.HarborPositionEvent;
        public UnityEvent<IGameOverEventArgs> GameOverEvent => GlobalEventProcessorLogic.GameOverEvent;
    }
}