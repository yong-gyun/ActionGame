using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    Dictionary<string, Transform> _objects = new Dictionary<string, Transform>();

    public GameObject Spawn()
    {
        GameObject go = null;
        return go;
    }



    public void Clear()
    {
        _objects.Clear();
    }
}
