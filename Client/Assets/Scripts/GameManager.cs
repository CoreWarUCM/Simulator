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

    public void SaveWarrior(string[] rawData)
    {
        // _warriorIO.SaveWarrior(new []{";redcode",";author pepepopo",";name pruebapito", "DAT 0", "MOV 0", "DAT 0"});
        _warriorIO.SaveWarrior(rawData);
    }
}
