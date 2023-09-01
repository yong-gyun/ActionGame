using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Shop : UI_Popup
{
    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        return true;
    }

    void OnClickHealButton()
    {
        //체력 20회복 (인벤토리에 저장)
    }

    void OnClickHpIncreamentButton()
    {
        //체력 25 증가 (최대 100)
    }

    void OnClickAttackIncreamentButton()
    {
        //공격력 1증가 (최대 5)
    }

    void OnClickManaButton()
    {
        //인벤토리에 저장
    }
}
