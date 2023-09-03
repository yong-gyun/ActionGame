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
    PlayerState _state;
    Vector3 _dir;

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

        _rb.velocity = _dir.normalized * _stat.Speed;
    }

    void UpdateDefense()
    {
        
    }

    void OnDodge(int idx)
    {
        switch (idx)
        {
            case 0:
                _rb.AddForce(_dir * _stat.DodgePower, ForceMode.Impulse);
                break;
            case 1:
                State = PlayerState.Idle;
                break;
        }
    }
}