using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public class StatData
    {
        public float maxHp;
        public float hp;
        public float attack;
        public float speed;
    }

    public class PlayerStatData : StatData
    {
        public float maxMp;
        public float mp;
        public float dodgePower;
        public float walkSpeed;
        public float runSpeed;

        public PlayerStatData(float maxHp, float maxMp, float attack, float walkSpeed, float runSpeed, float dodgePower)
        {
            this.maxHp = maxHp;
            this.hp = maxHp;
            this.maxMp = maxMp;
            this.mp = maxHp;
            this.attack = attack;
            this.walkSpeed = walkSpeed;
            this.runSpeed = runSpeed;
            this.dodgePower = dodgePower;
        }
    }

    public class EnemyStatData : StatData
    {
        public float attackDistance;

        public EnemyStatData(float maxHp, float attack, float speed, float attackDistance)
        {
            this.maxHp = maxHp;
            this.hp = maxHp;
            this.attack = attack;
            this.speed = speed;
            this.attackDistance = attackDistance;
        }
    }
}
