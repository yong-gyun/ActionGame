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
        base.Init();
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
        {
            Managers.UI.ShowEffectPopupUI<UI_Alert>().OnAlert($"{Mathf.Abs(price)}G가 부족합니다.");
            return;
        }

        switch(_type)
        {
            case BuyableItem.Hp_Potion:
                if (Managers.Data.BuyableItem[BuyableItem.Hp_Potion].maxCount > Managers.Game.CurrentPositionsCount[(int)Potion.Hp_Potion])
                {
                    Managers.Game.CurrentPositionsCount[(int)Potion.Hp_Potion]++;

                    if (Managers.Game.GetPlayer.MaxHp < Managers.Game.GetPlayer.Hp)
                        Managers.Game.GetPlayer.Hp = Managers.Game.GetPlayer.MaxHp;
                    execute = true;
                }
                break;
            case BuyableItem.Mp_Potion:
                if (Managers.Data.BuyableItem[BuyableItem.Mp_Potion].maxCount > Managers.Game.CurrentPositionsCount[(int)Potion.Mp_Potion])
                {
                    Managers.Game.CurrentPositionsCount[(int)Potion.Mp_Potion]++;
                    
                    if (Managers.Game.GetPlayer.MaxMp < Managers.Game.GetPlayer.Mp)
                        Managers.Game.GetPlayer.Mp = Managers.Game.GetPlayer.MaxMp;
                    execute = true;
                }
                break;
            case BuyableItem.Attack_Upgrade:
                if (Managers.Data.BuyableItem[BuyableItem.Attack_Upgrade].maxCount > Managers.Game.PlayerStatUpgradeCount[(int)StatUpgrade.Attack_Upgrade] && Managers.Data.BuyableItem[BuyableItem.Attack_Upgrade].maxCount >= count)
                {
                    Managers.Game.PlayerStatUpgradeCount[(int)StatUpgrade.Attack_Upgrade] += count;
                    Managers.Game.GetPlayer.Attack += UPGRADE_ATTACK * count;
                    execute = true;
                }
                break;
            case BuyableItem.Speed_Upgrade:
                if (Managers.Data.BuyableItem[BuyableItem.Speed_Upgrade].maxCount > Managers.Game.PlayerStatUpgradeCount[(int)StatUpgrade.Speed_Upgrade] && Managers.Data.BuyableItem[BuyableItem.Speed_Upgrade].maxCount >= count)
                {
                    Managers.Game.PlayerStatUpgradeCount[(int)StatUpgrade.Speed_Upgrade] += count;
                    Managers.Game.GetPlayer.WalkSpeed += UPGRADE_SPEED * count;
                    Managers.Game.GetPlayer.RunSpeed += UPGRADE_SPEED * count;
                    execute = true;
                }
                break;
            case BuyableItem.Hp_Upgrade:
                if (Managers.Data.BuyableItem[BuyableItem.Hp_Upgrade].maxCount > Managers.Game.PlayerStatUpgradeCount[(int)StatUpgrade.Hp_Upgrade] && Managers.Data.BuyableItem[BuyableItem.Hp_Upgrade].maxCount >= count)
                {
                    Managers.Game.PlayerStatUpgradeCount[(int)StatUpgrade.Hp_Upgrade] += count;
                    Managers.Game.GetPlayer.MaxHp += UPGRADE_HP * count;
                    execute = true;
                }
                break;
            case BuyableItem.Mp_Upgrade:
                if (Managers.Data.BuyableItem[BuyableItem.Mp_Upgrade].maxCount > Managers.Game.PlayerStatUpgradeCount[(int)StatUpgrade.Mp_Upgrade] && Managers.Data.BuyableItem[BuyableItem.Mp_Upgrade].maxCount >= count)
                {
                    Managers.Game.PlayerStatUpgradeCount[(int)StatUpgrade.Mp_Upgrade] += count;
                    Managers.Game.GetPlayer.MaxMp += UPGRADE_MP * count;
                    execute = true;
                }
                break;
        }

        if (execute == false)
        {
            Managers.UI.ShowEffectPopupUI<UI_Alert>().OnAlert($"{Managers.Data.ShopItem[(int)_type].name}은/는 \n최대 {Managers.Data.BuyableItem[_type].maxCount}까지 구매 가능합니다.");
            return;
        }

        Managers.Game.CurrentGold -= price;
    }

    void OnClickCancleButton()
    {
        Managers.UI.ClosePopupUI();
    }
}
