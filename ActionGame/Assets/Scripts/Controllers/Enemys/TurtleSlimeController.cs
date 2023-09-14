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

    protected override void OnAttack(int idx)
    {
        switch(idx)
        {
            case 0:
                PlayerController pc = _lockTarget.GetComponent<PlayerController>();

                RaycastHit hit;

                Debug.DrawRay(transform.position, Vector3.up + transform.forward * 2.5f, Color.red, 1f);

                if (Physics.Raycast(transform.position, transform.forward + Vector3.up, out hit, 2.5f))
                {
                    if (hit.collider.CompareTag("Shield") && pc.State == Define.PlayerState.Defense || (hit.transform.position - transform.position).magnitude > AttackDistance)
                        return;
                }

                pc.OnDamaged(Attack);
                break;
            case 1:
                State = Define.MonsterState.Idle;
                break;
        }
    }
}
