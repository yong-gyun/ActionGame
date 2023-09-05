using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventHandler : MonoBehaviour
{
    Dictionary<string, Action> _animEnterEvents = new Dictionary<string, Action>();
    Dictionary<string, Action> _animExitEvents = new Dictionary<string, Action>();

    public static void OnEnter(Animator anim, int idx, Action action)
    {
        AnimationClip clip = anim.runtimeAnimatorController.animationClips[idx];
        string clipName = clip.name;

        clip.AddEvent(new AnimationEvent()
        {
            time = 0,
            functionName = "OnCompletedEnterEvent",
            stringParameter = clipName,
        });
    }

    public static void OnExit(Animator anim, int idx, Action action)
    {
        AnimationClip clip = anim.runtimeAnimatorController.animationClips[idx];
        string clipName = clip.name;

        clip.AddEvent(new AnimationEvent()
        {
            time = clip.length,
            functionName = "OnCompletedExitEvent",
            stringParameter = clipName,
        });
    }

    void OnCompletedEnterEvent(string clipName)
    {
        if (_animEnterEvents.Count == 0)
            return;

        _animEnterEvents[clipName].Invoke();
    }

    void OnCompletedExitEvent(string clipName)
    {
        if (_animExitEvents.Count == 0)
            return;

        _animExitEvents[clipName].Invoke();
    }
}
