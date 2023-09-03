using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Stat : MonoBehaviour
{
    public float MaxHp { get; set; }
    public float Hp { get; set; }
    public float Attack { get; set; }
    public float Speed { get; set; }
    protected bool _init;

    void Start()
    {
        Init();
    }

    protected virtual bool Init()
    {
        if(_init)
            return false;

        _init = true;
        return true;
    }
}