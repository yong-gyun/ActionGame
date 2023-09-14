using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
using static Define;

public class DataManager
{
    public PlayerStatData PlayerStat { get; private set; }
    public Dictionary<SkillType, PlayerSkillData> PlayerSkill = new Dictionary<SkillType, PlayerSkillData>();
    public Dictionary<MonsterId, MonsterStatData> MonsterStat { get; private set; } = new Dictionary<MonsterId, MonsterStatData>();
    public Dictionary<BuyableItem, BuyableItemData> BuyableItem { get; private set; } = new Dictionary<BuyableItem, BuyableItemData>();
    public Dictionary<int, string> StageInfo { get; private set; } = new Dictionary<int, string>();
    public List<ShopItemData> ShopItem { get; private set; } = new List<ShopItemData>();

    public DataManager()
    {
        Init();
    }

    public void Init()
    {
        LoadPlayerData();
        LoadMonsterStatData();
        LoadBuyableItemData();
        LoadShopItemData();
        LoadStageInfo();
    }

    void LoadPlayerData()
    {
        PlayerStat = new PlayerStatData(100f, 100f, 20f, 4f, 6f, 5f);
        PlayerSkill.Add(SkillType.Skill_1, new PlayerSkillData { type = SkillType.Skill_1, usedMp = 4f, cooltime = 3f, attack = 10f });
        PlayerSkill.Add(SkillType.Skill_2, new PlayerSkillData { type = SkillType.Skill_2, usedMp = 10f, cooltime = 5f, attack = 20f });
    }

    void LoadMonsterStatData()
    {
        MonsterStat.Add(MonsterId.TurtleSlime, new MonsterStatData(100f, 5f, 4f, 2f));
        MonsterStat.Add(MonsterId.BigSlime, new MonsterStatData(100f, 5f, 4f, 2.5f));
        MonsterStat.Add(MonsterId.MeleeSkeletone, new MonsterStatData(80f, 5f, 4f, 1.5f));
        //MonsterStat.Add(MonsterId.BigSlime, new MonsterStatData(100f, 5f, 4f, 2.5f));
        //MonsterStat.Add(MonsterId.BigSlime, new MonsterStatData(100f, 5f, 4f, 2.5f));
        MonsterStat.Add(MonsterId.Slime, new MonsterStatData(25f, 5f, 4f, 2.5f));
    }

    void LoadBuyableItemData()
    {
        BuyableItem.Add(Define.BuyableItem.Hp_Potion, new BuyableItemData(8, 10));
        BuyableItem.Add(Define.BuyableItem.Mp_Potion, new BuyableItemData(5, 10));
        BuyableItem.Add(Define.BuyableItem.Hp_Upgrade, new BuyableItemData(10, 10));
        BuyableItem.Add(Define.BuyableItem.Mp_Upgrade, new BuyableItemData(10, 15));
        BuyableItem.Add(Define.BuyableItem.Attack_Upgrade, new BuyableItemData(10, 8));
        BuyableItem.Add(Define.BuyableItem.Speed_Upgrade, new BuyableItemData(10, 5));
    }

    void LoadShopItemData()
    {
        ShopItem.Add(new ShopItemData(Define.BuyableItem.Hp_Potion, "체력 회복물약", $"체력을 {HP_HEAL} 회복시킨다.", BuyableItem[Define.BuyableItem.Hp_Potion].cost));
        ShopItem.Add(new ShopItemData(Define.BuyableItem.Mp_Potion, "마나 회복물약", $"마나를 {MP_HEAL} 회복시킨다.", BuyableItem[Define.BuyableItem.Mp_Potion].cost));
        ShopItem.Add(new ShopItemData(Define.BuyableItem.Hp_Upgrade, "체력 증가", $"체력을 {UPGRADE_HP} 증가시킨다.", BuyableItem[Define.BuyableItem.Hp_Upgrade].cost));
        ShopItem.Add(new ShopItemData(Define.BuyableItem.Mp_Upgrade, "마나 증가", $"마나를 {UPGRADE_MP} 증가시킨다.", BuyableItem[Define.BuyableItem.Mp_Upgrade].cost));
        ShopItem.Add(new ShopItemData(Define.BuyableItem.Attack_Upgrade, "공격력 증가", $"공격력을 {UPGRADE_ATTACK} 증가시킨다.", BuyableItem[Define.BuyableItem.Attack_Upgrade].cost));
        ShopItem.Add(new ShopItemData(Define.BuyableItem.Speed_Upgrade, "이동속도 증가", $"이동속도를 {UPGRADE_SPEED} 증가시킨다.", BuyableItem[Define.BuyableItem.Speed_Upgrade].cost));
    }

    void LoadStageInfo()
    {
        StageInfo.Add(1, "숲속에서 훈련을 하던 중 성쪽에서 폭발음이 들렸다. 왕국쪽으로 달려갈려는 찰나 숨어있던 마족들이 나와 공격하기 시작하는데..");
        StageInfo.Add(2, "마족들을 해치우고 나니 숲 주변에 있던 마을이 걱정되기 시작하였다. 성으로 가기전 마을에 찾아가 마족들을 해치우자");
        StageInfo.Add(3, "마을을 구한 후 성에 있는 마물들을 해치우기로 마음을 먹는다. 성에 있는 모든 마물들을 해치우자");
    }

    public void Clear()
    {
        
    }
}
