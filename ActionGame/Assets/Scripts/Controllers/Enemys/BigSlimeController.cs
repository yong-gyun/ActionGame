using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSlimeController : SlimeController
{
    Transform _firePos;

    public override Define.EnemyState State
    {
        get { return _state; }
        set
        {
            _state = value;

            switch (_state)
            {
                case Define.EnemyState.Move:
                    _anim.CrossFade("Move", 0.1f);
                    break;
                case Define.EnemyState.Attack:
                    _anim.CrossFade("Attack", 0.1f);
                    break;
                case Define.EnemyState.Hit:
                    _anim.CrossFade("Hit", 0.1f);
                    break;
                case Define.EnemyState.RangeAttack:
                    _anim.CrossFade("RangeAttack", 0.1f);
                    break;
                case Define.EnemyState.Die:
                    _anim.CrossFade("Die", 0.1f);
                    break;
            }
        }
    }

    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        WorldObjectType = Define.WorldObject.BigSlime;
        return true;
    }

    protected override void UpdateAttack()
    {
        Vector3 dir = (_lockTarget.position - transform.position);

        if (dir.magnitude > _stat.AttackDistance)
        {
            State = Define.EnemyState.Move;
            return;
        }

        if(dir.magnitude < _stat.RangedAttackDistance)
        {
            if (dir.magnitude > _stat.AttackDistance)
            {
                State = Define.EnemyState.RangeAttack;
                return;
            }
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
    }

    protected override void UpdateRangeAttack()
    {
        Vector3 dir = (_lockTarget.position - transform.position);

        if (dir.magnitude > _stat.AttackDistance)
        {
            State = Define.EnemyState.Move;
            return;
        }

        if (dir.magnitude < _stat.RangedAttackDistance)
        {
            if (dir.magnitude < _stat.AttackDistance)
            {
                State = Define.EnemyState.Attack;
                return;
            }
        }
    }

    void OnRangeAttack()
    {
        Vector3 dir = _lockTarget.position - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);

        GameObject go = Managers.Resource.Instantiate("BigSlimeProjectile");
        BigSlimeProjectile projectile = go.GetOrAddComponent<BigSlimeProjectile>();
        projectile.Init(_stat.Attack);
    }
}