using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    Transform _root;

    public Transform Root
    {
        get
        {
            if (_root == null)
                _root = new GameObject("@UI_Root").transform;

            return _root;
        }
    }

    public UI_Scene SceneUI;
    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();
    int _order = 10;

    public void SetCanvas(GameObject go, bool sort)
    {
        if (go == null)
            return;

        Canvas canvas = go.GetOrAddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if(sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }

    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}", Root);
        T popup = go.GetOrAddComponent<T>();
        _popupStack.Push(popup);
        _order++;

        return popup;
    }

    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Scene/{name}", Root);
        T sceneUI = go.GetOrAddComponent<T>();
        SceneUI = sceneUI;
        return sceneUI;
    }

    public void ClosePopupUI()
    {
        if (_popupStack.Count == 0)
            return;

        UI_Popup popup = _popupStack.Pop();
        Managers.Resource.Destory(popup.gameObject);
        _order--;
    }

    public void ClosePopupUI(UI_Popup popup)
    {
        if (_popupStack.Count == 0)
            return;

        if (popup == _popupStack.Peek())
            ClosePopupUI();
    }

    public void CloseAllPopupUI()
    {
        while (_popupStack.Count > 0)
            ClosePopupUI();
    }

    public void Clear()
    {
        CloseAllPopupUI();
        SceneUI = null;
        _order = 10;
    }
}
