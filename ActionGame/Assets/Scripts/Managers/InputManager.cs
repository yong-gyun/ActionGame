using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{
    Action<Define.MouseEvent> _mouseAction = null;
    Action _keyAction = null;

    public void AddMouseEvent(Action<Define.MouseEvent> evt) { _mouseAction += evt; }
    public void AddKeyEvent(Action evt) { _keyAction += evt; }
    bool _pressed = false;
    float _pressedTime = 0;

    public void OnUpdate()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (_keyAction != null)
            _keyAction.Invoke();

        if(_mouseAction != null)
        {
            if(Input.GetMouseButton(0))
            {
                if(!_pressed)
                    _mouseAction.Invoke(Define.MouseEvent.PointerDown);

                _pressed = true;
                _pressedTime = Time.time;
            }
            else
            {
                if(_pressed)
                {
                    if (Time.time < _pressedTime + 0.2f)
                    {
                        _mouseAction.Invoke(Define.MouseEvent.Click);
                    }

                    _mouseAction.Invoke(Define.MouseEvent.PointerUp);
                }

                _pressed = false;
                _pressedTime = 0;
            }
        }
    }

    public void Clear()
    {
        _keyAction = null;
        _mouseAction = null;
    }
}