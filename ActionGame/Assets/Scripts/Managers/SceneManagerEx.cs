using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    public void Load(Define.Scene type)
    {
        SceneManager.LoadScene(GetSceneName(type));
    }

    string GetSceneName(Define.Scene type)
    {
        string sceneName = System.Enum.GetName(typeof(Define.Scene), type);
        char[] chars = sceneName.ToCharArray();
        chars[0].ToString().ToUpper();
        return new string(chars);
    }
}
