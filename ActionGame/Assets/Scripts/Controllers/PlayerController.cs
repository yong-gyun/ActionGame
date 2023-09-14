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
                case PlayerState.Skill_1:
                    _anim.CrossFade("Skill_1", 0.1f);
                    break;
                case PlayerState.Skill_2:
                    _anim.CrossFade("Skill_2", 0.1f);
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
    Collider _shieldCollider;
    Sword _sword;

    public PlayerSkillOne SkillOne { get { return _skillOne; } }
    public PlayerSkillTwo SkillTwo { get { return _skillTwo; } }


    PlayerSkillOne _skillOne;
    PlayerSkillTwo _skillTwo;
    
    public GameObject ShieldItem { get; set; }

    [SerializeField] Vector3 _dir;
    [SerializeField] PlayerState _state;
    int _attackCombo = 0;
    bool _isAttacked;
    bool _useSkill;

    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        if (gameObject.GetComponentInChildren<UI_PlayerStatBar>() == null)
            Managers.UI.MakeWorldSpaceUI<UI_PlayerStatBar>(transform);

        _shieldCollider = gameObject.FindChild<Collider>("Shield", true);
        _shieldCollider.enabled = false;
        SetStat();
        
        GameObject go = gameObject.FindChild("Sword", true);
        _sword = go.GetOrAddComponent<Sword>();
        _sword.SetDamage(Attack);
        _sword.Enabled = false;
        tag = "Player";
        _skillOne = gameObject.GetOrAddComponent<PlayerSkillOne>();
        _skillTwo = gameObject.GetOrAddComponent<PlayerSkillTwo>();
        Managers.Input.MouseAction += UpdateMouseEvent;
        Managers.Input.KeyAction += UpdateKeyEvent;
        return true;
    }

    public void SetStat()
    {
        MaxHp = Managers.Data.PlayerStat.maxHp + Managers.Game.PlayerStatUpgradeCount[(int) StatUpgrade.Hp_Upgrade] * UPGRADE_HP;
        Hp = MaxHp;
        MaxMp = Managers.Data.PlayerStat.maxMp + Managers.Game.PlayerStatUpgradeCount[(int) StatUpgrade.Mp_Upgrade] * UPGRADE_MP;
        Mp = MaxMp;
        Attack = Managers.Data.PlayerStat.attack + Managers.Game.PlayerStatUpgradeCount[(int)StatUpgrade.Attack_Upgrade] * UPGRADE_ATTACK;
        DodgePower = Managers.Data.PlayerStat.dodgePower;
        RunSpeed = Managers.Data.PlayerStat.runSpeed + Managers.Game.PlayerStatUpgradeCount[(int)StatUpgrade.Speed_Upgrade] * UPGRADE_SPEED;
        WalkSpeed = Managers.Data.PlayerStat.walkSpeed + Managers.Game.PlayerStatUpgradeCount[(int)StatUpgrade.Speed_Upgrade] * UPGRADE_SPEED;
    }

    private void Update()
    {
        if (State == PlayerState.Die)
            return;

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
            case PlayerState.Attack:
                UpdateAttack();
                break;
        }
    }

    void UpdateMouseEvent()
    {
        if (State == PlayerState.Hit)
            return;

        if(Input.GetMouseButtonDown(0) && State != PlayerState.Attack)
        {
            if (_attackCombo == 3)
                _attackCombo = 0;

            State = PlayerState.Attack;
            _anim.SetFloat("AttackCombo", _attackCombo++);
        }
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

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            OnDodge(0);
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

        if(Input.GetKeyDown(KeyCode.Q))
        {
            if (_skillOne.SkillUsedable() == false)
                return;

            State = PlayerState.Skill_1;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            if (_skillTwo.SkillUsedable() == false)
                return;

            State = PlayerState.Skill_2;
            ForWait(0.5f, () =>
            {
                _rb.AddForce(transform.forward * 5f, ForceMode.Impulse);
            });
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
        Quaternion targetRotation = Quaternion.LookRotation(_dir, Vector3.up); 
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 20f * Time.deltaTime);
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

    protected override void UpdateAttack()
    {
        if (_isAttacked)
            return;

        State = PlayerState.Idle;
    }

    void OnUseSkillOne()
    {
        _skillOne.UseSkill();
    }

    void OnUseSkillTwo()
    {
        _skillTwo.UseSkill();
    }

    void OnDodge(int idx)
    {
        switch (idx)
        {
            case 0:
                if (State == PlayerState.Dodge)
                    return;

                _rb.AddForce(transform.forward * DodgePower * 2.5f, ForceMode.Impulse);
                State = PlayerState.Dodge;
                break;
            case 1:
                _rb.velocity = Vector3.zero;
                State = PlayerState.Idle;
                break;
        }
    }

    void OnAttack(int idx)
    {
        ForWait(2f, () => { _attackCombo = 0; });

        switch(idx)
        {
            case 0:
                _sword.Enabled = true;
                _isAttacked = true;
                break;
            case 1:
                _sword.Enabled = false;
                _isAttacked = false;
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

        //점멸 종료 후 false로
        //_isInvin = true;
        //ForWait(1f, () => {  });
        Hp -= damage;

        _sword.Enabled = false;
        _isAttacked = false;

        if (State != PlayerState.Attack || State != PlayerState.Skill_1 || State != PlayerState.Skill_2)
            State = PlayerState.Hit;

        StartCoroutine(CoFlash());

        if (Hp <= 0)
        {
            OnDie();
        }
    }

    void OnDamagedAnimEvent() { State = PlayerState.Idle; }

    IEnumerator CoFlash()
    {
        //float time = 0f;
        //float destTime = 0.5f;

        _isInvin = true;
        //List<Material> mats = new List<Material>();

        //foreach (MeshRenderer mesh in gameObject.FindChild("Model").GetComponentsInChildren<MeshRenderer>())
        //{
        //    mats.Add(mesh.material);
        //}

        //float r = 1f;

        //while (r > 0.5f)
        //{
        //    time += Time.deltaTime / destTime;
        //    r = Mathf.Lerp(0f, 0.5f, time);

        //    foreach (Material mat in mats)
        //    {
        //        Color color = mat.color;
        //        color.g = r;
        //        color.b = r;
        //        mat.color = color;
        //    }

        //    yield return null;
        //}

        //time = 0f;

        //while (r > 0f)
        //{
        //    time += Time.deltaTime / destTime;
        //    r = Mathf.Lerp(1f, 0f, time);

        //    foreach (Material mat in mats)
        //    {
        //        Color color = mat.color;
        //        color.g = r; 
        //        color.b = r;
        //        mat.color = color;
        //    }

        //    yield return null;
        //}

        yield return new WaitForSeconds(1f);
        _isInvin = false;
    }
}