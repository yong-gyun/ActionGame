using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class TestScene : BaseScene
{
    [SerializeField] Transform _spawnPos;
    GameObject _shopUI;
    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        Managers.Object.SpawnMonster(MonsterId.BigSlime).transform.position = _spawnPos.position;

        //Managers.Object.SpawnMonster(MonsterId.BigSlime); 
        return true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && _shopUI == null)
            _shopUI = Managers.UI.ShowPopupUI<UI_Shop>().gameObject;
    }
}
