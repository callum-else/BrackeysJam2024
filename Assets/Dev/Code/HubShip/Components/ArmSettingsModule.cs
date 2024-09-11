using Assets.Common;
using UnityEngine;

namespace Assets.HubShip
{
    public class ArmSettingsModule : MonoBehaviour, IArmSettingsModule
    {
        [SerializeField] private ArmSettings settings;
        public IArmSettings Settings { get => settings; }
    }
}