using Assets.Common;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Environmental
{
    public class GlitchEffectAnimationModule : MonoBehaviour, IGlitchEffectAnimationModule
    {
        private Vector3 originScale;
        private Vector3 targetScale;
        private int sizeDelta;
        private IEnumerable<AnimationStep> spawnAnimation;

        private void Awake()
        {
            originScale = transform.localScale;
        }

        public void AnimateSpawn(int delta)
        {
            sizeDelta = delta;
            transform.localScale = Vector3.zero;
            targetScale = CalculateTargetScale();
        }

        private Vector3 CalculateTargetScale() =>
            originScale * (1 + (sizeDelta * 0.01f));

        public void ApplyScaleDelta(int delta)
        {
            if (sizeDelta > 150)
                return;

            sizeDelta += delta;
            targetScale = CalculateTargetScale();
        }

        private void FixedUpdate()
        {
            if (transform.localScale == targetScale)
                return;

            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, 0.1f);
            
            if (Vector3.Distance(transform.localScale, targetScale) < 0.1f)
                transform.localScale = targetScale;
        }
    }
}