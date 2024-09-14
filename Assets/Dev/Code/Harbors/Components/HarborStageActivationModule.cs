using Assets.Global;
using UnityEngine;

namespace Assets.HubShip
{
    public class HarborStageActivationModule : MonoBehaviour
    {
        private IGlobalEventProcessorModule gepm;
        private IHarborEventProcessorModule epm;
        private IHarborSettingsModule sm;
        private HarborState state;

        private void Awake()
        {
            gepm = GetComponent<IGlobalEventProcessorModule>();
            epm = GetComponent<IHarborEventProcessorModule>();
            sm = GetComponent<IHarborSettingsModule>();
            state = HarborState.Inactive;
        }

        private void OnEnable()
        {
            gepm.OnStageChanged.AddListener(HandleStageChanged);
        }

        private void OnDisable()
        {
            gepm.OnStageChanged.RemoveListener(HandleStageChanged);
        }

        private void HandleStageChanged(int stage)
        {
            if (state == HarborState.Active)
                return;

            if (stage < sm.Settings.ActivationStage)
                return;

            state = HarborState.Active;
            epm.OnStateChanged.Invoke(state);
        }
    }
}
