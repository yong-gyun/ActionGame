using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    protected Animator _anim;

    protected virtual void Awake()
    {
        _anim = GetComponent<Animator>();
        Init();
    }

    protected virtual void Update()
    {
        UpdateMove();
    }

    protected virtual void Init()
    {

    }

    protected abstract void UpdateMove();
    protected abstract void UpdateAttack();
    protected abstract void OnAttack();
}
