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

    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        WorldObjectType = WorldObject.Enemy;
        _lockTarget = Managers.Game.GetPlayer.transform;
        _agent = gameObject.GetOrAddComponent<NavMeshAgent>();
        _agent.angularSpeed = 0f;
        _agent.height = 0.5f;
        tag = "Enemy";
        
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
        switch(State)
        {
            case MonsterState.Move:
                UpdateMove();
                break;
            case MonsterState.Attack:
                UpdateAttack();
                break;
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

        if (dir.magnitude > AttackDistance)
        {
            State = MonsterState.Move;
            return;
        }

        Quaternion qua = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, qua, 20 * Time.deltaTime);
    }

    protected abstract void OnAttack();

    protected virtual void OnHitEvent(int idx)
    {
        switch(idx)
        {
            case 0:
                Vector3 dir = transform.position - _lockTarget.position;
                float knockback = 4f;
                _rb.AddForce(dir.normalized * knockback, ForceMode.Impulse);
                break;
            case 1:
                _rb.velocity = Vector3.zero;
                State = MonsterState.Move;
                break;
        }
    }

    public override void OnDamaged(float damage)
    {
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
        _agent.SetDestination(transform.position);
        GetComponent<Collider>().enabled = false;
        _rb.isKinematic = true;
        State = MonsterState.Die;
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