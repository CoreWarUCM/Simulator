
using System;
using System.Collections.Generic;
using UnityEngine;

public class VirusManager
{
    private Dictionary<int, Virus> _tournament;

    private VirusPair _versus;

    private bool tournamentMode = false;

    public VirusManager()
    {
        _tournament = new Dictionary<int, Virus>();
        _versus = new VirusPair();
    }

    public void SetVersusVirus(bool first, Virus v)
    {
        Debug.Log("PRINT");
        if (first)
            _versus.A = v;
        else
            _versus.B = v;
    }

    public void SetTournamentVirus(int pos, Virus v)
    {
        _tournament[pos] = v;
    }

    public bool IsVersusReady()
    {
        return _versus.IsValidPair();
    }

    public void SetMode(bool tournament)
    {
        tournamentMode = tournament;
    }

    public VirusPair GetCurrentVersus()
    {
        return tournamentMode ? GetNextBattle() : _versus;
    }

    public VirusPair GetNextBattle()
    {
        return new VirusPair();
    }

    public Virus GetTournamentVirus(int pos)
    {
        return _tournament[pos];
    }

    public Virus GetVersusVirus(bool first)
    {
        return first ? _versus.A : _versus.B;
    }
    
    public void RemoveTournamentVirus(int player)
    {
        _tournament.Remove(player);
    }

    public void ClearVirusList()
    {
        _versus.Clear();
        _tournament.Clear();
    }

    public int GetTournamentCount()
    {
        return _tournament.Count;
    }

    
}
