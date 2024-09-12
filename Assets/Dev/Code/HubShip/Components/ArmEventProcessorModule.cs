using UnityEngine;
using UnityEngine.Events;

namespace Assets.HubShip
{
    public interface IArmEventProcessorModule
    {
        UnityEvent<ArmState> OnStateChanged { get; }
    }

    public class ArmEventProcessorModule : MonoBehaviour, IArmEventProcessorModule
    {
        private UnityEvent<ArmState> onStateChanged;
        public UnityEvent<ArmState> OnStateChanged { get => onStateChanged ??= new(); }
    }
}