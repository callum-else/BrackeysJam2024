using UnityEngine;

namespace Assets.HubShip
{
    [CreateAssetMenu(fileName = "New Harbor Setting", menuName = "Scriptables/Hub Ship/Harbor Settings")]
    public class HarborSettings : ScriptableObject, IHarborSettings
    {
        [SerializeField] private int activationStage;
        public int ActivationStage => activationStage;

        [SerializeField] private AudioClip activationAudio;
        public AudioClip ActivationAudio => activationAudio;
    }
}