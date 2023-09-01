using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public class StatData
    {
        public float hp;
        public float maxHp;
        public float attack;
        public float moveSpeed;
    }

    public class EnemyStatData : StatData
    {
        public float attackDistance;
        public float rangedAttackDistance;

        /// <summary>
        /// Initialize data
        /// </summary>
        /// <param name="maxHp"></param>
        /// <param name="attack"></param>
        /// <param name="moveSpeed"></param>
        /// <param name="attackDistance"></param>
        public EnemyStatData(float maxHp, float attack, float moveSpeed, float attackDistance)
        {
            this.maxHp = maxHp;
            hp = maxHp;
            this.attack = attack;
            this.moveSpeed = moveSpeed;
            this.attackDistance = attackDistance;
        }

        /// <summary>
        /// Initialize Data
        /// </summary>
        /// <param name="maxHp"></param>
        /// <param name="attack"></param>
        /// <param name="moveSpeed"></param>
        /// <param name="attackDistance"></param>
        /// <param name="rangedAttackDistance"></param>
        public EnemyStatData(float maxHp, float attack, float moveSpeed, float attackDistance, float rangedAttackDistance)
        {
            this.maxHp = maxHp;
            hp = maxHp;
            this.attack = attack;
            this.moveSpeed = moveSpeed;
            this.attackDistance = attackDistance;
            this.rangedAttackDistance = rangedAttackDistance;
        }
    }

    public class PlayerStatData
    {
        public float avoidance;
    }
}
