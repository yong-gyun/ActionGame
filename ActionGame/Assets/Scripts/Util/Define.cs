using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public const float HP_HEAL = 5F;
    public const float MP_HEAL = 4F;
    public const float UPGRADE_HP = 10F;
    public const float UPGRADE_MP = 5F;
    public const float UPGRADE_SPEED = 0.1F;
    public const float UPGRADE_ATTACK = 4F;

    public enum StatUpgrade
    {
        Hp,
        Mp,
        Attack,
        Speed
    }

    public enum Potion
    {
        Hp_Heal,
        Mp_Heal
    }

    public enum BuyableItem
    {
       Hp_Potion,
       Mp_Potion,
       HpUpgrade,
       MpUpgrade,
       AttackUpgrade,
       SpeedUpgrade
    }

    public enum DropItem
    {
        Shield,
        Hp_Heal,
        Mp_Heal,
        Small_Coin,
        Big_Coin
    }

    public enum MonsterId
    {
        TurtleSlime,
        BigSlime,
        Slime,
    }

    public enum PlayerState
    {
        Idle,
        Move,
        Attack,
        Defense,
        Dodge,
        Skill,
        Hit,
        Die
    }

    public enum MonsterState
    {
        Idle,
        Move,
        Attack,
        Defense,
        Hit,
        Die
    }

    public enum SceneType
    {
        Title,
        Game,
        Test
    }

    public enum SkillType
    {
        Skill_1,
        Skill_2,
    }

    public enum WorldObject
    {
        Unknow,
        Player,
        Enemy,
        Boss
    }

    public enum AnimEvent
    {
        Enter,
        Exit
    }
}