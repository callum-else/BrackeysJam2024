using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UI
{
    public class UIReferences : MonoBehaviour, IGameplayUIModuleReferences, IIntroUIModuleReferences, IGameOverUIModuleReferences
    {
        [SerializeField] private TextMeshProUGUI soulsSavedScoreText;
        public TextMeshProUGUI SoulsSavedScoreText => soulsSavedScoreText;

        [SerializeField] private TextMeshProUGUI soulsLostScoreText;
        public TextMeshProUGUI SoulsLostScoreText => soulsLostScoreText;

        [SerializeField] private TextMeshProUGUI soulsLostScoreTitle;
        public TextMeshProUGUI SoulsLostScoreTitle => soulsLostScoreTitle;

        [SerializeField] private TextMeshProUGUI timeSurvivedScoreText;
        public TextMeshProUGUI TimeSurvivedScoreText => timeSurvivedScoreText;

        [SerializeField] private GameObject gameplayUIParent;
        public GameObject GameplayUIParent => gameplayUIParent;

        [Space]

        [SerializeField] private TextMeshProUGUI introText;
        public TextMeshProUGUI IntroText => introText;

        [SerializeField] private GameObject introUIParent;
        public GameObject IntroUIParent => introUIParent;

        [Space]

        [SerializeField] private GameObject gameOverUIParent;
        public GameObject GameOverUIParent => gameOverUIParent;

        [SerializeField] private TextMeshProUGUI gameOverSoulsLostText;
        public TextMeshProUGUI GameOverSoulsLostText => gameOverSoulsLostText;

        [SerializeField] private TextMeshProUGUI gameOverSoulsSavedText;
        public TextMeshProUGUI GameOverSoulsSavedText => gameOverSoulsSavedText;

        [SerializeField] private TextMeshProUGUI gameOverTimeSurvivedText;
        public TextMeshProUGUI GameOverTimeSurvivedText => gameOverTimeSurvivedText;

        [SerializeField] private Button gameOverQuitButton;
        public Button GameOverQuitButton => gameOverQuitButton;
    }
}