using Assets.Global;
using UnityEngine;

namespace Assets.HubShip
{
    public class HarborIntroAnimActivationModule : MonoBehaviour
    {
        private IGlobalEventProcessorModule gepm;
        private IHarborEventProcessorModule epm;

        private void Awake()
        {
            gepm = GetComponent<IGlobalEventProcessorModule>();
            epm = GetComponent<IHarborEventProcessorModule>();

            gepm.IntroAnimationEvent.AddListener(HandleIntroAnimEvent);
        }

        private void HandleIntroAnimEvent(IIntroAnimationEventArgs args)
        {
            if (args.Phase != GlobalIntroAnimationPhase.HarborBootSequence2) return;
            epm.OnStateChanged.Invoke(HarborState.Active);
            gepm.IntroAnimationEvent.RemoveListener(HandleIntroAnimEvent);
        }
    }
}
