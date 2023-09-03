using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object
    {
        T origin = Resources.Load<T>(path);

        if (origin == null)
            return null;

        return origin;
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject origin = Load<GameObject>($"Prefabs/{path}");
        
        if(origin == null)
        {
            Debug.Log($"Faild load {path}");
            return null;
        }

        GameObject go = Object.Instantiate(origin, parent);

        go.transform.parent = parent;
        go.name = origin.name;
        return go;
    }

    public GameObject Instantiate(string path, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        GameObject go = Instantiate(path, parent);
        go.transform.position = position;
        go.transform.rotation = rotation;
        return go;
    }

    public void Destory(GameObject go, float time = 0)
    {
        if (go == null)
            return;

        Object.Destroy(go, time);
    }
}
