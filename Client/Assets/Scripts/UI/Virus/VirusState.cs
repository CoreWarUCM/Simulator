using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class VirusState : MonoBehaviour
{
    [Tooltip("Nombre del virus")] [SerializeField]
    private InputField inputName;

    [Tooltip("Nombre del autor")] [SerializeField]
    private InputField inputAuthor;

    [Header("Solo para torneo")] [Tooltip("Referencia al componente Button")] [SerializeField]
    private Button button;

    private int _playerIndex = -1;

    public void SetName(string newName)
    {
        inputName.text = newName;
    }

    public void SetAuthor(string newAuthor)
    {
        inputAuthor.text = newAuthor;
    }

    public void ActivateButton()
    {
        button.interactable = true;
    }

    public void SetPlayerIndex(int player)
    {
        _playerIndex = player;
    }

    public void Reset()
    {
        _playerIndex = -1;
        inputName.text = "NULL";
        inputAuthor.text = "NULL";

        if (!button) return;
        button.interactable = false;
    }

    public bool IsVirusActive()
    {
        return button.interactable;
    }

    public int GetPlayerIndex()
    {
        return _playerIndex;
    }
}