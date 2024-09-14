using TMPro;
using UnityEngine;

namespace Assets.UI
{
    public class UIReferences : MonoBehaviour, IGameplayUIModuleReferences
    {
        [SerializeField] private TextMeshProUGUI soulsSavedScoreText;
        public TextMeshProUGUI SoulsSavedScoreText => soulsSavedScoreText;

        [SerializeField] private TextMeshProUGUI soulsLostScoreText;
        public TextMeshProUGUI SoulsLostScoreText => soulsLostScoreText;

        [SerializeField] private TextMeshProUGUI soulsLostScoreTitle;
        public TextMeshProUGUI SoulsLostScoreTitle => soulsLostScoreTitle;

        [SerializeField] private TextMeshProUGUI timeSurvivedScoreText;
        public TextMeshProUGUI TimeSurvivedScoreText => timeSurvivedScoreText;
    }
}