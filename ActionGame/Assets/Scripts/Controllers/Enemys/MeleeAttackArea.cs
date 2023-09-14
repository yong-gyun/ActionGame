using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackArea : MonoBehaviour
{
    Collider _area;
    float _attack;

    public void SetDamage(float attack)
    {
        _attack = attack;
    }

    public bool IsAttacked 
    { 
        get 
        { return _isAttacked; }
        set
        {
            Init();

            _isAttacked = value;
            _area.enabled = value;
            _onCollisionShield = false;
        }
    }

    [SerializeField] bool _isAttacked;
    [SerializeField] bool _onCollisionShield;
    bool _init;

    bool Init()
    {
        if (_init)
            return false;

        _area = GetComponent<Collider>();
        _area.enabled = false;
        return true;
    }

    private void Start()
    {
        Init();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shield"))
        {
            _onCollisionShield = true;
            return;
        }

        if (other.CompareTag("Player") && !_onCollisionShield)
        {
            other.GetComponent<PlayerController>().OnDamaged(_attack);
        }
    }
}
