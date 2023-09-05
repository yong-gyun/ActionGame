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

    PlayerStat _stat;
    Collider _shieldCollider;
    Sword _sword;

    [SerializeField] Vector3 _dir;
    PlayerState _state;
    
    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        _shieldCollider = gameObject.FindChild<Collider>("Shield", true);
        _stat = gameObject.GetOrAddComponent<PlayerStat>();
        GameObject go = gameObject.FindChild("Sword", true);
        _sword = go.GetOrAddComponent<Sword>();
        _sword.Enabled = false;
        return true;
    }

    private void Update()
    {
        UpdateMouseEvent();
        UpdateKeyEvent();

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
            _stat.Speed = _stat.RunSpeed;
            _anim.SetFloat("Speed", 1f);
        }
        else
        {
            _stat.Speed = _stat.WalkSpeed;
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

        transform.position += _dir.normalized * _stat.Speed * Time.deltaTime;
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
                _rb.AddForce(_dir * _stat.DodgePower, ForceMode.Impulse);
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
}