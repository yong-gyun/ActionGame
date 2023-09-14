using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static Define;

public abstract class MonsterController : BaseController
{
    public virtual MonsterState State
    {
        get { return _state; }
        set
        {
            _state = value;

            switch(_state)
            {
                case MonsterState.Idle:
                    _anim.CrossFade("Idle", 0.1f);
                    break;
                case MonsterState.Move:
                    _anim.CrossFade("Move", 0.1f);
                    break;
                case MonsterState.Attack:
                    _anim.CrossFade("Attack", 0.1f);
                    break;
                case MonsterState.Hit:
                    _anim.CrossFade("Hit", 0.1f);
                    break;
                case MonsterState.Die:
                    _anim.CrossFade("Die", 0.1f);
                    break;
            }
        }
    }

    public float AttackDistance { get { return _attackDistance; } set { _attackDistance = value; } }
    public MonsterId MonsterId { get { return _monsterId; } set { _monsterId = value; } }
    
    [SerializeField] float _attackDistance;

    protected Transform _lockTarget;
    protected NavMeshAgent _agent;
    [SerializeField] protected MonsterState _state;
    [SerializeField] protected MonsterId _monsterId;
    protected bool _useSkill;

    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        WorldObjectType = WorldObject.Enemy;
        _lockTarget = Managers.Object.Player.transform;
        _agent = gameObject.GetOrAddComponent<NavMeshAgent>();
        _agent.angularSpeed = 0f;
        _agent.height = 0.5f;
        tag = "Enemy";
        SetStat();

        if (_lockTarget != null)
            State = MonsterState.Move;

        return true;
    }

    public virtual void SetStat()
    {
        MonsterStatData statData = new MonsterStatData();

        if (Managers.Data.MonsterStat.TryGetValue(_monsterId, out statData) == false)
        {
            Debug.Log($"Null Reference this data {_monsterId}");
            return;
        }

        MaxHp = statData.maxHp;
        Hp = statData.hp;
        Attack = statData.attack;
        Speed = statData.speed;
        AttackDistance = statData.attackDistance;
    }

    protected virtual void Update()
    {
        if (State == MonsterState.Die)
            return;

        switch(State)
        {
            case MonsterState.Die:
                UpdateDie();
                break;
            case MonsterState.Idle:
                UpdateIdle();
                break;
            case MonsterState.Move:
                UpdateMove();
                break;
            case MonsterState.Attack:
                UpdateAttack();
                break;
        }
    }

    protected override void UpdateIdle()
    {
        if(_lockTarget != null)
        {
            State = MonsterState.Move;
            return;
        }
    }

    protected override void UpdateMove()
    {
        float distance = (_lockTarget.position - transform.position).magnitude;

        if(distance <= AttackDistance)
        {
            _agent.SetDestination(transform.position);
            State = MonsterState.Attack;
            return;
        }

        _agent.SetDestination(_lockTarget.position);
        Vector3 dir = (_lockTarget.position - transform.position);
        Quaternion qua = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, qua, 20 * Time.deltaTime);
        _agent.speed = Speed;
    }

    protected override void UpdateAttack()
    {
        if(_lockTarget == null)
        {
            State = MonsterState.Idle;
            return;
        }

        Vector3 dir = (_lockTarget.position - transform.position);
        Quaternion qua = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, qua, 20 * Time.deltaTime);
    }

    protected abstract void OnAttack(int idx);

    protected virtual void OnHitEvent()
    {
        Vector3 dir = transform.forward * -1f;
        _agent.SetDestination(transform.position);

        if (_hitCoroutine != null)
            StopCoroutine(_hitCoroutine);

        _hitCoroutine = StartCoroutine(CoWaitForHitEvent());
    }
    Coroutine _hitCoroutine;

    IEnumerator CoWaitForHitEvent()
    {
        yield return new WaitForSeconds(2f);

        if (State == MonsterState.Die)
            yield break;
        _rb.velocity = Vector3.zero;
        State = MonsterState.Idle;
    }

    public override void OnDamaged(float damage)
    {
        if (State == MonsterState.Die)
            return;

        Hp -= damage;
        State = MonsterState.Hit;

        if(Hp <= 0)
        {
            Hp = 0f;
            OnDeadEvent();
        }
    }

    public virtual void OnDeadEvent()
    {
        if (State == MonsterState.Die)
            return;

        State = MonsterState.Die;
        _agent.SetDestination(transform.position);
        GetComponent<Collider>().enabled = false;
        _rb.isKinematic = true;
    }

    protected void OnDropItem()
    {
        int percent = Random.Range(1, 100);

        if (percent <= 40)
        {
            //Gold
        }
        else if (percent <= 60)
        { 
            //Shield
        }
        else if (percent <= 80)
        {
            //Hp Heal
        }
        else
        {
            //Mp Heal
        }
    }
}