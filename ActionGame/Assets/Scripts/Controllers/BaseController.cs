using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    public float MaxHp { get { return _maxHp; } set { _maxHp = value; } }
    public float Hp { get { return _hp; } set { _hp = value; } }
    public float Attack { get { return _attack; } set { _attack = value; } }
    public float Speed { get { return _speed; } set { _speed = value; } }

    [SerializeField] protected float _maxHp;
    [SerializeField] protected float _hp;
    [SerializeField] protected float _attack;
    [SerializeField] protected float _speed;

    public Define.WorldObject WorldObjectType { get; protected set; }

    protected Animator _anim;
    protected Rigidbody _rb;
    protected bool _init;

    void Start()
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

    public abstract void OnDamaged(float damage);

    protected virtual void OnDie()
    {
        Managers.Object.Despawn(gameObject);
    }
}
