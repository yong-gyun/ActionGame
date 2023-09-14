using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Loading : UI_Scene
{
    enum Texts
    {
        LoadingText
    }

    enum Scrollbars
    {
        LoadingBar
    }

    string _loadingText = "Loading...";

    protected override void Init()
    {
        base.Init();
    
    }

    IEnumerator CoLoading()
    {
        AsyncOperation op = Managers.Scene.LoadAsync(Define.SceneType.Game);
        op.allowSceneActivation = false;

        while(!op.isDone)
        {
            yield return null;
        }

    }
}
