using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    GameObject _player;

    public GameObject Player 
    {
        get
        {
            if (_player == null)
                _player = GameObject.Find("Player");
            return _player;
        }
    }
}
