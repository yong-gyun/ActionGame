using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
using static Define;

public class DataManager
{
    public PlayerStatData PlayerStatData { get; private set; }
    public Dictionary<WorldObject, EnemyStatData> EnemyStatData { get; private set; } = new Dictionary<Define.WorldObject, EnemyStatData>();

    public DataManager()
    {
        Init();
    }

    public void Init()
    {
        PlayerStatData = new PlayerStatData(100f, 100f, 20f, 4f, 6f, 12.5f);
        EnemyStatData.Add(WorldObject.BigSlime, new EnemyStatData(20f, 5f, 4f, 2f));
    }

    public void Clear()
    {
        
    }
}
