using UnityEngine;
using UnityEngine.UI;

public class Load : MonoBehaviour
{
    [Header("Atributos para Normal1v1")] [SerializeField]
    private InputField inputName;

    [SerializeField] private InputField inputAuthor;

    [Header("Atributos para Torneo")] [SerializeField]
    private Remove removeButton;

    [SerializeField] private VirusState[] virusList;

    [Tooltip("Sprite que corresponde a un botón habilitado")] [SerializeField]
    private Sprite enableButton;

    private VirusIO.Virus virusIO;

    public void LoadWarrior(int player)
    {
        GameManager.instance.LoadWarrior(player);
        virusIO = GameManager.instance.GetWarrior(player);
    }

    public void ApplyTextWarrior()
    {
        inputName.text = virusIO.GetName();
        inputAuthor.text = virusIO.GetAuthor();
    }

    /// <summary>
    /// Añade un nuevo virus a la lista del torneo
    /// </summary>
    public void AddToList()
    {
        // 1. Busqueda del primer hueco vacio que haya.
        int player = 0;
        while (player < virusList.Length && virusList[player].IsVirusActive())
        {
            player++;
        }
        // TODO: Se podria poner un pequeño panel que avise de que ya no hay huecos disponibles para añadir más virus
        // o simplemente se podría añadir un efecto de parpaedo en el botón de añadir indicando que no se puede usar
        if (player == virusList.Length) return;
        
        LoadWarrior(player);
        if (!virusIO.isValidWarrior()) return;

        var virus = virusList[player];
        var n = virusIO.GetName();
        virus.SetName(n);
        var a = virusIO.GetAuthor();
        virus.SetAuthor(a);
        void Ua() => removeButton.ChangeVirus(virus);
        virus.AddListenerButton(Ua);
        virus.SetButtonSprite(enableButton);
    }
}