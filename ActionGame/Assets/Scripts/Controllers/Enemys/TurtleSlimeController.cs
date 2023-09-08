using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleSlimeController : MonsterController
{
    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        return true;
    }

    protected override void OnAttack()
    {
        
    }
}
