using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    Transform _root;

    public UI_Scene SceneUI { get; private set; }

    public Transform Root
    {
        get
        {
            if(_root == null)
                _root = new GameObject("@UI_Root").transform;

            return _root;
        }
    }

    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();
    int _order = 10;

    public void SetCanvas(GameObject go, bool sort)
    {
        if (go == null)
            return;

        Canvas canvas = go.GetOrAddComponent<Canvas>();
        canvas.overrideSorting = true;
        
        if(sort)
        {
            _order++;
            canvas.sortingOrder = _order;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }

    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        if(string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}");

        if (go == null)
            return null;

        T popup = go.GetOrAddComponent<T>();
        popup.transform.SetParent(Root);
        _popupStack.Push(popup);
        popup.transform.SetParent(Root);
        return popup;
    }

    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if(string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Scene/{name}");

        if (go == null)
            return null;

        T sceneUI = go.GetOrAddComponent<T>();
        sceneUI.transform.SetParent(Root);
        SceneUI = sceneUI;
        sceneUI.transform.SetParent(Root);

        return sceneUI;
    }

    public T MakeSubitemUI<T>(Transform parent, string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Subitem/{name}");
        T subitem = go.GetOrAddComponent<T>();
        subitem.transform.SetParent(parent);
        return subitem;
    }

    public void ClosePopupUI()
    {
        if (_popupStack.Count == 0)
            return;

        UI_Popup popup = _popupStack.Pop();
        Managers.Resource.Destory(popup.gameObject);
        _order--;
    }

    public void CloseAllPopupUI()
    {
        while(_popupStack.Count > 0)
            ClosePopupUI();
    }
}