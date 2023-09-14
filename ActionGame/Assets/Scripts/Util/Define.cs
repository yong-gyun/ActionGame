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
        Hp_Upgrade,
        Mp_Upgrade,
        Attack_Upgrade,
        Speed_Upgrade
    }

    public enum Potion
    {
        Hp_Potion,
        Mp_Potion
    }

    public enum BuyableItem
    {
       Hp_Potion,
       Mp_Potion,
       Hp_Upgrade,
       Mp_Upgrade,
       Attack_Upgrade,
       Speed_Upgrade
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
        MeleeSkeletone
    }

    public enum PlayerState
    {
        Idle,
        Move,
        Attack,
        Defense,
        Dodge,
        Skill_1,
        Skill_2,
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
        Menu,
        Loading,
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