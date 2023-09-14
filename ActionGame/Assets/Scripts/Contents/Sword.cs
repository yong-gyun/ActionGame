using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
            _trail.enabled = _enabled;
            _mcs.Clear();
        }
    }

    bool _enabled;
    bool _init;
    [SerializeField] float _damage;
    Collider _swordCollider;
    TrailRenderer _trail;
    List<GameObject> _mcs = new List<GameObject>();

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
        if (other.CompareTag("Enemy") && !_mcs.Contains(other.gameObject))
        {
            other.GetComponent<MonsterController>().OnDamaged(_damage);
            GameObject go = Managers.Resource.Instantiate("Effect/SwordHitEffect");
            go.transform.position = other.transform.position;
            Managers.Resource.Destory(go, 1f);
            _mcs.Add(other.gameObject);
        }    
    }
}
