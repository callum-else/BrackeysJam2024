using UnityEngine;

namespace Assets.Environmental
{
    public class GlitchEffectPoolObjModule : MonoBehaviour, IGlitchEffectPoolObjModule
    {
        private Vector3 scale;

        private void Awake()
        {
            scale = transform.localScale;
        }

        public void ResetForPool()
        {
            transform.localScale = scale;
        }
    }
}