using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseScene : MonoBehaviour
{
    public Define.SceneType CurrentSceneType { get { return _sceneType; } }
    protected Define.SceneType _sceneType;
    protected bool _init;

    private void Awake()
    {
        Init();
    }

    protected virtual bool Init()
    {
        if (_init)
            return false;

        EventSystem eventSystem = FindObjectOfType<EventSystem>();
        
        if(eventSystem == null)
            eventSystem = Managers.Resource.Instantiate("UI/EventSystem").GetComponent<EventSystem>();

        eventSystem.name = "@EventSystem";
        return true;
    }
}