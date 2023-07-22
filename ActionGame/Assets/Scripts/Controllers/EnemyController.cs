using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseController
{
    protected Transform _lockTarget;


    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        return true;
    }
}
