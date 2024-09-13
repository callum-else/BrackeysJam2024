using UnityEngine;

namespace Assets.HubShip
{
    public interface IHarborSettings
    {
        AudioClip ActivationAudio { get; }
        int ActivationStage { get; }
    }
}