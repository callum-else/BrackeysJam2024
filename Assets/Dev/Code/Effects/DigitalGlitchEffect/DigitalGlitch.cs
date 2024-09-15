using UnityEngine;

namespace Assets.Effects
{
    [AddComponentMenu("Image Effects/GlitchEffect")]
    [RequireComponent(typeof(Camera))]
    public class DigitalGlitch : MonoBehaviour, IDigitalGlitch
    {
        [SerializeField] private Shader shader;
        [SerializeField] private Texture2D displacementMap;

        private float glitchup;
        private float glitchupTime = 0.05f;

        private float glitchdown;
        private float glitchdownTime = 0.05f;

        private float flicker;
        private float flickerTime = 0.5f;

        private Material material;
        protected Material Material
        {
            get
            {
                if (material == null)
                {
                    material = new Material(shader);
                    material.hideFlags = HideFlags.HideAndDontSave;
                }
                return material;
            }
        }

        [SerializeField] private bool showGlitch;
        public bool ShowGlitch
        {
            get => showGlitch;
            set => showGlitch = value;
        }

        [SerializeField, Range(0, 1)] private float intensity;
        public float Intensity
        {
            get => intensity;
            set => intensity = value;
        }

        [SerializeField, Range(0, 1)] private float flipIntensity;
        public float FlipIntensity
        {
            get => flipIntensity;
            set => flipIntensity = value;
        }

        [SerializeField, Range(0, 1)] private float colorIntensity;
        public float ColorIntensity
        {
            get => colorIntensity;
            set => colorIntensity = value;
        }

        // Called by camera to apply image effect
        void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            if (!ShowGlitch)
            {
                Graphics.Blit(source, destination);
                return;
            }

            Material.SetFloat("_Intensity", Intensity);
            Material.SetFloat("_ColorIntensity", ColorIntensity);
            Material.SetTexture("_DispTex", displacementMap);

            flicker += Time.deltaTime * ColorIntensity;
            if (flicker > flickerTime)
            {
                Material.SetFloat("filterRadius", Random.Range(-3f, 3f) * ColorIntensity);
                Material.SetVector("direction", Quaternion.AngleAxis(Random.Range(0, 360) * ColorIntensity, Vector3.forward) * Vector4.one);
                flicker = 0;
                flickerTime = Random.value;
            }

            if (ColorIntensity == 0)
                Material.SetFloat("filterRadius", 0);

            glitchup += Time.deltaTime * FlipIntensity;
            if (glitchup > glitchupTime)
            {
                if (Random.value < 0.1f * FlipIntensity)
                    Material.SetFloat("flip_up", Random.Range(0, 1f) * FlipIntensity);
                else
                    Material.SetFloat("flip_up", 0);

                glitchup = 0;
                glitchupTime = Random.value / 10f;
            }

            if (FlipIntensity == 0)
                Material.SetFloat("flip_up", 0);

            glitchdown += Time.deltaTime * FlipIntensity;
            if (glitchdown > glitchdownTime)
            {
                if (Random.value < 0.1f * FlipIntensity)
                    Material.SetFloat("flip_down", 1 - Random.Range(0, 1f) * FlipIntensity);
                else
                    Material.SetFloat("flip_down", 1);

                glitchdown = 0;
                glitchdownTime = Random.value / 10f;
            }

            if (FlipIntensity == 0)
                Material.SetFloat("flip_down", 1);

            if (Random.value < 0.05 * Intensity)
            {
                Material.SetFloat("displace", Random.value * Intensity);
                Material.SetFloat("scale", 1 - Random.value * Intensity);
            }
            else
                Material.SetFloat("displace", 0);

            Graphics.Blit(source, destination, material);
        }
    }
}