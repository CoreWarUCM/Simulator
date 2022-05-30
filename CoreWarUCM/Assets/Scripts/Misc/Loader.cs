using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    public FadeAnim fadeAnim;
    
    void Awake()
    {
        Application.targetFrameRate = -1;
        QualitySettings.vSyncCount = 0;
        DontDestroyOnLoad(this);
        UserConfig.Init();
        if (fadeAnim)
        {
            fadeAnim.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && fadeAnim)
        {
            fadeAnim.gameObject.SetActive(true);
            fadeAnim.FadeToLevel(1);
        }
    }
    
    public static void LoadScene(int levelToLoad)
    {
        SceneManager.LoadScene(levelToLoad);
    }

    private void OnApplicationQuit()
    {
        UserConfig.Close();
    }
}
