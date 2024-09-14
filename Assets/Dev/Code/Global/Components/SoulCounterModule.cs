using Assets.Global;
using UnityEngine;

public class SoulCounterModule : MonoBehaviour
{
    private int soulsSaved;
    private int soulsLost;
    private IGlobalEventProcessorModule gepm;

    private void Awake()
    {
        gepm = GetComponent<IGlobalEventProcessorModule>();
    }

    private void OnEnable()
    {
        gepm.OnShipCrashed.AddListener(HandleOnShipCrashed);
        gepm.OnShipGlitched.AddListener(HandleOnShipGlitched);
        gepm.OnShipSaved.AddListener(HandleOnShipSaved);
    }

    private void OnDisable()
    {
        gepm.OnShipCrashed.RemoveListener(HandleOnShipCrashed);
        gepm.OnShipGlitched.RemoveListener(HandleOnShipGlitched);
        gepm.OnShipSaved.RemoveListener(HandleOnShipSaved);
    }

    private void HandleOnShipCrashed(IShipCrashedEventArgs args) => IncrementLostSouls(args.Value);
    private void HandleOnShipGlitched(IShipGlitchedEventArgs args) => IncrementLostSouls(args.Value);

    private void IncrementLostSouls(int value)
    {
        soulsLost += value;

        // if (soulsLost > threshold)
        //     game over
    }

    private void HandleOnShipSaved(IShipSavedEventArgs args) => soulsSaved += args.Value;
}
