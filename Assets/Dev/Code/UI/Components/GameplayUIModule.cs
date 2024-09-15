using Assets.Global;
using TMPro;
using UnityEngine;

namespace Assets.UI
{
    public class GameplayUIModule : MonoBehaviour
    {
        private IGlobalEventProcessorModule gepm;
        private IGlobalScoreTrackerModule gscm;
        private GameObject uiParent;
        private TextMeshProUGUI soulsSavedScoreText;
        private TextMeshProUGUI soulsLostScoreText;
        private TextMeshProUGUI soulsLostScoreTitle;
        private TextMeshProUGUI timeSurvivedScoreText;
        private Color soulsLostTextCol;
        private Color soulsLostTitleCol;

        private void Awake()
        {
            gepm = GetComponent<IGlobalEventProcessorModule>();
            gscm = GetComponent<IGlobalScoreTrackerModule>();

            var refs = GetComponent<IGameplayUIModuleReferences>();
            uiParent = refs.GameplayUIParent;
            soulsSavedScoreText = refs.SoulsSavedScoreText;
            soulsLostScoreText = refs.SoulsLostScoreText;
            soulsLostScoreTitle = refs.SoulsLostScoreTitle;
            timeSurvivedScoreText = refs.TimeSurvivedScoreText;

            soulsLostTextCol = soulsLostScoreText.color;
            soulsLostTitleCol = soulsLostScoreTitle.color;

            gepm.GameOverAnimationEvent.AddListener(HandleGameOverAnimEvent);
        }

        private void OnEnable()
        {
            gscm.OnSoulsLostUpdated.AddListener(HandleOnSoulsLostScoreUpdated);
            gscm.OnSoulsSavedUpdated.AddListener(HandleOnSoulsSavedScoreUpdated);
            gscm.OnThresholdPercentUpdated.AddListener(HandleOnThresholdUpdated);
        }

        private void OnDisable()
        {
            gscm.OnSoulsLostUpdated.RemoveListener(HandleOnSoulsLostScoreUpdated);
            gscm.OnSoulsSavedUpdated.RemoveListener(HandleOnSoulsSavedScoreUpdated);
            gscm.OnThresholdPercentUpdated.RemoveListener(HandleOnThresholdUpdated);
        }

        private void FixedUpdate()
        {
            timeSurvivedScoreText.text = $"{gscm.TimeSurvived:0.0}";
        }

        private void HandleOnSoulsSavedScoreUpdated(int value) => soulsSavedScoreText.text = $"{value:0000}";
        private void HandleOnSoulsLostScoreUpdated(int value) => soulsLostScoreText.text = $"{value:0000}";
        private void HandleOnThresholdUpdated(float value)
        {
            soulsLostScoreText.color = Color.Lerp(soulsLostTextCol, Color.red, value);
            soulsLostScoreTitle.color = Color.Lerp(soulsLostTitleCol, Color.red, value);
        }

        private void HandleGameOverAnimEvent(GlobalGameOverAnimationPhase phase)
        {
            if (phase != GlobalGameOverAnimationPhase.ShowGameOverScreen) return;
            uiParent.SetActive(false);
            gepm.GameOverAnimationEvent.RemoveListener(HandleGameOverAnimEvent);
        }
    }
}