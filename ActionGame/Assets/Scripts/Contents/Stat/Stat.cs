using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Stat : MonoBehaviour
{
    public float MaxHp { get { return _maxHp; } set { _maxHp = value; } }
    public float Hp { get { return _hp; } set { _hp = value; } }
    public float Attack { get { return _attack; } set { _attack = value; } }
    public float Speed { get { return _speed; } set { _speed = value; } }
    //public Action OnDeadEventHandler = null;

    [SerializeField] protected float _maxHp;
    [SerializeField] protected float _hp;
    [SerializeField] protected float _attack;
    [SerializeField] protected float _speed;

    protected Define.WorldObject _type;
    protected bool _init;

    void Start()
    {
        Init();
    }

    protected virtual bool Init()
    {
        if(_init)
            return false;

        _type = Managers.Object.GetWorldObjectType(gameObject);
        SetStat();
        _init = true;
        return true;
    }

    public abstract void SetStat();
    public abstract void OnDamaged(float damage);
    protected abstract void OnDie();
}