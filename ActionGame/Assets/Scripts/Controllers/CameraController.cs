using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Vector3 _offset ;
    Transform _player;

    private void Start()
    {
        _player = Managers.Game.GetPlayer.transform;
    }

    private void LateUpdate()
    {
        transform.position = _offset + _player.position;
    }
}
