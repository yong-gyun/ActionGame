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
        SpecalAttack_One,
        SpecalAttack_Two,
        AttackAvoidance,
        Die
    }

    public enum MouseEvent
    {
        Click,
        PointerDown,
        PointerUp,
        Pressed,
    }

    public enum Scene
    {
        Title,
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
