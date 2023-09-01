using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    PlayerStat _stat;

    public Define.PlayerState State
    {
        get
        {
            return _state;
        }
        set
        {
            //TODO @이벤트 전환 부드럽게 만들기
            //float smoothDamp = 0.1f;
            float transitionDuration = 0.1f;
            float normalizedTimeOffset = 0.1f;
            _state = value;

            switch (value)
            {
                case Define.PlayerState.Idle:
                    _anim.CrossFade("Idle", transitionDuration, 0, normalizedTimeOffset);
                    break;
                case Define.PlayerState.Move:
                    _anim.CrossFade("Move", transitionDuration, 0, normalizedTimeOffset);
                    break;
                case Define.PlayerState.Attack:
                    _anim.CrossFade("Attack", transitionDuration, 0, normalizedTimeOffset);
                    break;
                case Define.PlayerState.Defense:
                    _anim.CrossFade("Defense", transitionDuration, 0, normalizedTimeOffset);
                    break;
                case Define.PlayerState.AttackAvoidance:
                    _anim.CrossFade("Attackaboidance", transitionDuration, 0, normalizedTimeOffset);
                    break;
                case Define.PlayerState.SpecalAttack_One:
                    _anim.CrossFade("SkillOne", transitionDuration, 0, normalizedTimeOffset);
                    break;
                case Define.PlayerState.SpecalAttack_Two:
                    _anim.CrossFade("SkillTwo", transitionDuration, 0, normalizedTimeOffset);
                    break;
            }
        }
    }

    Rigidbody _rigid;
    [SerializeField] Define.PlayerState _state;
    [SerializeField] Vector3 _dir;
    [SerializeField] bool _useSkill;
    Collider _sword;
    Collider _shield;

    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        _stat = gameObject.GetOrAddComponent<PlayerStat>();
        _rigid = GetComponent<Rigidbody>();
        return true;
    }

    void Update()
    {
        if (State == Define.PlayerState.Die)
            return;

        OnKeyEvent();
        OnMouseEvent();

        switch (State)
        {
            case Define.PlayerState.Move:
                UpdateMove();
                break;
            case Define.PlayerState.Attack:
                UpdateAttack();
                break;
            case Define.PlayerState.Defense:
                UpdateDefence();
                break;
        }
    }

    protected override void Refresh()
    {
        _stat.SetStat();
    }

    void OnKeyEvent()
    {
        if (_useSkill)
            return;

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            OnAttackaboidance(0);
            return;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            State = Define.PlayerState.SpecalAttack_One;
            _useSkill = true;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            State = Define.PlayerState.SpecalAttack_Two;
            _useSkill = true;
            return;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        _dir = (horizontal * Vector3.right) + (vertical * Vector3.forward);

        if (_dir != Vector3.zero)
        {
            if (Input.GetKey(KeyCode.LeftShift))
                _stat.MoveSpeed = _stat.MoveSpeedToRun;
            else
                _stat.MoveSpeed = _stat.MoveSpeedToWalk;
            
            if (State == Define.PlayerState.Idle)
                State = Define.PlayerState.Move;
        }
    }

    void OnMouseEvent()
    {
        if (!_useSkill)
        {
            _useSkill = Input.GetMouseButtonDown(0) || Input.GetMouseButton(1);

            if (Input.GetMouseButtonDown(0))
            {
                State = Define.PlayerState.Attack;
                return;
            }
            else if (Input.GetMouseButton(1))
            {
                State = Define.PlayerState.Defense;
                return;
            }
        }
        else
        {
            if (Input.GetMouseButtonUp(1))
            {
                _useSkill = false;
                State = Define.PlayerState.Idle;
            }
        }
    }

    protected override void UpdateMove()
    {
        if (_dir == Vector3.zero)
        {
            State = Define.PlayerState.Idle;
            return;
        }

        if (Physics.Raycast(transform.position, transform.forward, 2.5f, LayerMask.GetMask("Block")))
            return;

        float destSpeed = _stat.MoveSpeed / _stat.MoveSpeedToRun == 1 ? 1f : 0.5f;
        _anim.SetFloat("Speed", destSpeed);


        if (_dir != Vector3.zero)
        {
            transform.position += _dir.normalized * _stat.MoveSpeed * Time.deltaTime;
            Quaternion qua = Quaternion.LookRotation(_dir.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, qua, 20f * Time.deltaTime);
        }
    }

    protected override void UpdateAttack()
    {

    }

    void UpdateDefence()
    {

    }

    float _dashPower = 5f;

    void OnAttackaboidance(int sequnce)
    {
        switch(sequnce)
        {
            case 0:
                _rigid.AddForce(_dir * _dashPower, ForceMode.Impulse);
                State = Define.PlayerState.AttackAvoidance;
                break;
            case 1:
                State = Define.PlayerState.Idle;
                break;
        }
    }

    void OnAttack(int sequnce)
    {
        switch (sequnce)
        {
            case 0:
                //무기 콜라이더 ON
                break;
            case 1:
                //무기 콜라이더 OFF
                break;
            case 2:
                //무기 애니메이션 OFF
                State = Define.PlayerState.Idle;
                _useSkill = false;
                break;
        }
    }

    void OnSkillOne()
    {
        State = Define.PlayerState.Idle;
        _useSkill = false;
    }

    void OnSkillTwo()
    {
        State = Define.PlayerState.Idle;
        _useSkill = false;
    }

    protected override void OnDie()
    {

    }
}
