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
        //ü�� 20ȸ�� (�κ��丮�� ����)
    }

    void OnClickHpIncreamentButton()
    {
        //ü�� 25 ���� (�ִ� 100)
    }

    void OnClickAttackIncreamentButton()
    {
        //���ݷ� 1���� (�ִ� 5)
    }

    void OnClickManaButton()
    {
        //�κ��丮�� ����
    }
}
