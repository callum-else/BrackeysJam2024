using UnityEngine;
using UnityEngine.Events;

namespace Assets.Global
{
    public class StageClockEventProcessorModule : MonoBehaviour, IStageClockEventProcessorModule
    {
        private UnityEvent startClockEvent;
        public UnityEvent StartClockEvent => startClockEvent ??= new();

        private UnityEvent pauseClockEvent;
        public UnityEvent PauseClockEvent => pauseClockEvent ??= new();
    }
}