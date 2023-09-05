using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance = null;
    static Managers Instance { get { Init(); return s_instance; } }

    ResourceManager _resource = new ResourceManager();
    UIManager _ui = new UIManager();
    ObjectManager _object = new ObjectManager();
    DataManager _data = new DataManager();
    GameManager _game = new GameManager();

    public static ResourceManager Resource { get { return Instance._resource; } }
    public static UIManager UI { get { return Instance._ui; } }
    public static ObjectManager Object { get { return Instance._object; } }
    public static DataManager Data { get { return Instance._data; } }
    public static GameManager Game { get { return Instance._game; } }

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");

            if(go == null)
            {
                go = new GameObject("@Managers");
                go.AddComponent<Managers>();
            }

            s_instance = go.GetComponent<Managers>();
            DontDestroyOnLoad(go);
        }
    }
}