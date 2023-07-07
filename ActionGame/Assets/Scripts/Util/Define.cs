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
        Defence,
        SpecalAttack_One,
        SpecalAttack_Two,
        AttackAvoidance,
    }

    public enum Scene
    {
        Menu,
        Game,
    }

    public enum Sound
    {
        Bgm,
        Effect
    }

    public enum Weapone
    {
        Sword,
        Shield
    }
}
