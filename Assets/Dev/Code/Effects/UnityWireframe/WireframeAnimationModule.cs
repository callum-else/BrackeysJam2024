using DG.Tweening;
using System;
using UnityEngine;

namespace Assets.Effects
{
    public enum WireframeProps
    {
        WireThickness,
        WireSmoothness,
        WireColor,
        BaseColor,
        Emission
    }

    public class WireframeAnimationModule : MonoBehaviour, IWireframeAnimationModule
    {
        private const string wireThickness = "_WireThickness";
        private const string wireSmoothness = "_WireSmoothness";
        private const string wireColor = "_WireColor";
        private const string baseColor = "_BaseColor";
        private const string emission = "_Emission";

        private string GetPropName(WireframeProps prop)
        {
            switch (prop)
            {
                case WireframeProps.WireThickness:  return wireThickness;
                case WireframeProps.WireSmoothness: return wireSmoothness;
                case WireframeProps.WireColor:      return wireColor;
                case WireframeProps.BaseColor:      return baseColor;
                case WireframeProps.Emission:       return emission;
                default: throw new NotImplementedException(prop.ToString());
            }
        }

        public TOut DOWithProp<T, TOut>(WireframeProps prop, T targetValue, float duration, Func<T, string, float, TOut> tweener) =>
            tweener.Invoke(targetValue, GetPropName(prop), duration);

        public float GetThickness(Material src) => src.GetFloat(wireThickness);
        public float GetSmoothness(Material src) => src.GetFloat(wireSmoothness);
        public Color GetWireColor(Material src) => src.GetColor(wireColor);
        public Color GetBaseColor(Material src) => src.GetColor(baseColor);
        public float GetEmission(Material src) => src.GetFloat(emission);
    }
}