using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class UI_Base : MonoBehaviour
{
    protected Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    private void Awake()
    {
        Init();
    }

    protected abstract void Init();
    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type);
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        _objects.Add(typeof(T), objects);

        for (int i = 0; i < names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
                objects[i] = gameObject.FindChild(names[i]);
            else
                objects[i] = gameObject.FindChild<T>(names[i]);

            if (objects[i] == null)
                Debug.Log($"Bind Faild {names[i]}");
        }
    }

    protected void BindText(Type type) { Bind<TMP_Text>(type); }
    protected void BindImage(Type type) { Bind<Image>(type); }
    protected void BindSlider(Type type) { Bind<Slider>(type); }
    protected void BindButton(Type type) { Bind<Button>(type); }
    protected void BindObject(Type type) { Bind<GameObject>(type); }
    protected void BindInputField(Type type) { Bind<InputField>(type); }
    
    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;

        if(_objects.TryGetValue(typeof(T), out objects) == false)
        {
            Debug.Log($"Faild Get UI {typeof(T)}");
            return null;
        }

        return objects[idx] as T;
    }

    protected TMP_Text GetText(int idx) { return Get<TMP_Text>(idx); }
    protected Image GetImage(int idx) { return Get<Image>(idx); }
    protected Slider GetSlider(int idx) { return Get<Slider>(idx); }
    protected Button GetButton(int idx) { return Get<Button>(idx); }
    protected GameObject GetObject(int idx) { return Get<GameObject>(idx); }
    protected InputField GetInputField(int idx) { return Get<InputField>(idx); }
}