using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
    [SerializeField] float _maxMp;
    [SerializeField] float _mp;
    [SerializeField] float _moveSpeedToWalk;
    [SerializeField] float _moveSpeedToRun;

    public float MaxMp { get { return _maxMp; } set { _maxMp = value; } }
    public float Mp { get { return _mp; } set { _mp = Mathf.Clamp(value, 0, _maxMp); } }    
    public float MoveSpeedToWalk { get { return _moveSpeedToWalk; } set { _moveSpeedToWalk = value; } }
    public float MoveSpeedToRun { get { return _moveSpeedToRun; } set { _moveSpeedToRun = value; } }
    PlayerController _player;

    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        SetStat();
        _player = GetComponent<PlayerController>();
        return true;
    }

    public void SetStat()
    {
        _maxHp = 100f;
        _hp = _maxHp;
        _attack = 10f;
        _moveSpeedToWalk = 5f;
        _moveSpeedToRun = 7.5f;
        _mp = 0f;
    }

    public override void OnDamaged(float damage)
    {
        if (_player.State == Define.PlayerState.Defense || _player.State == Define.PlayerState.AttackAvoidance)
            return;

        base.OnDamaged(damage);
    }
}
