using System;
using UnityEngine;

namespace Assets.Effects
{
    public interface IWireframeAnimationModule
    {
        TOut DOWithProp<T, TOut>(WireframeProps prop, T targetValue, float duration, Func<T, string, float, TOut> tweener);
        Color GetBaseColor(Material src);
        float GetEmission(Material src);
        float GetSmoothness(Material src);
        float GetThickness(Material src);
        Color GetWireColor(Material src);
    }
}