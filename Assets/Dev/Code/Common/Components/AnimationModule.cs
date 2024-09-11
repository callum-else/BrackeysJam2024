using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Common
{
    public abstract class AnimationModule : MonoBehaviour
    {
        private new IAnimationStep[] animation;
        private int loops = 0;

        private bool isPlaying = false;
        private float nextInvoke = 0f;
        private int index = 0;
        private int loop = 0;

        protected void Play(IEnumerable<IAnimationStep> animation, int loops = 0)
        {
            SetDefaults();

            this.animation = animation.ToArray();
            this.loops = loops;

            isPlaying = true;
        }

        private void SetDefaults()
        {
            isPlaying = false;
            nextInvoke = 0f;
            index = 0;
            loop = 0;
        }

        private void HandleAnimation()
        {
            if (!isPlaying) return;
            if (Time.fixedTime < nextInvoke) return;

            var anim = animation[index++];
            anim.Step.Invoke(anim.Duration);
            nextInvoke = Time.fixedTime + anim.Duration;

            if (index < animation.Length) return;
            switch (loops)
            {
                case 0:
                    SetDefaults();
                    return;
                case -1:
                    index = 0;
                    return;
                default:
                    if (++loop > loops)
                        SetDefaults();
                    return;
            }
        }

        protected virtual void FixedUpdate()
        {
            HandleAnimation();
        }
    }
}