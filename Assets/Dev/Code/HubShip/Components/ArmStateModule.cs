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
        private IArmEventProcessorModule eventProcessor;
        private IArmSettingsModule settingsModule;
        private ArmState state;

        public ArmState State { get; }

        private void Awake()
        {
            eventProcessor = GetComponent<IArmEventProcessorModule>();
            settingsModule = GetComponent<IArmSettingsModule>();
            state = ArmState.Inactive;
        }

        private void OnEnable() =>
            eventProcessor.OnGameStageChanged.AddListener(HandleStageChanged);

        private void OnDisable() =>
            eventProcessor.OnGameStageChanged.RemoveListener(HandleStageChanged);

        private void HandleStageChanged(int stage)
        {
            if (state == ArmState.Active)
                return;

            if (stage < settingsModule.Settings.ActivationStage)
                return;

            state = ArmState.Active;
            eventProcessor.OnStateChanged.Invoke(state);
        }
    }
}
