using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScene : MonoBehaviour
{
    public Define.Scene SceneType { get { return _sceneType; } }
    protected Define.Scene _sceneType;
    protected bool _init;

    private void Awake()
    {
        Init();
    }

    protected virtual bool Init()
    {
        if (_init)
            return false;

        GameObject evtSystem = GameObject.Find("EventSystem");

        if (evtSystem == null)
            Managers.Resource.Instantiate("UI/EventSystem");

        _init = true;
        return true;
    }
}
