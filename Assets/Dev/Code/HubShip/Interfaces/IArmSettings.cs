using UnityEngine;

namespace Assets.HubShip
{
    public interface IArmSettings
    {
        AudioClip ActivationAudio { get; }
        int ActivationStage { get; }
    }
}