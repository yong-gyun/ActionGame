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

    protected override void Init()
    {
        BindObject(typeof(GameObjects));
        BindText(typeof(Texts));
        BindButton(typeof(Buttons));

        GetButton((int)Buttons.PotionTabButton).onClick.AddListener(OnClickPotionTabButton);
        GetButton((int)Buttons.UpgradeTabButton).onClick.AddListener(OnClickUpgradeTabButton);
        GetButton((int)Buttons.CloseButton).onClick.AddListener(ClosePopupUI);

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
    }
}
