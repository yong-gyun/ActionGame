using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static Define;

public class EnemyController : BaseController
{
    public virtual EnemyState State
    {
        get { return _state; }
        set
        {
            _state = value;

            switch(_state)
            {
                case EnemyState.Move:
                    _anim.CrossFade("Move", 0.1f);
                    break;
                case EnemyState.Attack:
                    _anim.CrossFade("Attack", 0.1f);
                    break;
                case EnemyState.Hit:
                    _anim.CrossFade("Hit", 0.1f);
                    break;
                case EnemyState.Die:
                    _anim.CrossFade("Die", 0.1f);
                    break;
            }
        }
    }

    protected EnemyStat _stat;
    protected Transform _lockTarget;
    protected NavMeshAgent _agent;
    protected EnemyState _state;

    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        _lockTarget = Managers.Game.GetPlayer.transform;
        _agent = gameObject.GetOrAddComponent<NavMeshAgent>();
        _stat = gameObject.GetOrAddComponent<EnemyStat>();
        _agent.speed = _stat.Speed;
        _agent.angularSpeed = 0f;
        _agent.height = 0.5f;
        return true;
    }
    
    protected override void UpdateMove()
    {
        float distance = (_lockTarget.position - transform.position).magnitude;

        if(distance <= _stat.AttackDistance)
        {
            _agent.SetDestination(transform.position);
            State = EnemyState.Attack;
            return;
        }

        _agent.SetDestination(_lockTarget.position);
        Vector3 dir = (_lockTarget.position - transform.position);
        Quaternion qua = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, qua, 20 * Time.deltaTime);
    }

    protected override void UpdateAttack()
    {
        if(_lockTarget == null)
        {
            State = EnemyState.Idle;
            return;
        }

        Vector3 dir = (_lockTarget.position - transform.position);

        if (dir.magnitude > _stat.AttackDistance)
        {
            State = EnemyState.Move;
            return;
        }

        Quaternion qua = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, qua, 20 * Time.deltaTime);
    }
}