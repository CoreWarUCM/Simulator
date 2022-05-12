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

    [SerializeField]
    private BattleSimulator simulator;

    private List<string> _warrior1Data;
    private List<string> _warrior2Data;

    private void SetUpWarriors(VirusIO.Virus first, VirusIO.Virus second)
    {
        Instance._warrior1Data = new List<string>();
        Instance._warrior2Data = new List<string>();
        Parser.LoadWarriors(first.GetPath(),second.GetPath(),
            out Instance._warrior1Data, out Instance._warrior2Data);

        Instance.simulator.LoadWarriors(Instance._warrior1Data, Instance._warrior2Data);
    }
    private void Awake()
    {
        if (GameManager.Instance)
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
                SetUpWarriors(Instance._virus[0], Instance._virus[1]);
            }
            else
            {
                Debug.LogError("This should not be possible; starting battle scene without warriors :C");
            }
            Destroy(this);
            
        }
        else
        {
            Instance = this;
            _virusIO = new VirusIO();
            _virus = new Dictionary<int, VirusIO.Virus>();
            if (simulator) //We are in launching in simulation, lets select some warriors by default
            {
                string PATH = Application.dataPath+"/SampleWarriors/";
                Debug.Log("EPA QUE VOY: " + PATH);
                _virus[0] = new VirusIO.Virus(PATH+"imp.redcode","Debug","Dev",null,true);
                _virus[1] = new VirusIO.Virus(PATH+"inversedwarf.redcode","Debug2","Dev2",null,true);
                SetUpWarriors(_virus[0], _virus[1]);
            }
            DontDestroyOnLoad(this.gameObject);
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