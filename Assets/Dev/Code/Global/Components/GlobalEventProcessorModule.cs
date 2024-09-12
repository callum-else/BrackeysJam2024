using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IShipDestroyedEventArgs
{

}

public interface IShipSavedEventArgs
{

}

public interface IGlobalEventProcessorModule
{
    UnityEvent<IShipDestroyedEventArgs> OnShipDestroyed { get; }
    UnityEvent<IShipSavedEventArgs> OnShipSaved { get; }
    UnityEvent<int> OnStageChanged { get; }
}

public static class GlobalEventProcessorLogic
{
    private static UnityEvent<IShipDestroyedEventArgs> onShipDestroyed;
    public static UnityEvent<IShipDestroyedEventArgs> OnShipDestroyed => onShipDestroyed ??= new();

    private static UnityEvent<IShipSavedEventArgs> onShipSaved;
    public static UnityEvent<IShipSavedEventArgs> OnShipSaved => onShipSaved ??= new();

    private static UnityEvent<int> onStageChanged;
    public static UnityEvent<int> OnStageChanged => onStageChanged ??= new();
}

public class GlobalEventProcessorModule : MonoBehaviour, IGlobalEventProcessorModule
{
    public UnityEvent<IShipDestroyedEventArgs> OnShipDestroyed => GlobalEventProcessorLogic.OnShipDestroyed;
    public UnityEvent<IShipSavedEventArgs> OnShipSaved => GlobalEventProcessorLogic.OnShipSaved;
    public UnityEvent<int> OnStageChanged => GlobalEventProcessorLogic.OnStageChanged;
}
