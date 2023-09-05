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
}