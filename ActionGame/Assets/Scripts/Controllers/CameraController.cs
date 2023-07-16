using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Vector3 _offset = new Vector3(3.75f, 6f, -2f);
    Transform _player;
    
    private void Start()
    {
        _offset = new Vector3(3.75f, 6f, -2f);
        _player = Managers.Game.Player.transform;
    }

    private void LateUpdate()
    {
        transform.position = _offset + _player.position;
    }
}
