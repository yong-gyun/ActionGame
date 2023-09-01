using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Stat : MonoBehaviour
{
    [SerializeField] protected float _maxHp;
    [SerializeField] protected float _hp;
    [SerializeField] protected float _attack;
    [SerializeField] protected float _moveSpeed;
    
    public float MaxHp { get { return _maxHp; } set { _maxHp = value; } }
    public float Hp { get { return Hp; } set { _hp = Mathf.Clamp(value, 0, _maxHp); } }
    public float Attack { get { return _attack; } set { _attack = value; } }
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }

    protected bool _init;
    public Action OnDieEvent = null;

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

    public virtual void OnDamaged(float damage)
    {
        _hp -= damage;

        if(_hp <= 0)
        {
            _hp = 0;
            OnDieEvent.Invoke();
        }
    }
}