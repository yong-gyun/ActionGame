using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
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

    public enum EnemyState
    {

    }

    public enum SkillType
    {
        Skill_1,
        Skill_2,
    }
}
