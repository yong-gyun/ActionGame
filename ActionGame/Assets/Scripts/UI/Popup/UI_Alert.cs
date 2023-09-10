using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Alert : UI_Popup
{
    enum Texts
    {
        AlertText
    }

    protected override void Init()
    {
        base.Init();
        BindText(typeof(Texts));
    }

    public void OnAlert(string message)
    {
        GetText((int) Texts.AlertText).text = message;
        StartCoroutine(CoClose());
    }

    IEnumerator CoClose()
    {
        yield return new WaitForSeconds(0.75f);
        Managers.UI.CloseEffectPopupUI(gameObject);
    }
}
