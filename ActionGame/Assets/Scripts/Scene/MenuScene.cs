using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScene : BaseScene
{
    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        Managers.UI.ShowSceneUI<UI_Menu>();
        return true;
    }
}
