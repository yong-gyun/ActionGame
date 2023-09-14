using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Vector3 _offset = new Vector3(0f, 6f, -4f);
    Transform _player;

    private void Start()
    {
        _player = Managers.Object.Player.transform;
    }

    private void LateUpdate()
    {
        if(_player == null)
            return;

        transform.position = _offset + _player.position;
    }
}
