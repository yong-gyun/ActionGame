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
        ShopItem.Add(new ShopItemData(Define.BuyableItem.Hp_Potion, "체력 회복물약", $"체력을 {HP_HEAL} 회복시킨다.", BuyableItem[Define.BuyableItem.Hp_Potion].cost));
        ShopItem.Add(new ShopItemData(Define.BuyableItem.Mp_Potion, "마나 회복물약", $"마나를 {MP_HEAL} 회복시킨다.", BuyableItem[Define.BuyableItem.Mp_Potion].cost));
        ShopItem.Add(new ShopItemData(Define.BuyableItem.HpUpgrade, "체력 증가", $"체력을 {UPGRADE_HP} 증가시킨다.", BuyableItem[Define.BuyableItem.HpUpgrade].cost));
        ShopItem.Add(new ShopItemData(Define.BuyableItem.MpUpgrade, "마나 증가", $"마나를 {UPGRADE_MP} 증가시킨다.", BuyableItem[Define.BuyableItem.MpUpgrade].cost));
        ShopItem.Add(new ShopItemData(Define.BuyableItem.AttackUpgrade, "공격력 증가", $"공격력을 {UPGRADE_ATTACK} 증가시킨다.", BuyableItem[Define.BuyableItem.AttackUpgrade].cost));
        ShopItem.Add(new ShopItemData(Define.BuyableItem.SpeedUpgrade, "이동속도 증가", $"이동속도를 {UPGRADE_SPEED} 증가시킨다.", BuyableItem[Define.BuyableItem.SpeedUpgrade].cost));
    }

    public void Clear()
    {
        
    }
}
