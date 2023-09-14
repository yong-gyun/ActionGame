using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillTwo : BaseSkill
{
    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        _type = Define.SkillType.Skill_2;
        _skillFunc += CreateSkillEffect;
        SetStat();
        return true;
    }

    GameObject CreateSkillEffect()
    {
        GameObject go = Managers.Resource.Instantiate("Effect/PlayerSkillTwoArea");
        go.transform.position = _player.transform.position + Vector3.up;
        go.transform.parent = _player.transform;
        StartCoroutine(CoPlayerBuff());
        return go;
    }

    IEnumerator CoPlayerBuff()
    {
        float prevAttack = _player.Attack;
        float prevSpeed = _player.Speed;
        float prevMaxHp = _player.MaxHp;
        float prevHp = _player.Hp;

        _player.Attack += 5f;
        _player.Speed += 2f;
        _player.MaxHp += 15f;
        _player.Hp += 15f;

        yield return new WaitForSeconds(2f);

        _player.Attack = prevAttack;
        _player.Speed = prevSpeed;
        _player.MaxHp = prevMaxHp;
        _player.Hp = prevHp;
    }
}
