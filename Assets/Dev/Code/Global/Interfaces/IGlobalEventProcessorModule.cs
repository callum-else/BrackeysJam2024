using UnityEngine;
using UnityEngine.Events;

namespace Assets.Global
{
    public interface IGlobalEventProcessorModule
    {
        UnityEvent<IShipCrashedEventArgs> OnShipCrashed { get; }
        UnityEvent<IShipSavedEventArgs> OnShipSaved { get; }
        UnityEvent<IShipGlitchedEventArgs> OnShipGlitched { get; }
        UnityEvent<int> OnStageChanged { get; }
        UnityEvent<AudioClip> VoiceOverEvent { get; }
        UnityEvent<Vector3> HarborPositionEvent { get; }
        UnityEvent<IGameOverEventArgs> GameOverEvent { get; }
        UnityEvent<IIntroAnimationEventArgs> IntroAnimationEvent { get; }
        UnityEvent<GlobalGameOverAnimationPhase> GameOverAnimationEvent { get; }
    }
}