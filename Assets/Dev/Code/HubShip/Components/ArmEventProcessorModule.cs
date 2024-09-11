using UnityEngine;
using UnityEngine.Events;

namespace Assets.HubShip
{
    public interface IArmEventProcessorModule
    {
        UnityEvent<int> OnGameStageChanged { get; }
        UnityEvent<ArmState> OnStateChanged { get; }
    }

    public class ArmEventProcessorModule : MonoBehaviour, IArmEventProcessorModule
    {
        private UnityEvent<int> onGameStageChanged;
        public UnityEvent<int> OnGameStageChanged { get => onGameStageChanged ??= new(); }

        private UnityEvent<ArmState> onStateChanged;
        public UnityEvent<ArmState> OnStateChanged { get => onStateChanged ??= new(); }
    }
}