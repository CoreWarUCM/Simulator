using System;
using UnityEngine;

public static class Load 
{

    public static void LoadVirus(int player, VirusState state, Action extraCallBack = null)
    {
        GameManager.Instance.LoadVirus(player, state, UpdateVirusState, extraCallBack);
    }
    
    public static void UpdateVirusState(int player,VirusState state, VirusIO.Virus v)
    {
        if (!v.isValidWarrior() || !state) return;
        // Indice del jugador
        state.SetPlayerIndex(player);
        // Nombre del virus
        var n = v.GetName();
        state.SetName(n);
        // Autor del virus
        var a = v.GetAuthor();
        state.SetAuthor(a);
    }
}