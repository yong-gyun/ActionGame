using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    public Define.WorldObject WorldObjectType { get; protected set; }

    protected Animator _anim;
    protected Rigidbody _rb;
    protected bool _init;

    void Awake()
    {
        Init();    
    }

    protected virtual bool Init()
    {
        if (_init)
            return false;

        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        return true;
    }

    protected virtual void UpdateIdle() { }
    protected virtual void UpdateMove() { }
    protected virtual void UpdateAttack() { }
}
