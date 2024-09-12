using Assets.Global;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.HubShip
{
    public class HarborEventProcessorModule : MonoBehaviour, IHarborEventProcessorModule
    {
        private UnityEvent<HarborState> onStateChanged;
        public UnityEvent<HarborState> OnStateChanged { get => onStateChanged ??= new(); }
    }
}