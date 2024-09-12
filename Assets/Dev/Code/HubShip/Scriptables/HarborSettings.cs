using UnityEngine;

namespace Assets.HubShip
{
    [CreateAssetMenu(fileName = "New Arm Setting", menuName = "Scriptables/Hub Ship/Arm Settings")]
    public class HarborSettings : ScriptableObject, IHarborSettings
    {
        [SerializeField] private int activationStage;
        public int ActivationStage { get => activationStage; }

        [SerializeField] private AudioClip activationAudio;
        public AudioClip ActivationAudio { get => activationAudio; }
    }
}