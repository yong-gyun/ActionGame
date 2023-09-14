using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        GameObject go = Managers.Resource.Instantiate($"Stage/Stage{Managers.Game.CurrentStage}_Map");
        
        PlayerController player = Managers.Object.SpawnPlayer();
        
        Camera.main.gameObject.GetOrAddComponent<CameraController>();
        Managers.UI.ShowSceneUI<UI_Hud>();
        return true;
    }

    private void Update()
    {
        Managers.Game.PlayTime += Time.deltaTime;
    }
}
