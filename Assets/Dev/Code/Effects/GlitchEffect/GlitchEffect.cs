//using UnityEngine;

//[AddComponentMenu("Image Effects/GlitchEffect")]
//[RequireComponent(typeof(Camera))]
//public class GlitchEffect : ActionAnimator, IEffect<GlitchEffectArgs>
//{
//    [SerializeField] private Texture2D _displacementMap;
//    [SerializeField] private Shader _shader;

//    private GlitchEffectArgs _args;

//    private float _glitchup;
//    private float _glitchupTime = 0.05f;

//    private float _glitchdown;
//    private float _glitchdownTime = 0.05f;

//    private float flicker;
//    private float _flickerTime = 0.5f;
    
//    private Material _material;
//    protected Material Material
//    {
//        get
//        {
//            if (_material == null)
//            {
//                _material = new Material(_shader);
//                _material.hideFlags = HideFlags.HideAndDontSave;
//            }
//            return _material;
//        }
//    }

//    private bool _showGlitch;
//    private EventProcessor _eventProcessor;

//    private void Start()
//    {
//        _eventProcessor = EventProcessor.Instance;
//        _eventProcessor.Subscribe<GlitchEffectEvent, GlitchEffectArgs>(OnEventTriggered);
//    }

//    private void OnDestroy()
//    {
//        _eventProcessor.Unsubscribe<GlitchEffectEvent, GlitchEffectArgs>(OnEventTriggered);
//    }

//    // Called by camera to apply image effect
//    void OnRenderImage(RenderTexture source, RenderTexture destination)
//    {
//        if (!_showGlitch)
//            return;

//        Material.SetFloat("_Intensity", _args.Intensity);
//        Material.SetFloat("_ColorIntensity", _args.ColorIntensity);
//        Material.SetTexture("_DispTex", _displacementMap);

//        flicker += Time.deltaTime * _args.ColorIntensity;
//        if (flicker > _flickerTime)
//        {
//            Material.SetFloat("filterRadius", Random.Range(-3f, 3f) * _args.ColorIntensity);
//            Material.SetVector("direction", Quaternion.AngleAxis(Random.Range(0, 360) * _args.ColorIntensity, Vector3.forward) * Vector4.one);
//            flicker = 0;
//            _flickerTime = Random.value;
//        }

//        if (_args.ColorIntensity == 0)
//            Material.SetFloat("filterRadius", 0);

//        _glitchup += Time.deltaTime * _args.FlipIntensity;
//        if (_glitchup > _glitchupTime)
//        {
//            if (Random.value < 0.1f * _args.FlipIntensity)
//                Material.SetFloat("flip_up", Random.Range(0, 1f) * _args.FlipIntensity);
//            else
//                Material.SetFloat("flip_up", 0);

//            _glitchup = 0;
//            _glitchupTime = Random.value / 10f;
//        }

//        if (_args.FlipIntensity == 0)
//            Material.SetFloat("flip_up", 0);

//        _glitchdown += Time.deltaTime * _args.FlipIntensity;
//        if (_glitchdown > _glitchdownTime)
//        {
//            if (Random.value < 0.1f * _args.FlipIntensity)
//                Material.SetFloat("flip_down", 1 - Random.Range(0, 1f) * _args.FlipIntensity);
//            else
//                Material.SetFloat("flip_down", 1);

//            _glitchdown = 0;
//            _glitchdownTime = Random.value / 10f;
//        }

//        if (_args.FlipIntensity == 0)
//            Material.SetFloat("flip_down", 1);

//        if (Random.value < 0.05 * _args.Intensity)
//        {
//            Material.SetFloat("displace", Random.value * _args.Intensity);
//            Material.SetFloat("scale", 1 - Random.value * _args.Intensity);
//        }
//        else
//            Material.SetFloat("displace", 0);

//        Graphics.Blit(source, destination, _material);
//    }

//    private void OnEventTriggered(GlitchEffectArgs args) => Play(args);

//    public void Play(GlitchEffectArgs args)
//    {
//        _args = args;

//        var anim = new ActionAnimation(
//            new ActionAnimationEvent { TriggerAfterSeconds = args.Duration, AnimationEvent = () => _showGlitch = false }
//        );

//        _showGlitch = true;
//        Play(anim);
//    }
//}
