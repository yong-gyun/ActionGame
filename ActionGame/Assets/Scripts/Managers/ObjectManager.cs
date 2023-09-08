using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectManager
{
    public Define.WorldObject GetWorldObjectType(GameObject go)
    {
        BaseController bc = go.GetComponent<BaseController>();

        if (bc == null)
            return Define.WorldObject.Unknow;

        return bc.WorldObjectType;
    }

    public MonsterController SpawnMonster(Define.MonsterId id)
    {
        GameObject go = Managers.Resource.Instantiate($"Creature/Monster/{id}");
        
        if(go == null)
        {
            Debug.Log($"z");
            return null;
        }
        
        MonsterController mc = go.GetComponent<MonsterController>();
        mc.MonsterId = id;
        mc.SetStat();
        return mc;
    }

    public void Despawn(GameObject go)
    {
        Managers.Resource.Destory(go);
    }
}
