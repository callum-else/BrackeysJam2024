using UnityEngine.Events;

namespace Assets.Global
{
    public interface IGlobalEventProcessorModule
    {
        UnityEvent<IShipDestroyedEventArgs> OnShipDestroyed { get; }
        UnityEvent<IShipSavedEventArgs> OnShipSaved { get; }
        UnityEvent<int> OnStageChanged { get; }
    }
}