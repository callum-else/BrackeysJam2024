using UnityEngine;

namespace Assets.HubShip
{
    public class HarborSettingsModule : MonoBehaviour, IHarborSettingsModule
    {
        [SerializeField] private HarborSettings settings;
        public IHarborSettings Settings { get => settings; }
    }
}