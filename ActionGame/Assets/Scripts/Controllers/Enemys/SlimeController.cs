using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : BigSlimeController
{
    protected override void OnDie()
    {
        //@TODO ������ ���

        Managers.Object.Despawn(gameObject);
    }
}
