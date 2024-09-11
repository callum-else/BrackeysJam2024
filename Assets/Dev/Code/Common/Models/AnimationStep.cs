using System;

namespace Assets.Common
{
    public class AnimationStep : IAnimationStep
    {
        public float Duration { get; set; }
        public Action<float> Step { get; set; }

        public AnimationStep(float duration, Action<float> step)
        {
            Duration = duration;
            Step = step;
        }
    }
}