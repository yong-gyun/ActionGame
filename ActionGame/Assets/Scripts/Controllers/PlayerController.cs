using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class PlayerController : BaseController
{
    public PlayerState State
    {
        get
        {
            return _state;
        }
        set
        {
            _state = value;
            Debug.Log(_state);
            switch(State)
            {
                case PlayerState.Idle:
                    _anim.CrossFade("Idle", 0.1f);
                    break;
                case PlayerState.Move:
                    _anim.CrossFade("Move", 0.1f);
                    break;
                case PlayerState.Attack:
                    _anim.CrossFade("Attack", 0.1f);
                    break;
                case PlayerState.Defense:
                    _anim.CrossFade("Defense", 0.1f);
                    break;
                case PlayerState.Dodge:
                    _anim.CrossFade("Dodge", 0.1f);
                    break;
                case PlayerState.Skill:
                    
                    break;
                case PlayerState.Hit:
                    _anim.CrossFade("Hit", 0.1f);
                    break;
                case PlayerState.Die:
                    _anim.CrossFade("Die", 0.1f);
                    break;
            }
        }
    }

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

    bool _isInvin;
    
    Coroutine _coroutine;
    Collider _shieldCollider;
    Sword _sword;
    public GameObject ShieldItem { get; set; }

    [SerializeField] Vector3 _dir;
    [SerializeField] PlayerState _state;
    
    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        _shieldCollider = gameObject.FindChild<Collider>("Shield", true);
        SetStat();
        GameObject go = gameObject.FindChild("Sword", true);
        _sword = go.GetOrAddComponent<Sword>();
        _sword.SetDamage(Attack);
        _sword.Enabled = false;

        Managers.Input.MouseAction += UpdateMouseEvent;
        Managers.Input.KeyAction += UpdateKeyEvent;
        return true;
    }

    void SetStat()
    {
        MaxHp = Managers.Data.PlayerStat.maxHp;
        Hp = Managers.Data.PlayerStat.hp;
        MaxMp = Managers.Data.PlayerStat.maxMp;
        Mp = Managers.Data.PlayerStat.mp;
        Attack = Managers.Data.PlayerStat.attack;
        WalkSpeed = Managers.Data.PlayerStat.walkSpeed;
        RunSpeed = Managers.Data.PlayerStat.runSpeed;
        DodgePower = Managers.Data.PlayerStat.dodgePower;
    }

    private void Update()
    {
        switch(State)
        {
            case PlayerState.Idle:
                UpdateIdle();
                break;
            case PlayerState.Move:
                UpdateMove();
                break;
            case PlayerState.Defense:
                UpdateDefense();
                break;
        }
    }

    void UpdateMouseEvent()
    {
        if(Input.GetMouseButtonDown(0))
            State = PlayerState.Attack;
        else if(Input.GetMouseButton(1))
            State = PlayerState.Defense;

        if(Input.GetMouseButtonUp(1))
        {
            if (State == PlayerState.Defense)
                State = PlayerState.Idle;
        }   
    }

    void UpdateKeyEvent()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        _dir = (Vector3.right * horizontal) + (Vector3.forward * vertical);

        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            State = PlayerState.Dodge;
            return;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Speed = RunSpeed;
            _anim.SetFloat("Speed", 1f);
        }
        else
        {
            Speed = WalkSpeed;
            _anim.SetFloat("Speed", 0f);
        }
    }

    protected override void UpdateIdle()
    {
        if (_dir != Vector3.zero)
        {
            State = PlayerState.Move;
            return;
        }
    }

    protected override void UpdateMove()
    {
        if(_dir == Vector3.zero)
        {
            State = PlayerState.Idle;
            return;
        }

        if(Physics.Raycast(transform.position, transform.forward, 2.5f, LayerMask.GetMask("Block")))
            return;

        transform.position += _dir.normalized * Speed * Time.deltaTime;
        Quaternion qua = Quaternion.LookRotation(_dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, qua, 20 * Time.deltaTime);
    }

    void UpdateDefense()
    {
        if(_dir != Vector3.zero)
        {
            State = PlayerState.Idle;
            _shieldCollider.enabled = false;
            return;
        }

        _shieldCollider.enabled = true;
    }

    void OnDodge(int idx)
    {
        switch (idx)
        {
            case 0:
                _rb.AddForce(_dir * DodgePower, ForceMode.Impulse);
                break;
            case 1:
                _rb.velocity = Vector3.zero;
                State = PlayerState.Idle;
                break;
        }
    }

    void OnAttack(int idx)
    {
        switch(idx)
        {
            case 0:
                _sword.Enabled = true;
                break;
            case 1:
                _sword.Enabled = false;
                break;
            case 2:
                State = PlayerState.Idle;
                break;
        }
    }

    public override void OnDamaged(float damage)
    {
        _isInvin |= State == PlayerState.Dodge;

        if(ShieldItem != null)
        {
            Managers.Resource.Destory(ShieldItem);
            return;
        }

        if (_isInvin)
            return;

        _isInvin = true;
        Hp -= damage;
        Managers.Game.GetPlayer.State = PlayerState.Hit;

        if (Hp <= 0)
        {
            OnDie();
        }
    }

    void ForWait(float seconds, Action evt)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(CoWait(seconds, evt));
    }

    IEnumerator CoFlash()
    {
        float time = 0f;
        float destTime = 0.75f;

        Material[] materials = gameObject.FindChild("Model").GetComponentsInChildren<Material>();
        float alpha = 1f;

        while (alpha >= 0.5f)
        {
            time += Time.deltaTime / destTime;
            alpha = Mathf.Lerp(1f, 0.5f, time);
            yield return null;
        }

        time = 0f;

        while (alpha <= 1f)
        {
            time += Time.deltaTime / destTime;
            alpha = Mathf.Lerp(0.5f, 1f, time);
            yield return null;
        }

        _isInvin = false;
    }

    IEnumerator CoWait(float seconds, Action evt = null)
    {
        yield return new WaitForSeconds(seconds);

        if (evt != null)
            evt.Invoke();
    }
}