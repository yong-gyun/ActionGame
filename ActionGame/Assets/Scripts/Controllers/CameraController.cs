using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Vector3 _offset = new Vector3(0f, 5.5f, -1f);
    Transform _player;

    private void Start()
    {
        _offset = new Vector3(0f, 5.5f, -1f);
        _player = Managers.Game.GetPlayer.transform;
    }

    private void LateUpdate()
    {
        transform.position = _offset + _player.position;
    }
}
