using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Stat : MonoBehaviour
{
    public float MaxHp { get { return _maxHp; } set { _maxHp = value; } }
    public float Hp { get { return _hp; } set { _hp = value; } }
    public float Damage { get { return _damage; } set { _damage = value; } }
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }

    [SerializeField] protected float _maxHp;
    [SerializeField] protected float _hp;
    [SerializeField] protected float _damage;
    [SerializeField] protected float _moveSpeed;

    public abstract void Init();

    public virtual void OnDamaged(Stat attacker)
    {
        _hp -= attacker.Damage;

        if (_hp <= 0)
        {
            _hp = 0;
            OnDead();
        }
    }

    protected virtual void OnDead()
    {

    }
}
