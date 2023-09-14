using Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Shop : UI_Popup
{
    enum Texts
    {
        GoldText,
        StatText
    }

    enum GameObjects
    {
        BuyPotionTab,
        BuyStatUpgradeTab
    }

    enum Buttons
    {
        PotionTabButton,
        UpgradeTabButton,
        CloseButton
    }

    PlayerController _player;
    PlayerStatData _statData;

    protected override void Init()
    {
        base.Init();
        BindObject(typeof(GameObjects));
        BindText(typeof(Texts));
        BindButton(typeof(Buttons));

        GetButton((int)Buttons.PotionTabButton).onClick.AddListener(OnClickPotionTabButton);
        GetButton((int)Buttons.UpgradeTabButton).onClick.AddListener(OnClickUpgradeTabButton);
        GetButton((int)Buttons.CloseButton).onClick.AddListener(ClosePopupUI);
        _player = Managers.Object.Player;
        _statData = Managers.Data.PlayerStat;

        List<ShopItemData> data = Managers.Data.ShopItem;

        for (int i = 0; i < data.Count; i++)
        {
            UI_Shop_Subitem subitem = null;

            if (data[i].type.ToString().Contains("Potion"))
                subitem = Managers.UI.MakeSubitemUI<UI_Shop_Subitem>(GetObject((int)GameObjects.BuyPotionTab).gameObject.FindChild("Content", true).transform);
            else if (data[i].type.ToString().Contains("Upgrade"))
                subitem = Managers.UI.MakeSubitemUI<UI_Shop_Subitem>(GetObject((int)GameObjects.BuyStatUpgradeTab).gameObject.FindChild("Content", true).transform);

            subitem.SetInfo(data[i]);
        }

        OnClickPotionTabButton();
        Time.timeScale = 0f;
    }

    protected override void ClosePopupUI()
    {
        base.ClosePopupUI();
        Time.timeScale = 1f;
    }

    void OnClickPotionTabButton()
    {
        GetObject((int)GameObjects.BuyStatUpgradeTab).SetActive(false);
        GetObject((int)GameObjects.BuyPotionTab).SetActive(true);
        Color selectedColor = GetButton((int)Buttons.PotionTabButton).colors.selectedColor;
        GetButton((int)Buttons.PotionTabButton).image.color = selectedColor;
        Color normalColor = GetButton((int)Buttons.UpgradeTabButton).colors.normalColor;
        GetButton((int)Buttons.UpgradeTabButton).image.color = normalColor;
    }

    void OnClickUpgradeTabButton()
    {
        GetObject((int)GameObjects.BuyPotionTab).SetActive(false);
        GetObject((int)GameObjects.BuyStatUpgradeTab).SetActive(true);
        Color selectedColor = GetButton((int)Buttons.UpgradeTabButton).colors.selectedColor;
        GetButton((int)Buttons.UpgradeTabButton).image.color = selectedColor;
        Color normalColor = GetButton((int)Buttons.PotionTabButton).colors.normalColor;
        GetButton((int)Buttons.PotionTabButton).image.color = normalColor;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Managers.UI.ClosePopupUI();

        GetText((int)Texts.GoldText).text = $"{Managers.Game.CurrentGold}G";

        GetText((int)Texts.StatText).text =
            $@"체력 : {_player.MaxHp} ({_statData.maxHp} + {Managers.Game.PlayerStatUpgradeCount[(int)Define.StatUpgrade.Hp_Upgrade] * Define.UPGRADE_HP}) 
            마나 : {_player.MaxMp} ({_statData.maxHp} + {Managers.Game.PlayerStatUpgradeCount[(int)Define.StatUpgrade.Mp_Upgrade] * Define.UPGRADE_MP})
            공격력 : {_player.Attack} ({_statData.attack} + {Managers.Game.PlayerStatUpgradeCount[(int)Define.StatUpgrade.Attack_Upgrade] * Define.UPGRADE_ATTACK})
            기본 이동속도 : {_player.WalkSpeed} ({_statData.walkSpeed} + {Managers.Game.PlayerStatUpgradeCount[(int)Define.StatUpgrade.Speed_Upgrade] * Define.UPGRADE_SPEED})
            달리기 속도 : {_player.RunSpeed} ({_statData.runSpeed} + {Managers.Game.PlayerStatUpgradeCount[(int)Define.StatUpgrade.Speed_Upgrade] * Define.UPGRADE_SPEED})";
    }
}
