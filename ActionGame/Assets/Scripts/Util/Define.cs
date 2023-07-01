using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum PlayerState
    {
        Idle,
        Walk,
        Run,
        Attack,
        Defence,
        SpecalAttack_One,
        SpecalAttack_Two,
        AttackAvoidance,
    }

    public enum Scene
    {
        Title,
        Game
    }

    public enum Sound
    {
        Bgm,
        Effect
    }
}
