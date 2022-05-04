using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class VirusState : MonoBehaviour
{
    [Tooltip("Nombre del virus")] [SerializeField]
    private InputField inputName;

    [Tooltip("Nombre del autor")] [SerializeField]
    private InputField inputAuthor;

    [Tooltip("Referencia al componente Button")] [SerializeField]
    private Button button;

    [Tooltip("Referencia al componente Image")] [SerializeField]
    private Image buttonSprite;

    public void SetName(string newName)
    {
        inputName.text = newName;
    }

    public void SetAuthor(string newAuthor)
    {
        inputAuthor.text = newAuthor;
    }

    public void AddListenerButton(UnityAction cb)
    {
        button.onClick.AddListener(cb);
        
        if (!button.interactable)
        {
            button.interactable = true;
        }
    }
    
    public void SetButtonSprite(Sprite newSprite)
    {
        button.image.sprite = newSprite;
    }

    public void Reset()
    {
        inputName.text = "NULL";
        inputAuthor.text = "NULL";
        button.onClick.RemoveAllListeners();
        button.image.sprite = button.spriteState.disabledSprite;
        button.onClick.RemoveAllListeners();
        button.interactable = false;
    }

    public bool IsVirusActive()
    {
        return button.interactable;
    }
}