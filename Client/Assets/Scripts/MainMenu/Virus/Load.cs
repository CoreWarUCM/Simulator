using UnityEngine;

public class Load : MonoBehaviour
{
    private VirusIO.Virus _virusIO;
    private int _player = -1;
    
    public void LoadWarrior(int player)
    {
        _player = player;
        GameManager.Instance.LoadWarrior(_player);
        _virusIO = GameManager.Instance.GetVirus(_player);
    }

    public void UpdateVirusState(VirusState virus)
    {
        if (!_virusIO.isValidWarrior()) return;

        // Indice del jugador
        virus.SetPlayerIndex(_player);
        // Nombre del virus
        var n = _virusIO.GetName();
        virus.SetName(n);
        // Autor del virus
        var a = _virusIO.GetAuthor();
        virus.SetAuthor(a);
    }

    public VirusIO.Virus GetCurrentVirus()
    {
        return _virusIO;
    }

    public int GetCurrentPlayer()
    {
        return _player;
    }
}