using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using TMPro;
using UnityEngine;

public class ObjectManager
{
    public PlayerController Player 
    { 
        get 
        {
            if (_player == null)
                return Object.FindObjectOfType<PlayerController>();
            return _player; 
        } 
    }
    PlayerController _player;

    public Define.WorldObject GetWorldObjectType(GameObject go)
    {
        BaseController bc = go.GetComponent<BaseController>();

        if (bc == null)
            return Define.WorldObject.Unknow;

        return bc.WorldObjectType;
    }

    public PlayerController SpawnPlayer()
    {
        GameObject go = Managers.Resource.Instantiate($"Creature/Player");

        if (go == null)
            return null;

        PlayerController pc = go.GetOrAddComponent<PlayerController>();
        pc.SetStat();
        _player = pc;
        return pc;
    }

    public MonsterController SpawnMonster(Define.MonsterId id)
    {
        GameObject go = Managers.Resource.Instantiate($"Creature/Monster/{id}");
        
        if(go == null)
            return null;
        
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
