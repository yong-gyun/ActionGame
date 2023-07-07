using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    protected Animator _anim;
    [SerializeField] protected bool _init;

    protected virtual void Awake()
    {
        _anim = GetComponent<Animator>();
        Init();
    }

    protected virtual void Update()
    {
        UpdateMove();
        UpdateAttack();
    }

    protected virtual bool Init()
    {
        if (_init)
            return false;

        _init = true;
        return true;
    }

    protected abstract void UpdateMove();
    protected abstract void UpdateAttack();
    protected abstract void OnAttack();
}
