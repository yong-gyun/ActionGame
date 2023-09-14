using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Menu : UI_Scene
{
    enum Buttons
    {
        Stage1Button,
        Stage2Button,
        Stage3Button,
    }

    protected override void Init()
    {
        base.Init();
        BindButton(typeof(Buttons));

        for (int i = 0; i < 3; i++)
        {
            if (Managers.Game.UnlockStageCount <= i)
                GetButton(i).interactable = false;
        }

        GetButton((int)Buttons.Stage1Button).onClick.AddListener(() => { OnSelected(1); });
        GetButton((int)Buttons.Stage2Button).onClick.AddListener(() => { OnSelected(2); });
        GetButton((int)Buttons.Stage3Button).onClick.AddListener(() => { OnSelected(3); });
    }

    void OnSelected(int idx)
    {
        Managers.Game.CurrentStage = idx;
        Managers.UI.ShowPopupUI<UI_StageStartCheck>();
    }
}