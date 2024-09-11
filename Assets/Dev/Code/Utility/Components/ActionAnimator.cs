using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActionAnimation : IEnumerable<ActionAnimationEvent>
{
    private IEnumerable<ActionAnimationEvent> _animtion;
    private int _loops = 0;
    private uint _index = 0;
    private int? _count;

    public int Loops
    {
        get => _loops;
    }

    public int Count
    {
        get => _count ??= _animtion.Count();
    }

    public uint Index
    {
        get => _index;
    }

    public ActionAnimation(IEnumerable<ActionAnimationEvent> animationEvents)
    {
        _animtion = animationEvents;
    }

    public ActionAnimation(params ActionAnimationEvent[] animationEvents)
    {
        _animtion = animationEvents;
    }

    public ActionAnimationEvent this[int i]
    {
        get => _animtion.ElementAt(i);
    }

    public ActionAnimation SetLoops(int loops)
    {
        _loops = loops;
        return this;
    }

    public void IncrementIndex() => _index++;
    public void SetIndex(uint index) => _index = index;

    public IEnumerator<ActionAnimationEvent> GetEnumerator()
    {
        return _animtion.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _animtion.GetEnumerator();
    }
}

public abstract class ActionAnimator : MonoBehaviour
{
    private bool _isPlaying = false;
    private float _nextAnimationTime;
    private ActionAnimation _animation;
    private int _animationIndex;

    protected bool IsPlaying
    {
        get => _isPlaying;
    }

    protected void Play(ActionAnimation animation)
    {
        SetAnimation(animation);
        Play();
    }

    protected void Play()
    {
        if (_animation != null)
        {
            _nextAnimationTime = Time.time + _animation[0].TriggerAfterSeconds;
            _animationIndex = 0;
            _isPlaying = true;
        }
        else
            print("Could not play effect animator.");
    }

    protected void Stop()
    {
        _isPlaying = false;
    }

    private void SetAnimation(ActionAnimation animation)
    {
        Stop();
        _nextAnimationTime = 0;
        _animation = animation;
    }

    private void FixedUpdate()
    {
        if (_isPlaying && Time.time >= _nextAnimationTime)
        {
            var currAnim = _animation[_animationIndex];
            currAnim.AnimationEvent.Invoke();

            _nextAnimationTime += currAnim.DurationSeconds;

            if (++_animationIndex < _animation.Count())
            {
                _nextAnimationTime += _animation[_animationIndex].TriggerAfterSeconds;
                return;
            }

            if (_animation.Loops != 0)
            {
                _animationIndex = 0;

                if (_animation.Loops != -1)
                    _animation.SetLoops(_animation.Loops - 1);

                return;
            }

            _isPlaying = false;
        }
    }
}

[Serializable]
public class ActionAnimationEvent
{
    public float TriggerAfterSeconds = 0f;
    public float DurationSeconds = 0f;
    public Action AnimationEvent;
}