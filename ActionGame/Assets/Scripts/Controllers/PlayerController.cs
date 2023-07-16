using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : BaseController
{

    PlayerStat _stat;
    Animator _anim;
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
            float transitionDuration = 0.25f;

            if (value == Define.PlayerState.Attack)
                _anim.SetBool("IsAttack", false);
                //_anim.CrossFade("Attack", transitionDuration);

            if (value == Define.PlayerState.Defense)
                _anim.SetBool("IsDefense", false);
                //_anim.CrossFade("Defense", transitionDuration);

            switch (value)
            {
                case Define.PlayerState.Idle:
                    _anim.SetFloat("Speed", 0f);
                    //_anim.CrossFade("Idle", transitionDuration);
                    break;
                case Define.PlayerState.Move:
                    float speed = _stat.MoveSpeed / _stat.MoveSpeedToRun == 1 ? 1f : 0.5f;
                    _anim.SetFloat("Speed", speed);
                    //_anim.CrossFade("Move", transitionDuration);
                    break;
                case Define.PlayerState.Attack:
                    if (value == Define.PlayerState.Move)
                        _anim.SetFloat("Speed", 0f);

                    _anim.SetBool("IsAttack", true);
                    //_anim.CrossFade("Attack", transitionDuration);
                    break;
                case Define.PlayerState.Defense:
                    _anim.SetBool("IsDefense", true);
                    //_anim.CrossFade("Defense", transitionDuration);
                    break;
                case Define.PlayerState.AttackAvoidance:
                    _anim.SetTrigger("OnAttackaboiance");
                    //_anim.CrossFade("Attackavoidance", transitionDuration);
                    break;
                case Define.PlayerState.SpecalAttack_One:
                    _anim.SetTrigger("OnSkillOne");
                    break;
                case Define.PlayerState.SpecalAttack_Two:
                    _anim.SetTrigger("OnSkillTwo");
                    break;
            }

            _state = value;
        }
    }

    Rigidbody _rigid;
    [SerializeField] Define.PlayerState _state;
    Vector3 _dir;
    bool _isAttacked;

    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        _stat = gameObject.GetOrAddComponent<PlayerStat>();
        _anim = GetComponent<Animator>();
        _rigid = GetComponent<Rigidbody>();
        return true;
    }

    void Update()
    {
        if (State == Define.PlayerState.Die)
            return;

        OnKeyEvent();
        OnMouseEvent();

        switch(State)
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
        _stat.Refresh();
    }

    void OnKeyEvent()
    {
        if (_isAttacked)
            return;

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            OnAttackaboidance();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            State = Define.PlayerState.SpecalAttack_One;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            State = Define.PlayerState.SpecalAttack_Two;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        _dir = (horizontal * Vector3.right) + (vertical * Vector3.forward);

        if(_dir != Vector3.zero)
        {
            if (Input.GetKey(KeyCode.LeftShift))
                _stat.MoveSpeed = _stat.MoveSpeedToRun;
            else
                _stat.MoveSpeed = _stat.MoveSpeedToWalk;

            if(State == Define.PlayerState.Idle)
                State = Define.PlayerState.Move;
        }
    }

    void OnMouseEvent()
    {
        if(!_isAttacked)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isAttacked = true;
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
            if(Input.GetMouseButtonUp(0))
            {
                _isAttacked = false;
                State = Define.PlayerState.Idle;
                _anim.SetBool("IsAttack", false);
            }
        }

        if(Input.GetMouseButtonUp(1))
        {
            _anim.SetBool("IsDefense", false);
        }
    }

    protected override void UpdateMove()
    {
        if (_dir == Vector3.zero)
        {
            State = Define.PlayerState.Idle;
        }

        if (Physics.Raycast(transform.position, transform.forward, 2.5f, LayerMask.GetMask("Block")))
            return;

        if(_dir != Vector3.zero)
        {
            transform.position += _dir.normalized * _stat.MoveSpeed * Time.deltaTime;
            Quaternion qua = Quaternion.LookRotation(_dir.normalized);
            transform.rotation = qua;
        }
    }

    protected override void UpdateAttack()
    {

    }

    void UpdateDefence()
    {
        
    }

    float _dashPower = 5f;

    void OnAttackaboidance()
    {
        _rigid.AddForce(_dir * _dashPower, ForceMode.Impulse);
        State = Define.PlayerState.AttackAvoidance;
    }

    void OnAttack(int sequnce)
    {
        switch(sequnce)
        {
            case 0:
                //무기 콜라이더 ON
                break;
            case 1:
                //무기 콜라이더 OFF
                break;
            case 2:
                //무기 애니메이션 OFF
                break;
        }
    }

    void OnSkillOne()
    {
        State = Define.PlayerState.Idle;
    }

    void OnSkillTwo()
    {
        State = Define.PlayerState.Idle;
    }

    protected override void OnDie()
    {

    }
}
