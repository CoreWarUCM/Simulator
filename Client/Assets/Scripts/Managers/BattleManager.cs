using System;
using System.Collections;
using System.Collections.Generic;
using Simulator;
using UnityEngine;

/// <summary>
/// Assistant manager to the GameManager that stays in
/// the GameScene. Stores reference to the UIManager and
/// the BattleSimulator. When the GameManager loads the GameScene
/// it delegates to this class the initialization of the virus that are going
/// to fight.
/// </summary>
public class BattleManager : MonoBehaviour
{
    // Reference to the BattleSimulator in the scene.
    [SerializeField] private BattleSimulator battleSimulator;

    // Reference to the BattleSimulator in the scene.
    [SerializeField] private UIManager UIManager;

    private bool activeBattle;

    /// <summary>
    /// Called from the GameManager. It passes the necesary information
    /// about the 2 virus to the UIManager (to show it in the interface) and the
    /// BattleSimulator(to load and prepare the logic)
    /// </summary>
    /// <param name="pair">Struct with the 2 virus that are going to fight</param>
    public void Init(VirusPair pair)
    {
        Virus A = pair.A;
        Virus B = pair.B;
        List<string> virusAData = new List<string>();
        List<string> virusBData = new List<string>();
        Parser.LoadVirus(A.GetPath(), B.GetPath(),
            out virusAData, out virusBData);

        battleSimulator.LoadVirus(virusAData, virusBData);

        UIManager.SetVirusA(A.GetName(), A.GetAuthor());
        UIManager.SetVirusB(B.GetName(), B.GetAuthor());
    }

    private void Start()
    {
        battleSimulator.Subscribe(MessageType.Death, message =>
        {
            VirusWinner((message.virus - 2) * -1);
        } );

    }


    /// <summary>
    /// Just, you know, starts the battle
    /// </summary>
    public void StartBattle()
    {
        activeBattle = true;
        battleSimulator.StartBattle();
    }

    public void VirusWinner(int virus)
    {
        if(!activeBattle)
            return;
        NewWinner(virus);
    }

    public void NewWinner(int virus)
    {
        activeBattle = false;
        UIManager.ShowResults(virus);
    }
}
