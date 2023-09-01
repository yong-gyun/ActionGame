using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : BaseController
{
    public virtual Define.EnemyState State
    {
        get { return _state; }
        set
        {
            _state = value;
        
            switch(_state)
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
                case Define.EnemyState.Die:
                    _anim.CrossFade("Die", 0.1f);
                    break;
            }
        }
    }

    protected Define.EnemyState _state;
    protected Transform _lockTarget;
    protected EnemyStat _stat;
    protected NavMeshAgent _nma;

    protected override bool Init()
    {
        if (base.Init() == false)

            return false;

        _lockTarget = Managers.Game.Player.transform;
        _stat = gameObject.GetOrAddComponent<EnemyStat>();
        _nma = gameObject.GetOrAddComponent<NavMeshAgent>();

        _nma.angularSpeed = 0f;
        return true;
    }

    protected virtual void Update()
    {
        switch(State)
        {
            case Define.EnemyState.Die:
                UpdateDie();
                break;
            case Define.EnemyState.Move:
                UpdateMove();
                break;
            case Define.EnemyState.Attack:
                UpdateAttack();
                break;
            case Define.EnemyState.RangeAttack:
                UpdateRangeAttack();
                break;
        }
    }

    protected virtual void UpdateDie()
    {

    }

    protected override void UpdateMove()
    {
        Vector3 dir = (_lockTarget.position - transform.position);
        
        if(dir.magnitude < _stat.AttackDistance)
        {
            _nma.SetDestination(transform.position);
            State = Define.EnemyState.Attack;
            return;
        }

        _nma.SetDestination(_lockTarget.position);
        _nma.speed = _stat.MoveSpeed;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
    }

    protected override void UpdateAttack()
    {
        Vector3 dir = (_lockTarget.position - transform.position);

        if (dir.magnitude > _stat.AttackDistance)
        {
            State = Define.EnemyState.Move;
            return;
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
    }

    protected virtual void UpdateRangeAttack() { }

    protected virtual void OnAttacked()
    {
        RaycastHit hit;
        Vector3 dir = _lockTarget.position - transform.position;

        if(Physics.Raycast(transform.position, dir.normalized, out hit, _stat.AttackDistance))
        {
            if (hit.collider.CompareTag("Shield"))
                return;
        }


    }


    public virtual void OnHit()
    {

    }
}