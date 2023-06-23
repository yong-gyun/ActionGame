using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util 
{
    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        if (go == null)
            return null;

        T component = go.GetComponent<T>();

        if (component == null)
            component = go.AddComponent<T>();

        return component;
    }

    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        if(recursive)
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if(string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }
        else
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                T component = transform.GetComponent<T>();

                if(component != null)
                {
                    if (string.IsNullOrEmpty(name) || component.name == name)
                        return component;
                }
            }
        }

        return null;
    }

    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);

        if (transform != null)
            return transform.gameObject;

        return null;
    }
}
