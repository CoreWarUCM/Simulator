
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manager storing all the loaded virus for easy access and manage.
/// Separates the virus from the 1v1 and the tournament and have a pointer
/// to the current fight to prevent crossing data.
/// The tournamnet part is UNFINISHED and only loads the virus but do not advance or manage the tournament draft
/// </summary>
public class VirusManager
{
    // Tournament virus 
    private Dictionary<int, Virus> _tournament;

    // 1V1 Virus
    private VirusPair _versus;

    // Check for mode (true : tournament - false : 1V1)
    private bool tournamentMode = false;
    
    public VirusManager()
    {
        _tournament = new Dictionary<int, Virus>();
        _versus = new VirusPair();
    }

    public void SetVersusVirus(bool first, Virus v)
    {
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

    // NOT WORKING AT THE MOMENT, ALWAYS RETURN THE FIRST 2 VIRUS
    public VirusPair GetNextBattle()
    {
        return new VirusPair(_tournament[0], _tournament[1]);
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
