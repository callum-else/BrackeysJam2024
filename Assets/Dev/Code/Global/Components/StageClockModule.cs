using UnityEngine;

public class StageClockModule : MonoBehaviour
{
    private IStageClockEventProcessorModule eventProcessor;
    private IGlobalEventProcessorModule globalEventProcessor;

    private bool ticking;
    private float currTime;
    private int stage;

    private void Awake()
    {
        currTime = 0;
        stage = 0;

        eventProcessor = GetComponent<IStageClockEventProcessorModule>();
        globalEventProcessor = GetComponent<IGlobalEventProcessorModule>();
        StartClock();
    }

    private void OnEnable()
    {
        eventProcessor.StartClockEvent.AddListener(StartClock);
        eventProcessor.PauseClockEvent.AddListener(PauseClock);

        globalEventProcessor.OnShipSaved.AddListener(HandleShipSaved);
        globalEventProcessor.OnShipDestroyed.AddListener(HandleShipDestroyed);
    }

    private void OnDisable()
    {
        eventProcessor.StartClockEvent.RemoveListener(StartClock);
        eventProcessor.PauseClockEvent.RemoveListener(PauseClock);

        globalEventProcessor.OnShipSaved.RemoveListener(HandleShipSaved);
        globalEventProcessor.OnShipDestroyed.RemoveListener(HandleShipDestroyed);
    }

    private void HandleShipSaved(IShipSavedEventArgs args)
    {

    }

    private void HandleShipDestroyed(IShipDestroyedEventArgs args)
    {

    }

    private void StartClock() =>
        ticking = true;

    private void PauseClock() => 
        ticking = false;

    private void FixedUpdate()
    {
        if (!ticking)
            return;

        currTime += Time.fixedDeltaTime;
        //Debug.Log($"{currTime / 30} - {stage}");
        if (currTime / 30 > stage)
        {
            stage++;
            globalEventProcessor.OnStageChanged.Invoke(stage);
            Debug.Log($"Stage: {stage}");
        }
    }
}
