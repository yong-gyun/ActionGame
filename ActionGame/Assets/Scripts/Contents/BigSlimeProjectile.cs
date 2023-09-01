using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSlimeProjectile : MonoBehaviour
{
    float _damage;
    float _arrivalTime;
    Vector3 _originPos;
    Vector3 _destPos;

    public void Init(float damage)
    {
        _damage = damage;
        Managers.Resource.Destory(gameObject, 3f);
        _destPos = Managers.Game.Player.transform.position;
        _originPos = transform.position;
        _arrivalTime = (transform.position - Managers.Game.Player.transform.position).magnitude * 0.5f; 
    }

    float _t = 0;

    private void Update()
    {
        _t += Time.deltaTime / _arrivalTime;

        Vector3 center = (_destPos - _originPos) + Vector3.up * 5f;
        Vector3 pointA = Vector3.Lerp(_originPos, center, _t);
        Vector3 pointB = Vector3.Lerp(center, _destPos, _t);

        transform.position = Vector3.Lerp(pointA, pointB, _t);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStat>().OnDamaged(_damage);
            Managers.Resource.Destory(gameObject);
        }

        if(other.CompareTag("Shield"))
            Managers.Resource.Destory(gameObject);
    }
}
