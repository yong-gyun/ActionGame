using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public PlayerController GetPlayer 
    { 
        get 
        {
            if (_player == null)
                _player = GameObject.Find("Player").GetComponent<PlayerController>();

            return _player; 
        } 
    }

    PlayerController _player;
    public int CurrentGold { get; set; } = 10000;
    public int CurerntScore { get; set; }
    public float PlayTime { get; set; }

    public int[] CurrentPositionsCount = new int[2];
    public int[] PlayerStatUpgradeCount = new int[4];
}