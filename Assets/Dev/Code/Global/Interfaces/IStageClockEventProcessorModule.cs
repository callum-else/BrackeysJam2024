using UnityEngine.Events;

namespace Assets.Global
{
    public interface IStageClockEventProcessorModule
    {
        UnityEvent PauseClockEvent { get; }
        UnityEvent StartClockEvent { get; }
    }
}