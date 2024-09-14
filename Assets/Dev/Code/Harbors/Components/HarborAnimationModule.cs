using Assets.Common;
using Assets.Effects;
using Assets.Global;
using DG.Tweening;
using UnityEngine;

namespace Assets.HubShip
{
    public class HarborAnimationModule : AnimationModule
    {
        private IWireframeAnimationModule wam;
        private IHarborEventProcessorModule epm;
        private IGlobalEventProcessorModule gepm;
        private IHarborSettingsModule sm;
        private AnimationStep[] activationAnim;

        private Renderer arm;
        private Renderer bubble;
        private Vector3 bubbleScale;
        private Collider bubbleCollider;

        private Material active;
        private Material inactive;

        private void Awake()
        {
            wam = GetComponent<IWireframeAnimationModule>();
            gepm = GetComponent<IGlobalEventProcessorModule>();
            epm = GetComponent<IHarborEventProcessorModule>();
            sm = GetComponent<IHarborSettingsModule>();
            var refs = GetComponent<IHarborReferences>();
            var mm = GetComponent<IMaterialModule>();

            arm = refs.ArmRenderer;
            mm.ApplyUniqueMaterial(arm);

            bubble = refs.BubbleRenderer;
            mm.ApplyUniqueMaterial(bubble);
            bubbleScale = bubble.transform.localScale;
            bubble.transform.localScale = Vector3.zero;
            
            bubbleCollider = refs.BubbleCollider;
            bubbleCollider.enabled = false;

            active = refs.ActiveMaterial;
            inactive = refs.InactiveMaterial;

            //HandleStateChanged(ArmState.Active);
        }

        private void OnEnable() =>
            epm.OnStateChanged.AddListener(HandleStateChanged);

        private void OnDisable() =>
            epm.OnStateChanged.RemoveListener(HandleStateChanged);

        private void HandleStateChanged(HarborState state)
        {
            if (state == HarborState.Active)
                Play(GetActivationAnim());
        }

        private void BlendWireframeMats(Material src, Material target, float duration)
        {
            var targetColor = wam.GetWireColor(target);
            wam.DOWithProp(WireframeProps.WireColor, targetColor, duration, src.DOColor);

            var targetEmission = wam.GetEmission(target);
            wam.DOWithProp(WireframeProps.Emission, targetEmission, duration, src.DOFloat);
        }

        private AnimationStep[] GetActivationAnim() => activationAnim ??= new AnimationStep[]
        {
            new AnimationStep(1f, (x) => 
            {
                gepm.VoiceOverEvent.Invoke(sm.Settings.ActivationAudio);
                bubble.transform.localScale = Vector3.zero;
                BlendWireframeMats(arm.material, active, x);
            }),
            new AnimationStep(0.5f, (x) =>
            {
                wam.DOWithProp(WireframeProps.WireColor, 1f, x, bubble.material.DOFade);
                bubble.transform.DOScale(bubbleScale, x);
            }),
            new AnimationStep(0.5f, (_) => 
            {
                bubbleCollider.enabled = true; 
            })
        };
    }
}