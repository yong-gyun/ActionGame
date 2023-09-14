using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillOne : BaseSkill
{
    protected override bool Init()
    {
        if(base.Init() == false)
            return false;
        _type = Define.SkillType.Skill_1;
        _skillFunc += CreateSkillEffect;
        SetStat();
        return true;
    }

    GameObject CreateSkillEffect()
    {
        GameObject go = Managers.Resource.Instantiate("Effect/SkillOne_Effect");
        go.transform.position = _player.transform.position + Vector3.up;
        go.transform.rotation = Quaternion.LookRotation(_player.transform.forward);
        return go;
    }
}
