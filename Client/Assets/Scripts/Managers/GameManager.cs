using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Core of the program.
/// Works as a central point that connects all the managers and
/// gives smaller classes access to them.
/// Uses a singleton pattern so it can be access from anywhere at any point in the
/// program cycle.
/// The singleton is initialized at the beginning of the program and exist till the end.
/// </summary>
public class GameManager : MonoBehaviour
{
    // Instance
    public static GameManager Instance;

    // Manager that controls file IO
    private VirusIO _virusIO;
    
    private VirusManager _virusManager;

    [SerializeField] private UIManager uiManager;

    [SerializeField] private BattleManager battleManager;


    /// <summary>
    /// When a scene is loaded the GameManager instance from that scene
    /// calls this awake. If the instance of the class in not initialized it
    /// assigns itself as the class instance and initializes the needed parameters.
    /// If class instance already exist it transfers the reference of the objects in scene
    /// to the current instance.
    /// </summary>
    private void Awake()
    {
        if (Instance)
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

            if (battleManager) //We are in launching in simulation, lets select some warriors by default
            {
                string PATH = Application.streamingAssetsPath + "/SampleWarriors/";
                Debug.Log("EPA QUE VOY: " + PATH);
                _virusManager.SetVersusVirus(true, new Virus(PATH + "imp.red", "Debug", "Dev", null));
                _virusManager.SetVersusVirus(false, new Virus(PATH + "inversedwarf.red", "Debug2", "Dev2", null));
                _virusManager.SetMode(false);
                SetupBattle();
            }

            DontDestroyOnLoad(this.gameObject);
        }
    }

    /// <summary>
    /// Calls the VirusIO coroutine to open a file browser and load a virus.
    /// Callbacks and data are passed as parameter needed to reflect the virus loaded.
    /// (Read VirusIO for more info)
    /// We need this call here because LoadVirus in VirusIO works asynchronously and needs a coroutine to work
    /// Because VirusIO does not extend MonoBehaviour it can't call Coroutines so the GameManager does it
    /// </summary>
    /// <param name="player">Player index</param>
    /// <param name="state"> VirusState(Interface Representation)</param>
    /// <param name="callback">Data Representation Callback</param>
    /// <param name="extraCallBack">Virus Data Callback</param>
    public void LoadVirus(int player, VirusState state = null, Action<int, VirusState, Virus> callback = null,
        Action<Virus> extraCallBack = null)
    {
        if (_virusIO.dialogOpen)
            return;
        StartCoroutine(_virusIO.LoadVirus(player, state, callback, extraCallBack));
    }

    /// <summary>
    /// Calls the VirusIO coroutine to open a file browser and save a virus.
    /// (Read VirusIO for more info)
    /// We need this call here because SaveVirus in VirusIO works asynchronously and needs a coroutine to work
    /// Because VirusIO does not extend MonoBehaviour it can't call Coroutines so the GameManager does it
    /// </summary>
    /// <param name="rawData">data to store in file</param>
    public void SaveVirus(string rawData, byte[] sprite)
    {
        if (_virusIO.dialogOpen)
            return;
        StartCoroutine(_virusIO.SaveVirus(rawData, sprite));
    }

    /// <summary>
    /// Calls the auxiliary manager to setup the next battle
    /// </summary>
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