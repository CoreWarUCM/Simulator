using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private VirusIO _virusIO;

    private VirusManager _virusManager;

    [SerializeField] private UIManager uiManager;

    [SerializeField] private BattleManager battleManager;


    private void Awake()
    {
        if (GameManager.Instance)
        {
            //Pass simulator for initialization
            if (battleManager)
            {
                Instance.battleManager = battleManager;
                Instance.SetupBattle();
            }
            if (uiManager)
                Instance.uiManager = uiManager;
            Destroy(this);
        }
        else
        {
            Instance = this;
            _virusManager = new VirusManager();
            _virusIO = new VirusIO();
            _virusIO.Init();

            if (battleManager) //We are in launching in simulation, lets select some warriors by default
            {
                string PATH = Application.dataPath + "/SampleWarriors/";
                Debug.Log("EPA QUE VOY: " + PATH);
                _virusManager.SetVersusVirus(true, new Virus(PATH + "imp.redcode", "Debug", "Dev", null, true));
                _virusManager.SetVersusVirus(false, new Virus(PATH + "inversedwarf.redcode", "Debug2", "Dev2", null, true));
                _virusManager.SetMode(false);
                SetupBattle();
            }

            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void LoadVirus(int player, VirusState state = null, Action<int, VirusState, Virus> callback = null,
        Action<Virus> extraCallBack = null)
    {
        if (_virusIO.dialogOpen)
            return;
        StartCoroutine(_virusIO.LoadVirus(player, state, callback, extraCallBack));
    }

    public void SaveVirus(string rawData)
    {
        if (_virusIO.dialogOpen)
            return;
        StartCoroutine(_virusIO.SaveVirus(rawData));
    }

    private void SetupBattle()
    {
        battleManager.Init(_virusManager.GetCurrentVersus());
    }

    public void StartGame()
    {
        SceneManager.LoadScene(2);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(1);
    }

    public UIManager GetUIManager()
    {
        return uiManager;
    }

    public VirusManager GetVirusManager()
    {
        return _virusManager;
    }

    public VirusIO GetVirusIO()
    {
        return _virusIO;
    }

    public void CloseApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }
}