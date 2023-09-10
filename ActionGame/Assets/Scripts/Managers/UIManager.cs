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

    public T MakeWorldSpaceUI<T>(Transform parent = null, string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/WorldSpace/{name}");
        T worldSpace = go.GetOrAddComponent<T>();

        if(parent != null)
            worldSpace.transform.SetParent(parent);

        Canvas canvas = go.GetOrAddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = Camera.main;
        return worldSpace;
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

    public T ShowEffectPopupUI<T>(string name = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/EffectPopup/{name}");
        T popupUI = go.GetOrAddComponent<T>();
        return popupUI;
    }

    public void CloseEffectPopupUI(GameObject go)
    {
        Managers.Resource.Destory(go);
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

        if(_popupStack.TryPeek(out popup))
            ClosePopupUI();
    }

    public void CloseAllPopupUI()
    {
        while(_popupStack.Count > 0)
            ClosePopupUI();
    }

    public void Clear()
    {
        CloseAllPopupUI();
        SceneUI = null;
    }
}