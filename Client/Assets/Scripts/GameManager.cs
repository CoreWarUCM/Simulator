using System;
using System.Collections;
using System.Collections.Generic;
using Simulator;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private VirusIO _virusIO;

    private Dictionary<int, VirusIO.Virus> _warriors;

    [SerializeField]
    private BattleSimulator _simulator;

    private void Awake()
    {
        if (instance)
        {
            //Pass simulator for initialization
            if (_simulator != null && instance._simulator == null)
                instance._simulator = _simulator;
            if (instance._warriors?.Count > 0)
            {
                Debug.Log("Initializing BattleSimulator");
                foreach (KeyValuePair<int, VirusIO.Virus> pair in instance._warriors)
                {
                    Debug.Log($"{pair.Key}:{pair.Value.GetPath()}");
                }
                Parser.Start(instance._warriors[0].GetPath(),instance._warriors[1].GetPath());
            }
            Destroy(this);
            
        }
        else
        {
            instance = this;
            _virusIO = new VirusIO();
            _warriors = new Dictionary<int, VirusIO.Virus>();
            DontDestroyOnLoad(this);
        }
    }

    public void LoadWarrior(int player)
    {
        VirusIO.Virus w = _virusIO.LoadWarrior();
        _warriors[player] = w;

#if UNITY_EDITOR
        w.DebugInfo();
        w.DebugRawData();
#endif
    }

    private void Update()
    {
        int a = 0;
    }

    public VirusIO.Virus GetWarrior(int player)
    {
        return _warriors[player];
    }

    public void SaveWarrior(string rawData)
    {
        _virusIO.SaveWarrior(rawData);
    }

    public void StartGame()
    {
        //Comprobar si podemos empezar partida, pero me da pereza pensar ahora
        if (true)
        {
            SceneManager.LoadScene("Base");
        }
    }
}