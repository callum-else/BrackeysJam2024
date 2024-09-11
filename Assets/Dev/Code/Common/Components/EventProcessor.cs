using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

public interface IEvent { }

public abstract class Event : IEvent
{
    private UnityEvent _event;

    public Event()
    {
        _event = new UnityEvent();
    }

    public void Subscribe(UnityAction action) => _event.AddListener(action);
    public void Unsubscribe(UnityAction action) => _event.RemoveListener(action);
    public void Publish() => _event.Invoke();
}

public abstract class Event<T> : IEvent
{
    private UnityEvent<T> _event;

    public Event()
    {
        _event = new UnityEvent<T>();
    }

    public void Subscribe(UnityAction<T> action) => _event.AddListener(action);
    public void Unsubscribe(UnityAction<T> action) => _event.RemoveListener(action);
    public void Publish(T data) => _event.Invoke(data);
}

public class EventProcessor : Singleton<EventProcessor>
{
    private List<IEvent> _events = new List<IEvent>();

    private T GetEvent<T>() where T : class, IEvent, new()
    {
        var ev = _events.FirstOrDefault(x => x.GetType() == typeof(T));

        if (ev == null)
        {
            ev = new T();
            _events.Add(ev);
        }

        return ev as T;
    }

    public void Subscribe<T>(UnityAction action) where T : Event, new()
    {
        GetEvent<T>().Subscribe(action);
    }

    public void Subscribe<TEvent, TArg>(UnityAction<TArg> action) where TEvent : Event<TArg>, new()
    {
        GetEvent<TEvent>().Subscribe(action);
    }

    public void Unsubscribe<T>(UnityAction action) where T : Event, new()
    {
        GetEvent<T>().Unsubscribe(action);
    }

    public void Unsubscribe<TEvent, TArg>(UnityAction<TArg> action) where TEvent : Event<TArg>, new()
    {
        GetEvent<TEvent>().Unsubscribe(action);
    }

    public void Publish<T>() where T : Event, new()
    {
        GetEvent<T>().Publish();
    }

    public void Publish<TEvent,TArg>(TArg args) where TEvent : Event<TArg>, new()
    {
        GetEvent<TEvent>().Publish(args);
    }
}