using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Stat : MonoBehaviour
{
    [SerializeField] protected float _maxHp;
    [SerializeField] protected float _hp;
    [SerializeField] protected float _damage;
    [SerializeField] protected float _moveSpeed;
    
    public float MaxHp { get { return _maxHp; } set { _maxHp = value; } }
    public float Hp { get { return Hp; } set { _hp = Mathf.Clamp(value, 0, _maxHp); } }
    public float Damage { get { return _damage; } set { _damage = value; } }
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

        Refresh();
        _init = true;
        return true;
    }

    public abstract void Refresh();

    public virtual void OnDamaged(float damage)
    {
        _hp -= damage;

        if(_hp <= 0)
        {
            OnDieEvent.Invoke();
        }
    }
}
