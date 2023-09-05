using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extension
{
    public static T GetOrAddComponent<T>(this GameObject go) where T : Component
    {
        return Util.GetOrAddComponent<T>(go);
    }

    public static T FindChild<T> (this GameObject go, string name = null, bool recursive = true) where T : UnityEngine.Object
    {
        return Util.FindChild<T> (go, name, recursive);
    }

    public static GameObject FindChild(this GameObject go, string name = null, bool recursive = true)
    {
        return Util.FindChild(go, name, recursive);
    }

    public static void BindAnimEvent(this GameObject go, int idx, Action action, Define.AnimEvent type)
    {
        Animator anim = go.GetComponent<Animator>();

        if (anim == null)
            return;

        switch(type)
        {
            case Define.AnimEvent.Enter:
                AnimEventHandler.OnEnter(anim, idx, action);
                break;
            case Define.AnimEvent.Exit:
                AnimEventHandler.OnExit(anim, idx, action);
                break;
        }
    }
}
