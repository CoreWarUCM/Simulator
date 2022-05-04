using System;
using System.Collections;
using System.Collections.Generic;
using Simulator;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private WarriorIO _warriorIO;

    private Dictionary<int, WarriorIO.Warrior> _warriors;

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
                foreach (KeyValuePair<int, WarriorIO.Warrior> pair in instance._warriors)
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
            _warriorIO = new WarriorIO();
            _warriors = new Dictionary<int, WarriorIO.Warrior>();
            DontDestroyOnLoad(this);
        }
    }

    public void LoadWarrior(int player)
    {
        WarriorIO.Warrior w = _warriorIO.LoadWarrior();
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

    public WarriorIO.Warrior GetWarrior(int player)
    {
        return _warriors[player];
    }

    public void SaveWarrior(string rawData)
    {
        _warriorIO.SaveWarrior(rawData);
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