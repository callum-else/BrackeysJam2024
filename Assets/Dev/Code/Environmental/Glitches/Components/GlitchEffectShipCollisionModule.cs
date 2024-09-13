using Assets.Global;
using UnityEngine;

namespace Assets.Environmental
{
    public class GlitchEffectShipCollisionModule : MonoBehaviour
    {
        private IGlitchEffectAnimationModule animator;
        private IGlobalEventProcessorModule gepm;
        private int colInstanceID;

        private void Awake()
        {
            animator = GetComponent<IGlitchEffectAnimationModule>();
            gepm = GetComponent<IGlobalEventProcessorModule>();
            colInstanceID = GetComponent<ICollisionModule>().GetInstanceID();
        }

        private void OnEnable() =>
            gepm.OnShipGlitched.AddListener(HandleGlitchCollision);

        private void OnDisable() =>
            gepm.OnShipGlitched.RemoveListener(HandleGlitchCollision);

        private void HandleGlitchCollision(IShipGlitchedEventArgs args)
        {
            if (colInstanceID != args.InstanceID)
                return;

            animator.ApplyScaleDelta(args.Value);
        }
    }
}