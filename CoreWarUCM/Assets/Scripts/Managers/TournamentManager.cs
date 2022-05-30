using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TournamentManager : MonoBehaviour
{
    [SerializeField] private List<VirusState> virusList;
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

        var state = virusList[player];
        Load.LoadVirus(player, state, (Virus v) =>
        {
            // Si el hueco encontrado es del ultimo jugador, entonces significa que ya no hay mas huecos
            if (player == virusList.Count - 1)
            {
                addButton.interactable = false;
            }
            VirusManager vM = GameManager.Instance.GetVirusManager();
            vM.SetTournamentVirus(player, v);
            var numPlayers = vM.GetTournamentCount();
            if (!configButton.interactable && numPlayers >= 2)
            {
                configButton.interactable = true;
            }

            state.ActivateButton();
        });
    }

    public void SetSelectedVirus(VirusState selected)
    {
        _selectedVirus = selected;
        remove.ChangeVirus(_selectedVirus);
    }
}