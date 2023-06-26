using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    PlayerStat _stat;

    protected override void Init()
    {
        Managers.Game.SetPlayer(gameObject);
        _stat = gameObject.GetOrAddComponent<PlayerStat>();
        _stat.Init();
        base.Init();
    }

    protected override void OnAttack()
    {
        
    }

    protected override void UpdateAttack()
    {
        
    }

    protected override void UpdateMove()    
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 dir = (Vector3.right * horizontal) + (Vector3.forward * vertical);
        
        if(dir != Vector3.zero)
        {
            if (Input.GetKey(KeyCode.LeftShift))
                _stat.MoveSpeed = _stat.MoveSpeedToRun;
            else
                _stat.MoveSpeed = _stat.MoveSpeedToWalk;

            transform.position += dir * _stat.MoveSpeed * Time.deltaTime;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 180);

            float value = _stat.MoveSpeed / _stat.MoveSpeedToRun;
            _anim.SetFloat("Speed", value);
        }
        else
        {
            _anim.SetFloat("Speed", 0f);
        }

    }
}