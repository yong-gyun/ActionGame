using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillDamaged : MonoBehaviour
{
    Define.SkillType _type;
    float _attack;
    List<GameObject> _mcs = new List<GameObject>();

    public void Init(Define.SkillType type, float attack)
    {
        _type = type;
        _attack = attack;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy") && _mcs.Contains(other.gameObject))
        {
            string path = "";
            if (_type == Define.SkillType.Skill_1)
                path = "SkillOneHit";
            else
                path = "SkillTwoHit";

            GameObject go = Managers.Resource.Instantiate($"Effect/{path}");
            go.transform.position = other.transform.position;
            Managers.Resource.Destory(go, 1f);
            other.GetComponent<MonsterController>().OnDamaged(_attack);
            _mcs.Add(other.gameObject);
        }
    }
}
