using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public bool Enabled
    {
        get { return _enabled; }
        set
        {
            _enabled = value;
            
            Init();
            _swordCollider.enabled = _enabled;
            _trail.emitting = _enabled;
        }
    }

    bool _enabled;
    bool _init;
    [SerializeField] float _damage;
    Collider _swordCollider;
    TrailRenderer _trail;

    bool Init()
    {
        if (_init)
            return false;

        _swordCollider = GetComponent<Collider>();
        _trail = gameObject.FindChild<TrailRenderer>();
        _init = true;
        return true;
    }

    public void SetDamage(float damage) { _damage = damage; }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<MonsterController>().OnDamaged(_damage);
        }    
    }
}
