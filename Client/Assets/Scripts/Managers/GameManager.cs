using System;
using System.Collections;
using System.Collections.Generic;
using Simulator;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private VirusIO _virusIO;

    public Dictionary<int, VirusIO.Virus> _virus;

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
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            _virusIO = new VirusIO();
            _virusIO.Init();

            _virus = new Dictionary<int, VirusIO.Virus>();
            if (battleManager) //We are in launching in simulation, lets select some warriors by default
            {
                string PATH = Application.dataPath + "/SampleWarriors/";
                Debug.Log("EPA QUE VOY: " + PATH);
                _virus[0] = new VirusIO.Virus(PATH + "imp.redcode", "Debug", "Dev", null, true);
                _virus[1] = new VirusIO.Virus(PATH + "inversedwarf.redcode", "Debug2", "Dev2", null, true);
                SetupBattle();
            }

            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void LoadVirus(int player, VirusState state = null, Action<int, VirusState, VirusIO.Virus> callback = null,
        Action extraCallBack = null)
    {
        if (_virusIO.dialogOpen)
            return;
        StartCoroutine(_virusIO.LoadWarrior(player, state, callback, extraCallBack));
    }


    public void SetVirus(int player, VirusIO.Virus v)
    {
        _virus[player] = v;
    }

    public VirusIO.Virus GetVirus(int player)
    {
        return _virus[player];
    }

    public void SaveVirus(string rawData)
    {
        if (_virusIO.dialogOpen)
            return;
        StartCoroutine(_virusIO.SaveVirus(rawData));
    }

    public void RemoveVirus(int player)
    {
        _virus.Remove(player);
    }

    private void SetupBattle()
    {
        battleManager.Init(_virus[0],_virus[1]);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(2);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void ClearVirusList()
    {
        _virus.Clear();
    }

    public int GetVirusListCount()
    {
        return _virus.Count;
    }

    public UIManager getUIManager()
    {
        return uiManager;
    }
}