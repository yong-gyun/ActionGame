using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerStat : Stat
{
    public float MaxMp { get { return _maxMp; } set { _maxMp = value; } }
    public float Mp { get { return _mp; } set { _mp = value; } }
    public float DodgePower { get { return _dodgePower; } set { _dodgePower = value; } }
    public float RunSpeed { get { return _runSpeed; } set { _runSpeed = value; } }
    public float WalkSpeed { get { return _walkSpeed; } set { _walkSpeed = value; } }

    [SerializeField] float _maxMp;
    [SerializeField] float _mp;
    [SerializeField] float _dodgePower;
    [SerializeField] float _runSpeed;
    [SerializeField] float _walkSpeed;
    bool _isDamaged;

    PlayerController _player;

    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        _player = GetComponent<PlayerController>();
        return true;
    }

    public override void SetStat()
    {
        MaxHp = Managers.Data.PlayerStatData.maxHp;
        Hp = Managers.Data.PlayerStatData.hp;
        MaxMp = Managers.Data.PlayerStatData.maxMp;
        Mp = Managers.Data.PlayerStatData.mp;
        Attack = Managers.Data.PlayerStatData.attack;
        WalkSpeed = Managers.Data.PlayerStatData.walkSpeed;
        RunSpeed = Managers.Data.PlayerStatData.runSpeed;
        DodgePower = Managers.Data.PlayerStatData.dodgePower;
    }

    public override void OnDamaged(float damage)
    {
        _isDamaged |= _player.State == Define.PlayerState.Dodge;
        
        if (_isDamaged)
            return;

        _isDamaged = true;
        Hp -= damage;
        Managers.Game.GetPlayer.State = Define.PlayerState.Hit;

        if(Hp <= 0)
        {
            OnDie();
        }
    }

    IEnumerator CoFlash()
    {
        float time = 0f;
        float destTime = 0.75f;

        Material[] materials = gameObject.FindChild("Model").GetComponentsInChildren<Material>();
        float alpha = 1f;
        
        while(alpha >= 0.5f)
        {
            time += Time.deltaTime / destTime;
            alpha = Mathf.Lerp(1f, 0.5f, time);
            yield return null;
        }

        time = 0f;

        while(alpha <= 1f)
        {
            time += Time.deltaTime / destTime;
            alpha = Mathf.Lerp(0.5f, 1f, time);
            yield return null;
        }

        _isDamaged = false;
    }

    protected override void OnDie()
    {

    }
}