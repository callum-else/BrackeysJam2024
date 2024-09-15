using UnityEngine;

namespace Assets.Global
{
    public class SfxModule : MonoBehaviour
    {
        private IGlobalEventProcessorModule gepm;

        private AudioSource audioSource;
        private AudioClip[] glitchFx;
        private AudioClip[] savedFx;

        private void Awake()
        {
            gepm = GetComponent<IGlobalEventProcessorModule>();
            
            var refs = GetComponent<ISfxReferences>();
            audioSource = refs.SfxSource;
            glitchFx = refs.GlitchFx;
            savedFx = refs.SavedFx;
        }

        private void OnEnable()
        {
            gepm.OnShipCrashed.AddListener(HandleOnShipCrashEvent);
            gepm.OnShipGlitched.AddListener(HandleOnShipGlitchedEvent);
            gepm.OnShipSaved.AddListener(HandleOnShipSavedEvent);
        }

        private void OnDisable()
        {
            gepm.OnShipCrashed.RemoveListener(HandleOnShipCrashEvent);
            gepm.OnShipGlitched.RemoveListener(HandleOnShipGlitchedEvent);
            gepm.OnShipSaved.RemoveListener(HandleOnShipSavedEvent);
        }

        private void HandleOnShipCrashEvent(IShipCrashedEventArgs args) => PlayGlitchFx();
        private void HandleOnShipGlitchedEvent(IShipGlitchedEventArgs args) => PlayGlitchFx();

        private void PlayGlitchFx() => 
            audioSource.PlayOneShot(glitchFx[Random.Range(0, glitchFx.Length)]);

        private void HandleOnShipSavedEvent(IShipSavedEventArgs args) => PlaySavedFx();

        private void PlaySavedFx() =>
            audioSource.PlayOneShot(savedFx[Random.Range(0, savedFx.Length)]);
    }
}