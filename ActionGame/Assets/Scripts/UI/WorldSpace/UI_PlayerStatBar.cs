using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PlayerStatBar : UI_Base
{
    enum Scrollbars
    {
        HPBar,
        MPBar
    }

    PlayerController _player;
    Collider _col;

    protected override void Init()
    {
        _player = Managers.Game.GetPlayer;
        _col = _player.GetComponent<Collider>();
        BindScrollbar(typeof(Scrollbars));
    }

    private void Update()
    {
        transform.position = _player.transform.position + Vector3.up * _col.bounds.size.y;
        transform.rotation = Camera.main.transform.rotation;

        GetScrollbar((int)Scrollbars.HPBar).size = Mathf.Lerp(GetScrollbar((int)Scrollbars.HPBar).size, _player.Hp / _player.MaxHp, Time.deltaTime * 5f);
        GetScrollbar((int)Scrollbars.MPBar).size = Mathf.Lerp(GetScrollbar((int)Scrollbars.MPBar).size, _player.Mp / _player.MaxMp, Time.deltaTime * 5f);
    }
}
