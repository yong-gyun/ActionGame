using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSkill : MonoBehaviour
{
    public float UsedMp { get { return _usedMp; } }
    public float Cooltime { get { return _cooltime; } }
    public float CurrentCooltime { get { return _currentCoolTime; } }
    public float Attack { get { return _attack; } }
    public bool Useable { get { return _useable; } }
    public Define.SkillType Type { get { return _type; } }
    
    protected Func<GameObject> _skillFunc = null;
    protected PlayerController _player;

    [SerializeField] protected float _usedMp;
    [SerializeField] protected float _cooltime;
    [SerializeField] protected float _currentCoolTime;
    [SerializeField] protected float _attack;
    [SerializeField] protected bool _useable;
    [SerializeField] protected Define.SkillType _type;
    bool _init = false;

    private void Start()
    {
        Init();
    }

    protected virtual bool Init()
    {
        if (_init)
            return false;
        _player = GetComponent<PlayerController>();
        return true;
    }

    protected void SetStat()
    {
        Data.PlayerSkillData data = Managers.Data.PlayerSkill[_type];
        _usedMp = data.usedMp;
        _cooltime = data.cooltime;
        _attack = data.attack;
    }

    public bool SkillUsedable()
    {
        if (_player.Mp < UsedMp || CurrentCooltime > 0)
            return false;
        return true;
    }

    public virtual void UseSkill()
    {
        _player.Mp -= UsedMp;
        _currentCoolTime = _cooltime;
        StartCoroutine(CoCountCooltime());
        GameObject go = _skillFunc.Invoke();
        go.GetOrAddComponent<PlayerSkillDamaged>().Init(Type, Attack);
        Managers.Resource.Destory(go, 5f);
        _useable = false;
    }

    protected void ExitSkillState() { _player.State = Define.PlayerState.Idle; }

    protected IEnumerator CoCountCooltime()
    {
        while (_currentCoolTime > 0)
        {
            _currentCoolTime -= Time.deltaTime;
            yield return null;
        }

        _currentCoolTime = 0f;
        _useable = true;
    }
}