using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class PlayerController : BaseController
{

    public Define.PlayerState State
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
                case Define.PlayerState.Idle:
                    _anim.CrossFade("Idle", 0.1f);
                    break;
            }    
        }
    }

    [SerializeField] Define.PlayerState _state;

    PlayerStat _stat;
    GameObject _weapone;

    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        Managers.Game.SetPlayer(gameObject);
        _stat = gameObject.GetOrAddComponent<PlayerStat>();
        
        RefreshPlayer();
        return true;
    }

    public void RefreshPlayer()
    {
        _stat.Init();
    }


    protected override void OnAttack()
    {
        
    }

    void OnHitEvent()
    {

    }

    void OnEndHitEvent()
    {
        
    }

    protected override void UpdateAttack()
    {
        if(Input.GetMouseButtonDown(0))
        {
            
        }
    }

    protected override void UpdateMove()    
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 dir = (Vector3.right * horizontal) + (Vector3.forward * vertical);
        
        if(dir != Vector3.zero)
        {
            float value = 0;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                _stat.MoveSpeed = _stat.MoveSpeedToRun;
                value = 1f;
            }
            else
            {
                _stat.MoveSpeed = _stat.MoveSpeedToWalk;
                value = 0.5f;
            }
            transform.position += dir * _stat.MoveSpeed * Time.deltaTime;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 180);

            _anim.SetFloat("Speed", value);
        }
        else
        {
            _anim.SetFloat("Speed", 0f);
        }
    }
}