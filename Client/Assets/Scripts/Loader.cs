using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this);
        UserConfig.Init();
        SceneManager.LoadScene(1);
    }

    private void OnApplicationQuit()
    {
        UserConfig.Close();
    }
}
