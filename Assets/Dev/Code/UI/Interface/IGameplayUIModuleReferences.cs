using TMPro;
using UnityEngine;

namespace Assets.UI
{
    public interface IGameplayUIModuleReferences
    {
        TextMeshProUGUI SoulsLostScoreText { get; }
        TextMeshProUGUI SoulsLostScoreTitle { get; }
        TextMeshProUGUI SoulsSavedScoreText { get; }
        TextMeshProUGUI TimeSurvivedScoreText { get; }
        GameObject GameplayUIParent { get; }
    }
}