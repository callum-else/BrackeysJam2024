using UnityEngine.Events;

namespace Assets.HubShip
{
    public interface IHarborEventProcessorModule
    {
        UnityEvent<HarborState> OnStateChanged { get; }
    }
}