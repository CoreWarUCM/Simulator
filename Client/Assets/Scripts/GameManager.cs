using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private WarriorIO _warriorIO;

    private Dictionary<int, WarriorIO.Warrior> _warriors;

    private void Awake()
    {
        if (instance)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            _warriorIO = new WarriorIO();
            _warriors = new Dictionary<int, WarriorIO.Warrior>();
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
}