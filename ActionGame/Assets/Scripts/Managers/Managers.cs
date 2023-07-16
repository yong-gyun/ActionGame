using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers Instance { get { Init(); return s_instance; } }
    static Managers s_instance = null;

    InputManager _input = new InputManager();
    ResourceManager _resource = new ResourceManager();
    ObjectManager _object = new ObjectManager();
    GameManager _game = new GameManager();

    public static InputManager Input { get { return Instance._input; } }
    public static ResourceManager Reosurce { get { return Instance._resource; } }
    public static ObjectManager Object { get { return Instance._object; } }
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

            DontDestroyOnLoad(go);
        }
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        _input.OnUpdate();
    }

    public static void Clear()
    {
        s_instance._object.Clear();
        s_instance._input.Clear();
    }
}
