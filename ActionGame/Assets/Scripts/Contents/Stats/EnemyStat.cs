using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

public class EnemyStat : Stat
{
    public float RangedAttackDistance { get { return _rangedAttackDistance; } set { _rangedAttackDistance = value; } }
    public float AttackDistance { get { return _attackDistance; } set { _attackDistance = value; } }

    [SerializeField] protected float _attackDistance;
    [SerializeField] protected float _rangedAttackDistance;

    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        EnemyController enemy = GetComponent<EnemyController>();
        SetStat(enemy.WorldObjectType);
        return true;
    }

    public void SetStat(Define.WorldObject type)
    {
        EnemyStatData stat = Managers.Data.StatDatas[type];
        MaxHp = stat.maxHp;
        Hp = stat.hp;
        Attack = stat.attack;
        AttackDistance = stat.attackDistance;
        MoveSpeed = stat.moveSpeed;
    }
}