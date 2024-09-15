using Assets.Common;
using Assets.Global;
using Assets.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IntroUIModule : AnimationModule
{
    private TextMeshProUGUI introText;
    private GameObject uiParent;
    private IGlobalEventProcessorModule gepm;
    private const float barLength = 2.0339f;

    private void Awake()
    {
        var refs = GetComponent<IIntroUIModuleReferences>();
        introText = refs.IntroText;
        introText.text = string.Empty;
        uiParent = refs.IntroUIParent;

        gepm = GetComponent<IGlobalEventProcessorModule>();
        gepm.IntroAnimationEvent.AddListener(HandleIntroAnimEvent);
    }

    private void HandleIntroAnimEvent(IIntroAnimationEventArgs args)
    {
        if (args.Phase != GlobalIntroAnimationPhase.Intro) return;
        Play(GetIntroTextAnim(), startTime: args.StartTime);
        gepm.IntroAnimationEvent.RemoveListener(HandleIntroAnimEvent);
    }

    private IEnumerable<AnimationStep> GetIntroTextAnim()
    {
        return new AnimationStep[]
        {
            new AnimationStep(barLength, _ => { }),
            new AnimationStep(barLength * 2, _ => introText.text = "THE STARS ARE DEAD."),
            new AnimationStep(barLength * 2, _ => introText.text = "THE STORM IS NEAR."),
            new AnimationStep(barLength * 2, _ => introText.text = "IT FEEDS ON SOULS."),
            new AnimationStep(barLength * 2, _ => introText.text = "YOU CAN'T SAVE THEM ALL."),
            new AnimationStep(0f, _ => uiParent.SetActive(false)),
        };
    }
}
