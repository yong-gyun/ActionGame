using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Hud : UI_Scene
{
    enum Images
    {
        HPBar,
        MPBar
    }

    enum Texts
    {
        ScoreText,
        TimeText
    }

    PlayerController _player;

    protected override void Init()
    {
        base.Init();
        _player = Managers.Game.GetPlayer;
        BindImage(typeof(Images));
        BindText(typeof(Texts));
    }

    private void Update()
    {
        GetImage((int)Images.HPBar).fillAmount = Mathf.Lerp(GetImage((int)Images.HPBar).fillAmount, _player.Hp / _player.MaxHp, Time.deltaTime * 5f);
        GetImage((int)Images.MPBar).fillAmount = Mathf.Lerp(GetImage((int)Images.MPBar).fillAmount, _player.Mp / _player.MaxMp, Time.deltaTime * 5f);
        GetText((int)Texts.ScoreText).text = $"{Managers.Game.CurerntScore}";
        GetText((int)Texts.TimeText).text = $"{string.Format("{0:D2} : {1:D2}", (int) Managers.Game.PlayTime / 60, (int)Managers.Game.PlayTime % 60)}";
    }
}
