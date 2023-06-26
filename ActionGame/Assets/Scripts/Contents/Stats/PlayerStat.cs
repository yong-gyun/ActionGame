using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
    public int Gold { get { return _gold; } set { _gold = value; } }
    public float MoveSpeedToWalk { get { return _moveSpeedToWalk; } }
    public float MoveSpeedToRun { get { return _moveSpeedToRun; } }
    
    [SerializeField] int _gold;
    [SerializeField] float _moveSpeedToWalk;
    [SerializeField] float _moveSpeedToRun;

    public override void Init()
    {
        _moveSpeedToWalk = 5f;
        _moveSpeedToRun = 7.5f;
    }
}
