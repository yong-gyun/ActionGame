using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : EnemyController
{
    [SerializeField] Vector3[] _partrolPoint = new Vector3[3];
    float _searchDistance = 5f;
    int _patrolIndex = 0;

    protected override bool Init()
    {
        if(base.Init() == false)
            return false;

        WorldObjectType = Define.WorldObject.Slime;
        Transform parent = GameObject.Find("@PatrolPoints").transform;

        for (int i = 1; i < parent.childCount; i++)
        {
            _partrolPoint[i] = parent.GetChild(i - 1).position;
        }

        _partrolPoint[0] = transform.position;
        return true;
    }

    protected override void UpdateMove()
    {
        Vector3 dir = _lockTarget.position - transform.position;
        
        if(dir.magnitude < _searchDistance)
        {
            base.UpdateMove();
            return;
        }

        UpdatePatrol();
    }

    void UpdatePatrol()
    {
        Vector3 destPos = _partrolPoint[_patrolIndex % _partrolPoint.Length];
        _nma.SetDestination(destPos);

        Vector3 dir = destPos - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);

        if(dir.magnitude < 0.1f)
            _patrolIndex++;
    }
}