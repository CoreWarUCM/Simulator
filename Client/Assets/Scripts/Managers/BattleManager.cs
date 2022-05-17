using System.Collections;
using System.Collections.Generic;
using Simulator;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private VirusIO.Virus _virusA, _virusB;

    [SerializeField] private BattleSimulator battleSimulator;
    [SerializeField] private UIManager UIManager;

    public void Init(VirusIO.Virus A, VirusIO.Virus B)
    {
        _virusA = A;
        _virusB = B;
        List<string> virusAData = new List<string>();
        List<string> virusBData = new List<string>();
        Parser.LoadWarriors(A.GetPath(), B.GetPath(),
            out virusAData, out virusBData);

        battleSimulator.LoadWarriors(virusAData, virusBData);

        UIManager.SetVirusA(A.GetName(), A.GetAuthor());
        UIManager.SetVirusB(B.GetName(), B.GetAuthor());
    }


    public void StartBattle()
    {
        battleSimulator.StartBattle();
    }
}
