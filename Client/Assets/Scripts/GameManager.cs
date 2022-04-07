using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private WarriorLoader _loader;

    private Dictionary<int, WarriorLoader.Warrior> _warriors;

    private void Awake()
    {
        if (instance)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            _loader = new WarriorLoader();
            _warriors = new Dictionary<int, WarriorLoader.Warrior>();
        }
    }

    public void LoadWarrior(int player)
    {
        WarriorLoader.Warrior w = _loader.LoadWarrior();
        _warriors[player] = w;

#if UNITY_EDITOR
        w.debugInfo();
#endif
    }
}
