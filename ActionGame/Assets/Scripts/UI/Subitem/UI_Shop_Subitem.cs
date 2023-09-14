using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    Data.ShopItemData _info;
    Button _btn;
    
    public void SetInfo(Data.ShopItemData info)
    {
        _info = info;
        GetImage((int)Images.ItemImage).sprite = Managers.Resource.Load<Sprite>($"Sprite/Icon/{_info.type}");
        GetText((int)Texts.NameText).text = _info.name;
        GetText((int)Texts.DescriptionText).text = _info.description;
        GetText((int)Texts.PriceText).text = $"{_info.cost}G";

        _btn = GetComponent<Button>();
        _btn.onClick.AddListener(OnClickShotSubitemButton);
    }

    void OnClickShotSubitemButton()
    {
        Managers.UI.ShowPopupUI<UI_BuyCount>().SetInfo(_info.type, _info.cost);
    }

    private void Update()
    {
        int cnt = 0;

        if (_info.type.ToString().Contains("Upgrade"))
        {

            switch(_info.type)
            {
                case Define.BuyableItem.Hp_Upgrade:
                    cnt = Managers.Game.PlayerStatUpgradeCount[(int)Define.StatUpgrade.Hp_Upgrade];
                    break;
                case Define.BuyableItem.Attack_Upgrade:
                    cnt = Managers.Game.PlayerStatUpgradeCount[(int)Define.StatUpgrade.Attack_Upgrade];
                    break;
                case Define.BuyableItem.Mp_Upgrade:
                    cnt = Managers.Game.PlayerStatUpgradeCount[(int)Define.StatUpgrade.Mp_Upgrade];
                    break;
                case Define.BuyableItem.Speed_Upgrade:
                    cnt = Managers.Game.PlayerStatUpgradeCount[(int)Define.StatUpgrade.Speed_Upgrade];
                    break;
            }

            if (Managers.Data.BuyableItem[_info.type].maxCount <= cnt)
            {
                _btn.interactable = false;
            }
        }
        else
        {

        }
    }

    protected override void Init()
    {
        BindImage(typeof(Images));
        BindText(typeof(Texts));
    }
}
