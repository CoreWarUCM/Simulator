using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TournamentManager : MonoBehaviour
{
    [SerializeField] private List<VirusState> virusList;
    [SerializeField] private Load load;
    [SerializeField] private Remove remove;
    [SerializeField] private Clear clear;
    [SerializeField] private Button configButton;
    [SerializeField] private Button addButton;

    private VirusState _selectedVirus;
    
    private void Start()
    {
        clear.SetList(virusList);
        clear.SetAddButton(addButton);
        remove.InitRemove(addButton, virusList.Count);
    }

    /// <summary>
    /// Añade un nuevo virus a la lista del torneo
    /// </summary>
    public void AddToList()
    {
        // 1. Busqueda del primer hueco vacio que haya.
        int player = 0;
        while (virusList[player].IsVirusActive())
        {
            player++;
        }

        load.LoadWarrior(player);
        var virusIO = load.GetCurrentVirus();
        if (!virusIO.isValidWarrior()) return;

        // Si el hueco encontrado es del ultimo jugador, entonces significa que ya no hay mas huecos
        if (player == virusList.Count - 1)
        {
            addButton.interactable = false;
        }

        var numPlayers = GameManager.Instance.GetVirusListCount();
        if (!configButton.interactable && numPlayers >= 2)
        {
            configButton.interactable = true;
        }

        var virus = virusList[player];
        load.UpdateVirusState(virus);
        virus.ActivateButton();
    }

    public void SetSelectedVirus(VirusState selected)
    {
        _selectedVirus = selected;
        remove.ChangeVirus(_selectedVirus);
    }
}