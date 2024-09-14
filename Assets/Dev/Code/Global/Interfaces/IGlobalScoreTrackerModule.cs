using UnityEngine.Events;

namespace Assets.Global
{
    public interface IGlobalScoreTrackerModule
    {
        UnityEvent<int> OnSoulsLostUpdated { get; }
        UnityEvent<int> OnSoulsSavedUpdated { get; }
        UnityEvent<float> OnThresholdPercentUpdated { get; }
        float TimeSurvived { get; set; }
    }
}