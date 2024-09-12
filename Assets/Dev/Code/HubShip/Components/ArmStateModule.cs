using Assets.Global;
using UnityEngine;

namespace Assets.HubShip
{
    public enum ArmState
    {
        Active,
        Inactive
    }

    public class ArmStateModule : MonoBehaviour, IArmStateModule
    {
        private IGlobalEventProcessorModule globalEventProcessor;
        private IArmEventProcessorModule eventProcessor;
        private IArmSettingsModule settingsModule;
        private ArmState state;

        public ArmState State { get; }

        private void Awake()
        {
            globalEventProcessor = GetComponent<IGlobalEventProcessorModule>();
            eventProcessor = GetComponent<IArmEventProcessorModule>();
            settingsModule = GetComponent<IArmSettingsModule>();
            state = ArmState.Inactive;
        }

        private void OnEnable()
        {
            globalEventProcessor.OnStageChanged.AddListener(HandleStageChanged);
        }

        private void OnDisable()
        {
            globalEventProcessor.OnStageChanged.RemoveListener(HandleStageChanged);
        }

        private void HandleStageChanged(int stage)
        {
            if (state == ArmState.Active)
                return;

            if (stage < settingsModule.Settings.ActivationStage)
                return;

            state = ArmState.Active;
            eventProcessor.OnStateChanged.Invoke(state);
            globalEventProcessor.VoiceOverEvent.Invoke(settingsModule.Settings.ActivationAudio);
        }
    }
}
