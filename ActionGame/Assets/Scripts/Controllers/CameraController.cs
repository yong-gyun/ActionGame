using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector3 _offset = new Vector3(4.5f, 6f, 0);
    GameObject _player;

    private void Start()
    {
        _player = Managers.Game.Player.gameObject;
    }

    private void LateUpdate()
    {
        transform.position = _offset + _player.transform.position;
    }

}
