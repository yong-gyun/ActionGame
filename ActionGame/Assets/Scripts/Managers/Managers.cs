using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers Instance { get { Init(); return s_instance; } }
    static Managers s_instance = null;

    ResourceManager _resource = new ResourceManager();
    GameManager _game = new GameManager();

    public static ResourceManager Resource { get { return Instance._resource; } }
    public static GameManager Game { get { return Instance._game; } }

    static void Init()
    {
        if(s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");

            if(go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            s_instance = go.GetComponent<Managers>();
            DontDestroyOnLoad(go);
        }
    }
}