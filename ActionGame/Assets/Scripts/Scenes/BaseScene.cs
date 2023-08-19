using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScene : MonoBehaviour
{
    public Define.Scene CurrentScene { get { return _currentScene; } }
    protected Define.Scene _currentScene;
    protected bool _init;

    private void Awake()
    {
        Init();
    }

    protected virtual bool Init()
    {
        if (_init)
            return false;

        GameObject go = GameObject.Find("@EventSystem");

        if (go == null)
            Managers.Resource.Instantiate("UI/EventSystem");

        _init = true;
        return true;
    }

    public virtual void Clear()
    {

    }
}
