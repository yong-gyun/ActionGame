using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers Instance { get { Init(); return s_instance; } }
    static Managers s_instance = null;

    ResourceManager _resource = new ResourceManager();
    ObjectManager _object = new ObjectManager();
    UIManager _ui = new UIManager();
    DataManager _data = new DataManager();
    GameManager _game = new GameManager();

    public static ResourceManager Resource { get { return Instance._resource; } }
    public static ObjectManager Object { get { return Instance._object; } }
    public static UIManager UI { get { return Instance._ui; } }
    public static DataManager Data { get { return Instance._data; } }
    public static GameManager Game { get { return Instance._game; } }

    static void Init()
    {
        if(s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");

            if (go == null)
            {
                go = new GameObject("@Managers");
                go.AddComponent<Managers>();
            }

            s_instance = go.GetComponent<Managers>();
            s_instance._data.Init();
            DontDestroyOnLoad(go);
        }
    }

    void Start()
    {
        Init();
    }

    public static void Clear()
    {
        s_instance._object.Clear();
        s_instance._ui.Clear();
        s_instance._data.Clear();
    }
}
