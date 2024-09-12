using UnityEngine;

namespace Assets.Environmental
{
    public class GlitchEffectPoolObjModule : MonoBehaviour, IGlitchEffectPoolObjModule
    {
        private IGlitchEffectAnimationModule animator;
        private new Renderer renderer;
        private new Collider collider;

        private void Awake()
        {
            animator = GetComponent<IGlitchEffectAnimationModule>();
            renderer = GetComponent<Renderer>();
            collider = GetComponent<Collider>();
        }

        public void EnableForPool(Vector3 location)
        {
            transform.position = location;
            animator.AnimateSpawn();
            renderer.enabled = true;
            collider.enabled = true;
        }

        public void DisableForPool()
        {
            renderer.enabled = false;
            collider.enabled = false;
        }
    }
}