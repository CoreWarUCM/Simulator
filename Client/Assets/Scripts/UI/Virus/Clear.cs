using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clear : MonoBehaviour
{
    [Tooltip("Lista de virus")] public List<VirusState> virusList;

    private Button _addButton;

    public void ClearList()
    {
        foreach (var virus in virusList)
        {
            virus.Reset();
        }

        GameManager.Instance.ClearVirusList();

        if (!_addButton) return;
        _addButton.interactable = true;
    }

    public void SetList(List<VirusState> newList)
    {
        virusList = newList;
    }

    public void SetAddButton(Button addButton)
    {
        _addButton = addButton;
    }
}