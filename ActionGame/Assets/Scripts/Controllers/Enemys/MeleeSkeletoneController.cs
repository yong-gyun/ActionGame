using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class MeleeSkeletoneController : MonsterController
{
    public override MonsterState State
    {
        get { return _state; }
        set
        {
            _state = value;

            switch (_state)
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
                case MonsterState.Defense:
                    _anim.CrossFade("Defense", 0.1f);
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

    float _defenseTime = 3f;
    float _currentDefenseTime = 0f;
    float _shieldDistance = 5f;
    float _cooltime = 5f;
    int _damagedCnt = 0;
    bool _hitable = true;

    MeleeAttackArea _attackArea;

    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        GameObject go = gameObject.FindChild("Sword", true);
        _attackArea = go.GetOrAddComponent<MeleeAttackArea>();
        _attackArea.SetDamage(Attack);
        return true;
    }

    protected override void Update()
    {
        if (State == MonsterState.Die)
            return;

        switch (State)
        {
            case MonsterState.Idle:
                UpdateIdle();
                break;
            case MonsterState.Move:
                UpdateMove();
                break;
            case MonsterState.Attack:
                UpdateAttack();
                break;
            case MonsterState.Defense:
                UpdateDefense();
                break;
        }
    }

    protected override void OnAttack(int idx)
    {
        switch(idx)
        {
            case 0:
                _attackArea.IsAttacked = true;
                break;
            case 1:
                _attackArea.IsAttacked = false;
                State = MonsterState.Idle;
                break;
        }
    }

    protected override void UpdateMove()
    {
        float distance = (_lockTarget.position - transform.position).magnitude;

        if(distance <= _shieldDistance)
        {
            if(Input.GetMouseButtonDown(0) && _currentDefenseTime <= 0f)
            {
                _agent.SetDestination(transform.position);
                State = MonsterState.Defense;
                _damagedCnt = 3;
                return;
            }
        }
        
        if (distance <= AttackDistance)
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

    void UpdateDefense()
    {
        _hitable = false;
        _currentDefenseTime += Time.deltaTime;

        if(_currentDefenseTime >= _defenseTime || _damagedCnt == 0)
        {
            StartCoroutine(CoDefenseCooltime());
            State = MonsterState.Move;
            _hitable = true;
            return;
        }

        Vector3 dir = (_lockTarget.position - transform.position);
        Quaternion qua = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, qua, 20 * Time.deltaTime);
    }

    IEnumerator CoDefenseCooltime()
    {
        yield return new WaitForSeconds(_cooltime);
        _currentDefenseTime = 0f;
    }

    public override void OnDamaged(float damage)
    {
        if (State == MonsterState.Die || _hitable == false)
            return;

        Hp -= damage;
        State = MonsterState.Hit;

        if (Hp <= 0)
        {
            Hp = 0f;
            OnDeadEvent();
        }
    }
}
