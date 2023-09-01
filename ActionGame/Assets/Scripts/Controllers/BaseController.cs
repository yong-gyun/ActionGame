using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    public Define.WorldObject WorldObjectType = Define.WorldObject.Unknow;
    protected bool _init;
    protected Animator _anim;

    private void Start()
    {
        Init();
    }

    protected virtual bool Init()
    {
        if (_init)
            return false;

        _anim = GetComponent<Animator>();
        _init = true;
        return true;
    }

    protected virtual void Refresh() { }
    protected virtual void UpdateMove() { }
    protected virtual void UpdateAttack() { }
    protected virtual void OnDie() { }
}
