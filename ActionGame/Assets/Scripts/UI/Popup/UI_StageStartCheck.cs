using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_StageStartCheck : UI_Popup
{
    enum RawImages
    {
        StageImage
    }

    enum Buttons
    {
        StartButton,
        ExitButton
    }

    enum Texts
    {
        Info
    }

    protected override void Init()
    {
        base.Init();

        BindButton(typeof(Buttons));
        Bind<RawImage>(typeof(RawImages));
        BindText(typeof(Texts));

        string info = Managers.Data.StageInfo[Managers.Game.CurrentStage];
        Get<RawImage>((int)RawImages.StageImage).texture = Managers.Resource.Load<Texture>($"Textures/UI_Stage{Managers.Game.CurrentStage}_RendererTexture");
        GetText((int)Texts.Info).text = info;
        GetButton((int)Buttons.StartButton).onClick.AddListener(() => { Managers.Scene.Load(Define.SceneType.Game); });
        GetButton((int)Buttons.ExitButton).onClick.AddListener(() => { ClosePopupUI(); });
    }
}
