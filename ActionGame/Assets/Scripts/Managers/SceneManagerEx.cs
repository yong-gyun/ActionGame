using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    public BaseScene CurrentScene { get { return UnityEngine.Object.FindObjectOfType<BaseScene>(); } }
    
    public void Load(Define.SceneType type)
    {
        SceneManager.LoadScene(GetSceneName(type));
    }

    public AsyncOperation LoadAsync(Define.SceneType type)
    {
        Load(Define.SceneType.Loading);
        return SceneManager.LoadSceneAsync(GetSceneName(type));
    }

    string GetSceneName(Define.SceneType type)
    {
        string name = Enum.GetName(typeof(Define.SceneType), type);

        char[] chars = name.ToCharArray();
        char.ToUpper(chars[0]);
        return new string(chars);
    }
}
