
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

public class DataManager
{
    public Dictionary<Define.WorldObject, EnemyStatData> StatDatas = new Dictionary<Define.WorldObject, EnemyStatData>();

    public void Init()
    {
        StatDatas.Add(Define.WorldObject.Slime, new EnemyStatData(20f, 5f, 4f, 1.5f));
    }

    public void Clear()
    {

    }
}
