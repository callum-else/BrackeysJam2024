using System;

namespace Assets.Common
{
    public interface IAnimationStep
    {
        float Duration { get; set; }
        Action<float> Step { get; set; }
    }
}