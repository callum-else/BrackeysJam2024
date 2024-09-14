using TMPro;

namespace Assets.UI
{
    public interface IGameplayUIModuleReferences
    {
        TextMeshProUGUI SoulsLostScoreText { get; }
        TextMeshProUGUI SoulsLostScoreTitle { get; }
        TextMeshProUGUI SoulsSavedScoreText { get; }
        TextMeshProUGUI TimeSurvivedScoreText { get; }
    }
}