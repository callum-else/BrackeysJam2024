using Assets.Common;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Environmental
{
    public class GlitchEffectAnimationModule : AnimationModule, IGlitchEffectAnimationModule
    {
        private Vector3 originScale;
        private IEnumerable<AnimationStep> spawnAnimation;

        private void Awake()
        {
            originScale = transform.localScale;
        }

        public void AnimateSpawn()
        {
            Play(GetSpawnAnimation());
        }

        private IEnumerable<AnimationStep> GetSpawnAnimation()
        {
            transform.localScale = Vector3.zero;
            return spawnAnimation ??= new AnimationStep[1]
            {
                new AnimationStep(1f, (x) => transform.DOScale(originScale, x))
            };
        }
    }
}