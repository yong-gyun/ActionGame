using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ResourceManager
{
    public T Load<T> (string path) where T : UnityEngine.Object
    {
        T origin = Resources.Load<T>(path);
        
        if (origin == null)
        {
            Debug.Log($"Faild load {path}");
            return null;
        }

        return origin;
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject origin = Load<GameObject>($"Prefabs/{path}");

        if (origin == null)
            return null;

        GameObject go = Object.Instantiate(origin, parent);
        return go;
    }

    public void Destroy(GameObject go, float time = 0)
    {
        if (go == null)
            return;

        Object.Destroy(go, time);
    }
}
