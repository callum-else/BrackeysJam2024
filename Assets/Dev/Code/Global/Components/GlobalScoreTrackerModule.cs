using UnityEngine;
using UnityEngine.Events;

namespace Assets.Global
{
    public class GlobalScoreTrackerModule : MonoBehaviour, IGlobalScoreTrackerModule
    {
        private static UnityEvent<float> onThresholdPercentUpdated;
        public UnityEvent<float> OnThresholdPercentUpdated => onThresholdPercentUpdated ??= new();

        private static UnityEvent<int> onSoulsLostUpdated;
        public UnityEvent<int> OnSoulsLostUpdated => onSoulsLostUpdated ??= new();

        private static UnityEvent<int> onSoulsSavedUpdated;
        public UnityEvent<int> OnSoulsSavedUpdated => onSoulsSavedUpdated ??= new();

        private static float timeSurvived;
        public float TimeSurvived
        {
            get => timeSurvived;
            set => timeSurvived = value;
        }
    }
}