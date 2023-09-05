using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : Stat
{
    public float AttackDistance { get { return _attackDistance; } set { _attackDistance = value; } }
    [SerializeField] float _attackDistance;

    public override void OnDamaged(float damage)
    {
        Hp -= damage;

        if(Hp <= 0)
        {
            Hp = 0;
            OnDie();
        }
    }

    public override void SetStat()
    {
        if (_type == Define.WorldObject.Unknow)
            return;
        
        EnemyStatData stat = Managers.Data.EnemyStatData[_type];

        MaxHp = stat.maxHp;
        Hp = stat.hp;
        Attack = stat.attack;
        Speed = stat.speed;
        AttackDistance = stat.attackDistance;
    }

    protected override void OnDie()
    {
        Managers.Object.Despawn(gameObject);
    }
}
