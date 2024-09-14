using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UI
{
    public interface IGameOverUIModuleReferences
    {
        GameObject GameOverUIParent { get; }
        TextMeshProUGUI GameOverSoulsLostText { get; }
        TextMeshProUGUI GameOverSoulsSavedText { get; }
        TextMeshProUGUI GameOverTimeSurvivedText { get; }
        Button GameOverQuitButton { get; }
    }
}