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

    private Dictionary<int, VirusIO.Virus> _virus;

    [SerializeField]
    private BattleSimulator simulator;

    private List<string> _warrior1Data;
    private List<string> _warrior2Data;

    private void Awake()
    {
        if (Instance)
        {
            //Pass simulator for initialization
            if (simulator != null && Instance.simulator == null)
                Instance.simulator = simulator;
            if (Instance._virus?.Count > 0)
            {
                Debug.Log("Initializing BattleSimulator");
                foreach (KeyValuePair<int, VirusIO.Virus> pair in Instance._virus)
                {
                    Debug.Log($"{pair.Key}:{pair.Value.GetPath()}");
                }
                Instance._warrior1Data = new List<string>();
                Instance._warrior2Data = new List<string>();
                Parser.LoadWarriors(Instance._virus[0].GetPath(),Instance._virus[1].GetPath(),
                    out Instance._warrior1Data, out Instance._warrior2Data);

                Instance.simulator.LoadWarriors(Instance._warrior1Data, Instance._warrior2Data);
            }
            Destroy(this);
            
        }
        else
        {
            Instance = this;
            _virusIO = new VirusIO();
            _virus = new Dictionary<int, VirusIO.Virus>();
            DontDestroyOnLoad(this);
        }
    }

    public void LoadWarrior(int player)
    {
        VirusIO.Virus w = _virusIO.LoadWarrior();
        _virus[player] = w;

#if UNITY_EDITOR
        w.DebugInfo();
        w.DebugRawData();
#endif
    }

    private void Update()
    {
        // TODO: Borrar
        int a = 0;
    }

    public VirusIO.Virus GetVirus(int player)
    {
        return _virus[player];
    }

    public void SaveWarrior(string rawData)
    {
        _virusIO.SaveWarrior(rawData);
    }

    public void RemoveWarrior(int player)
    {
        _virus.Remove(player);
    }

    public void StartGame()
    {
        //Comprobar si podemos empezar partida, pero me da pereza pensar ahora
        if (true)
        {
            SceneManager.LoadScene("Base");
        }
    }

    public void ClearVirusList()
    {
        _virus.Clear();
    }

    public int GetVirusListCount()
    {
        return _virus.Count;
    }
}