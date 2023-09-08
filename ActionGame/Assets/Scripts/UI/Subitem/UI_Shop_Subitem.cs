using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Shop_Subitem : UI_Base
{
    enum Images
    {
        ItemImage
    }

    enum Texts
    {
        PriceText,
        DescriptionText,
        NameText,
    }

    public void SetInfo(Data.ShopItemData data)
    {
        GetImage((int)Images.ItemImage).sprite = Managers.Resource.Load<Sprite>($"Sprite/Icon/{data.type}");
        GetText((int)Texts.NameText).text = data.name;
        GetText((int)Texts.DescriptionText).text = data.description;
        GetText((int)Texts.PriceText).text = $"{data.cost}";
    }

    protected override void Init()
    {
        BindImage(typeof(Images));
        BindText(typeof(Texts));
    }
}
