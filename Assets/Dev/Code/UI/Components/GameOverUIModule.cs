using Assets.Global;
using Assets.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUIModule : MonoBehaviour
{
    private IGlobalEventProcessorModule gepm;
    private GameObject uiParent;
    private TextMeshProUGUI soulsLostText;
    private TextMeshProUGUI soulsSavedText;
    private TextMeshProUGUI timeSurvivedText;
    private Button quitButton;

    private void Awake()
    {
        var refs = GetComponent<IGameOverUIModuleReferences>();

        uiParent = refs.GameOverUIParent;
        soulsLostText = refs.GameOverSoulsLostText;
        soulsSavedText = refs.GameOverSoulsSavedText;
        timeSurvivedText = refs.GameOverTimeSurvivedText;

        refs.GameOverQuitButton.onClick.AddListener(Application.Quit);

        gepm = GetComponent<IGlobalEventProcessorModule>();
        gepm.GameOverEvent.AddListener(HandleGameOverEvent);
        gepm.GameOverAnimationEvent.AddListener(HandleGameOverAnimEvent);
    }

    private void HandleGameOverEvent(IGameOverEventArgs args)
    {
        soulsLostText.text = $"{args.SoulsLost:0000}";
        soulsSavedText.text = $"{args.SoulsSaved:0000}";
        timeSurvivedText.text = $"{args.TimeSurvived:0.00}";
        gepm.GameOverEvent.RemoveListener(HandleGameOverEvent);
    }

    private void HandleGameOverAnimEvent(GlobalGameOverAnimationPhase phase)
    {
        if (phase != GlobalGameOverAnimationPhase.ShowGameOverScreen) return;
        uiParent.SetActive(true);
        gepm.GameOverAnimationEvent.RemoveListener(HandleGameOverAnimEvent);
    }
}
