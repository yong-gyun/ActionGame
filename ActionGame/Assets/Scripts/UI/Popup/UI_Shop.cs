using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Shop : UI_Popup
{
    enum GameObjects
    {
        Content
    }

    protected override void Init()
    {
        BindObject(typeof(GameObjects));
        ShopItemData[] data = Managers.Data.ShopItem.ToArray();

        for (int i = 0; i < data.Length; i++)
        {
            UI_Shop_Subitem subitem = Managers.UI.MakeSubitemUI<UI_Shop_Subitem>(GetObject((int)GameObjects.Content).transform);
            subitem.SetInfo(data[i]);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Managers.UI.ClosePopupUI();
    }
}
