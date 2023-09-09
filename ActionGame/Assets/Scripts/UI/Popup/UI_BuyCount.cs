using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class UI_BuyCount : UI_Popup
{
    enum InputFields
    {
        InputCount
    }

    enum Buttons
    {
        ConfirmButton,
        CancleButton
    }

    BuyableItem _type;
    

    int _cost;
    int _maxCount;

    protected override void Init()
    {
        BindInputField(typeof(InputFields));
        BindButton(typeof(Buttons));
        GetButton((int)Buttons.ConfirmButton).onClick.AddListener(OnClickConfirmButton);
        GetButton((int)Buttons.CancleButton).onClick.AddListener(OnClickCancleButton);
    }

    public void SetInfo(BuyableItem type, int cost)
    {
        _type = type;
        _cost = cost;
    }

    void OnClickConfirmButton()
    {
        int count = int.Parse(GetInputField((int)InputFields.InputCount).text);
        int price = _cost * count;
        int value = Managers.Game.CurrentGold - price;
        bool execute = false;

        if (value < 0)
            return;

        switch(_type)
        {
            case BuyableItem.Hp_Potion:
                if (Managers.Data.BuyableItem[BuyableItem.Hp_Potion].maxCount > Managers.Game.CurrentPositionsCount[(int)Potion.Hp_Heal])
                {
                    Managers.Game.CurrentPositionsCount[(int)Potion.Hp_Heal]++;
                    Managers.Game.GetPlayer.Hp += HP_HEAL;

                    if (Managers.Game.GetPlayer.MaxHp < Managers.Game.GetPlayer.Hp)
                        Managers.Game.GetPlayer.Hp = Managers.Game.GetPlayer.MaxHp;
                    execute = true;
                }
                break;
            case BuyableItem.Mp_Potion:
                if (Managers.Data.BuyableItem[BuyableItem.Mp_Potion].maxCount > Managers.Game.CurrentPositionsCount[(int)Potion.Mp_Heal])
                {
                    Managers.Game.CurrentPositionsCount[(int)Potion.Mp_Heal]++;
                    Managers.Game.GetPlayer.Mp += MP_HEAL;

                    if (Managers.Game.GetPlayer.MaxMp < Managers.Game.GetPlayer.Mp)
                        Managers.Game.GetPlayer.Mp = Managers.Game.GetPlayer.MaxMp;
                    execute = true;
                }
                else
                    return;
                break;
            case BuyableItem.AttackUpgrade:
                if (Managers.Data.BuyableItem[BuyableItem.AttackUpgrade].maxCount > Managers.Game.PlayerStatUpgradeCount[(int)StatUpgrade.Attack])
                {
                    Managers.Game.PlayerStatUpgradeCount[(int)StatUpgrade.Attack]++;
                    Managers.Game.GetPlayer.Attack += UPGRADE_ATTACK;
                    execute = true;
                }
                else
                    return;
                break;
            case BuyableItem.SpeedUpgrade:
                if (Managers.Data.BuyableItem[BuyableItem.SpeedUpgrade].maxCount > Managers.Game.PlayerStatUpgradeCount[(int)StatUpgrade.Speed])
                {
                    Managers.Game.PlayerStatUpgradeCount[(int)StatUpgrade.Speed]++;
                    Managers.Game.GetPlayer.WalkSpeed += UPGRADE_SPEED;
                    Managers.Game.GetPlayer.RunSpeed += UPGRADE_SPEED;
                    execute = true;
                }
                else
                    return;
                break;
            case BuyableItem.HpUpgrade:
                if (Managers.Data.BuyableItem[BuyableItem.SpeedUpgrade].maxCount > Managers.Game.PlayerStatUpgradeCount[(int)StatUpgrade.Speed])
                {
                    Managers.Game.PlayerStatUpgradeCount[(int)StatUpgrade.Hp]++;
                    Managers.Game.GetPlayer.MaxHp += UPGRADE_HP;
                    execute = true;
                }
                break;
            case BuyableItem.MpUpgrade:
                if (Managers.Data.BuyableItem[BuyableItem.MpUpgrade].maxCount > Managers.Game.PlayerStatUpgradeCount[(int)StatUpgrade.Mp])
                {
                    Managers.Game.PlayerStatUpgradeCount[(int)StatUpgrade.Mp]++;
                    Managers.Game.GetPlayer.MaxMp += UPGRADE_MP;
                    execute = true;
                }
                else
                    return;
                break;
        }

        if (execute == false)
            return;

        Managers.Game.CurrentGold -= price;
    }

    void OnClickCancleButton()
    {
        Managers.UI.ClosePopupUI();
    }
}
