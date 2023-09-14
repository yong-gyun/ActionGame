using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    float _rotSpeed = 360f;

    void Start()
    {
        Managers.Object.Player.ShieldItem = gameObject;
    }

    void Update()
    {
        transform.Rotate(Vector3.up * _rotSpeed * Time.deltaTime);
    }
}
