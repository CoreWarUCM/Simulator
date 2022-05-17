using UnityEngine;
using UnityEngine.UI;

public class Remove : MonoBehaviour
{
    private VirusState _virus;
    private Button _addButton;
    private int _numPlayers = 0;
    
    public void ChangeVirus(VirusState newW)
    {
        _virus = newW;
    }

    public void RemoveVirus(Button configButton)
    {
        if (!_virus) return;

        var i = _virus.GetPlayerIndex();
        VirusManager vM = GameManager.Instance.GetVirusManager();
        vM.RemoveTournamentVirus(i);
        _virus.Reset();
        _virus = null;
        var total = vM.GetTournamentCount();
        if (total < 2)
        {
            configButton.interactable = false;
        }
        else if (!_addButton.interactable && total < _numPlayers)
        {
            _addButton.interactable = true;
        }
    }

    public void InitRemove(Button addButton, int numPlayers)
    {
        _addButton = addButton;
        _numPlayers = numPlayers;
    }
}
