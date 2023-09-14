using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Hud : UI_Scene
{
    enum Images
    {
        HPBar,
        MPBar,
        SkillOneCooltimeImage,
        SkillTwoCooltimeImage
    }

    enum Texts
    {
        ScoreText,
        TimeText,
        HpHealPotionCountText,
        MpHealPotionCountText,
        SkillOneCooltimeText,
        SkillTwoCooltimeText,
        SkillOneMp,
        SkillTwoMp
    }

    PlayerController _player;

    protected override void Init()
    {
        base.Init();
        _player = Managers.Object.Player;
        BindImage(typeof(Images));
        BindText(typeof(Texts));
    }

    private void Update()
    {
        GetImage((int)Images.HPBar).fillAmount = Mathf.Lerp(GetImage((int)Images.HPBar).fillAmount, _player.Hp / _player.MaxHp, Time.deltaTime * 5f);
        GetImage((int)Images.MPBar).fillAmount = Mathf.Lerp(GetImage((int)Images.MPBar).fillAmount, _player.Mp / _player.MaxMp, Time.deltaTime * 5f);
        GetText((int)Texts.ScoreText).text = $"{Managers.Game.CurrentScore}";
        GetText((int)Texts.TimeText).text = $"{string.Format("{0:D2} : {1:D2}", (int) Managers.Game.PlayTime / 60, (int)Managers.Game.PlayTime % 60)}";

        GetText((int)Texts.HpHealPotionCountText).text = $"{Managers.Game.CurrentPotionsCount[(int) Define.Potion.Hp_Potion]}";
        GetText((int)Texts.MpHealPotionCountText).text = $"{Managers.Game.CurrentPotionsCount[(int)Define.Potion.Mp_Potion]}";

        GetText((int)Texts.SkillOneMp).text = $"{_player.SkillOne.UsedMp}";
        GetText((int)Texts.SkillTwoMp).text = $"{_player.SkillTwo.UsedMp}";

        GetImage((int)Images.SkillOneCooltimeImage).fillAmount = _player.SkillOne.CurrentCooltime / _player.SkillOne.Cooltime;
        GetImage((int)Images.SkillTwoCooltimeImage).fillAmount = _player.SkillTwo.CurrentCooltime / _player.SkillTwo.Cooltime;

        if (_player.SkillOne.CurrentCooltime <= 0)
            GetText((int)Texts.SkillOneCooltimeText).text = "";
        else
            GetText((int)Texts.SkillOneCooltimeText).text = $"{string.Format("{0:0}", _player.SkillOne.CurrentCooltime)}";

        if (_player.SkillTwo.CurrentCooltime <= 0)
            GetText((int)Texts.SkillTwoCooltimeText).text = "";
        else
            GetText((int)Texts.SkillTwoCooltimeText).text = $"{string.Format("{0:0}", _player.SkillTwo.CurrentCooltime)}";
    }
}
