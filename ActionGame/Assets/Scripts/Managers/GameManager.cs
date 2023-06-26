using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager
{
    public PlayerController Player { get { return _player; } }
    PlayerController _player;

    public void SetPlayer(GameObject go)
    {
        PlayerController player = go.GetComponent<PlayerController>();

        if (player == null)
            return;

        _player = player;
    }
}
