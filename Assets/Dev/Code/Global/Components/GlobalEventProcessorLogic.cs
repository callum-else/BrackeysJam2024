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
    }
}