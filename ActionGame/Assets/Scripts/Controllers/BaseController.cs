using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected bool _init;

    private void Start()
    {
        Init();
    }

    protected virtual bool Init()
    {
        if (_init)
            return false;

        _init = true;
        return true;
    }

    protected virtual void Refresh() { }

    protected virtual void UpdateMove() { }
    protected virtual void UpdateAttack() { }
    protected virtual void OnDie() { }
}
