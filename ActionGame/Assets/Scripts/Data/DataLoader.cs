using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public class PlayerSkillData
    {
        public Define.SkillType type;
        public float usedMp;
        public float attack;
        public float cooltime;
    }

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

    public class MonsterStatData : StatData
    {
        public float attackDistance;

        public MonsterStatData()
        {

        }

        public MonsterStatData(float maxHp, float attack, float speed, float attackDistance)
        {
            this.maxHp = maxHp;
            this.hp = maxHp;
            this.attack = attack;
            this.speed = speed;
            this.attackDistance = attackDistance;
        }
    }

    public struct BuyableItemData
    {
        public int cost;
        public int maxCount;

        public BuyableItemData(int cost, int maxCount)
        {
            this.cost = cost;
            this.maxCount = maxCount;
        }
    }

    public struct ShopItemData
    {
        public Define.BuyableItem type;
        public string name;
        public string description;
        public int cost;

        public ShopItemData(Define.BuyableItem type, string name, string description, int cost)
        {
            this.type = type;
            this.name = name;
            this.description = description;
            this.cost = cost;
        }
    }
}
