using TMPro;
using UnityEngine;

namespace Assets.UI
{
    public interface IIntroUIModuleReferences
    {
        TextMeshProUGUI IntroText { get; }
        GameObject IntroUIParent { get; }
    }
}