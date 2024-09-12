using UnityEngine.Events;

public interface IStageClockEventProcessorModule
{
    UnityEvent PauseClockEvent { get; }
    UnityEvent StartClockEvent { get; }
}