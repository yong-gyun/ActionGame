using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : BigSlimeController
{
    protected override void OnDie()
    {
        //@TODO 아이템 드랍

        Managers.Object.Despawn(gameObject);
    }
}
