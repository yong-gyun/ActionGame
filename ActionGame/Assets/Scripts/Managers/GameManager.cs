using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public int CurrentGold { get; set; } = 10000;
    public int CurrentScore { get; set; }
    public int CurrentStage { get; set; }
    public int MaxStage { get; private set; } = 3;
    public int UnlockStageCount { get; set; } = 1;
    public float PlayTime { get; set; }
    
    public int[] CurrentPotionsCount { get; set; } = new int[2];
    public int[] PlayerStatUpgradeCount { get; set; } = new int[4];
}