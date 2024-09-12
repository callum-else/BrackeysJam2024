using Assets.Global;
using UnityEngine;

namespace Assets.HubShip
{
    public enum HarborState
    {
        Active,
        Inactive
    }

    public class HarborStateModule : MonoBehaviour, IHarborStateModule
    {
        private IGlobalEventProcessorModule globalEventProcessor;
        private IHarborEventProcessorModule eventProcessor;
        private IHarborSettingsModule settingsModule;
        private HarborState state;

        public HarborState State { get; }

        private void Awake()
        {
            globalEventProcessor = GetComponent<IGlobalEventProcessorModule>();
            eventProcessor = GetComponent<IHarborEventProcessorModule>();
            settingsModule = GetComponent<IHarborSettingsModule>();
            state = HarborState.Inactive;
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
            if (state == HarborState.Active)
                return;

            if (stage < settingsModule.Settings.ActivationStage)
                return;

            state = HarborState.Active;
            eventProcessor.OnStateChanged.Invoke(state);
            globalEventProcessor.VoiceOverEvent.Invoke(settingsModule.Settings.ActivationAudio);
        }
    }
}
