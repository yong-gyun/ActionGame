using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
using static Define;

public class DataManager
{
    public PlayerStatData PlayerStat { get; private set; } = new PlayerStatData(100f, 100f, 20f, 4f, 6f, 12.5f);
    public Dictionary<MonsterId, MonsterStatData> MonsterStat { get; private set; } = new Dictionary<MonsterId, MonsterStatData>();
    public Dictionary<BuyableItem, BuyableItemData> BuyableItem { get; private set; } = new Dictionary<BuyableItem, BuyableItemData>();
    public List<ShopItemData> ShopItem { get; private set; } = new List<ShopItemData>();

    public DataManager()
    {
        Init();
    }

    public void Init()
    {
        Init_MonsterStatData();
        Init_BuyableItemData();
        Init_ShopItemData();
    }

    void Init_MonsterStatData()
    {
        MonsterStat.Add(MonsterId.TurtleSlime, new MonsterStatData(100f, 5f, 4f, 2.5f));
        MonsterStat.Add(MonsterId.BigSlime, new MonsterStatData(100f, 5f, 4f, 2.5f));
        MonsterStat.Add(MonsterId.Slime, new MonsterStatData(25f, 5f, 4f, 2.5f));
    }

    void Init_BuyableItemData()
    {
        BuyableItem.Add(Define.BuyableItem.Hp_Potion, new BuyableItemData(8, 10));
        BuyableItem.Add(Define.BuyableItem.Mp_Potion, new BuyableItemData(5, 10));
        BuyableItem.Add(Define.BuyableItem.HpUpgrade, new BuyableItemData(10, 10));
        BuyableItem.Add(Define.BuyableItem.MpUpgrade, new BuyableItemData(10, 15));
        BuyableItem.Add(Define.BuyableItem.AttackUpgrade, new BuyableItemData(10, 8));
        BuyableItem.Add(Define.BuyableItem.SpeedUpgrade, new BuyableItemData(10, 5));
    }
    void Init_ShopItemData()
    {
        ShopItem.Add(new ShopItemData(Define.BuyableItem.Hp_Potion, "ü�� ȸ������", $"ü���� {HP_HEAL} ȸ����Ų��.", BuyableItem[Define.BuyableItem.Hp_Potion].cost));
        ShopItem.Add(new ShopItemData(Define.BuyableItem.Mp_Potion, "���� ȸ������", $"������ {MP_HEAL} ȸ����Ų��.", BuyableItem[Define.BuyableItem.Mp_Potion].cost));
        ShopItem.Add(new ShopItemData(Define.BuyableItem.HpUpgrade, "ü�� ����", $"ü���� {UPGRADE_HP} ������Ų��.", BuyableItem[Define.BuyableItem.HpUpgrade].cost));
        ShopItem.Add(new ShopItemData(Define.BuyableItem.MpUpgrade, "���� ����", $"������ {UPGRADE_MP} ������Ų��.", BuyableItem[Define.BuyableItem.MpUpgrade].cost));
        ShopItem.Add(new ShopItemData(Define.BuyableItem.AttackUpgrade, "���ݷ� ����", $"���ݷ��� {UPGRADE_ATTACK} ������Ų��.", BuyableItem[Define.BuyableItem.AttackUpgrade].cost));
        ShopItem.Add(new ShopItemData(Define.BuyableItem.SpeedUpgrade, "�̵��ӵ� ����", $"�̵��ӵ��� {UPGRADE_SPEED} ������Ų��.", BuyableItem[Define.BuyableItem.SpeedUpgrade].cost));
    }

    public void Clear()
    {
        
    }
}