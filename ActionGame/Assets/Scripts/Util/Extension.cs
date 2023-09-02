using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extension
{
    public static T GetOrAddComponent<T>(this GameObject go) where T : Component
    {
        return Util.GetOrAddComponent<T>(go);
    }

    public static T FindChild<T> (this GameObject go, string name, bool recursive = false) where T : Object
    {
        return Util.FindChild<T> (go, name, recursive);
    }

    public static GameObject FindChild(this GameObject go, string name, bool recursive = false)
    {
        return Util.FindChild(go, name, recursive);
    }
}
