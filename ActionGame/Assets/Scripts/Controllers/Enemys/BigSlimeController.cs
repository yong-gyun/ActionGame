using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BigSlimeController : MonsterController
{
    protected override void OnAttack()
    {
        PlayerController pc = _lockTarget.GetComponent<PlayerController>();

        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.forward, out hit, 2.5f))
        {
            if(hit.collider.CompareTag("Shield") && pc.State == Define.PlayerState.Defense)
                return;
        }

        pc.OnDamaged(Attack);
        Debug.Log("Attack");
    }

    protected override void OnDie()
    {
        
        for (int i = 0; i < 2; i++)
        {
            Vector3 randPos = transform.position + (Random.insideUnitSphere * 2f);
            NavMeshPath path = new NavMeshPath();
            MonsterController mc = Managers.Object.SpawnMonster(Define.MonsterId.Slime);
            NavMeshAgent nma = mc.GetComponent<NavMeshAgent>();

            while(true)
            {
                if (nma.CalculatePath(randPos, path))
                    break;
            }

            mc.transform.position = randPos;
        }

        base.OnDie();
    }
}