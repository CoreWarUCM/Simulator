using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Load : MonoBehaviour
{
    [Header("Atributos para Normal1v1")]
    [SerializeField] private InputField name;
    [SerializeField] private InputField author;
    
    [Header("Atributos para Torneo")]
    [SerializeField] private GameObject warriorPrefab;
    [SerializeField] private Remove removeButton;
    [SerializeField] private GameObject list;

    private WarriorIO.Warrior warr;
    public void LoadWarrior(int player)
    {
        GameManager.instance.LoadWarrior(player);
        warr = GameManager.instance.GetWarrior(player);
    }

    public void ApplyTextWarrior()
    {
        name.text = warr.GetName();
        author.text = warr.GetAuthor();
    }

    /// <summary>
    /// Para el torneo. Añade un nuevo guerrero a la lista
    /// </summary>
    public void AddToList()
    {
        var player = list.transform.childCount;
        LoadWarrior(player);
        if (!warr.isValidWarrior()) return;

        var go = GameObject.Instantiate(warriorPrefab.transform, list.transform);
        // TODO: Esto feo, pero solo quiero ver que funcione. Aunque tampoco sé de otra manera xDDD
        name = go.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<InputField>();
        name.text = warr.GetName();
        author = go.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<InputField>();
        author.text = warr.GetAuthor();
        var button = go.GetComponent<Button>();
        button.onClick.AddListener(() => removeButton.ChangeWarrior(go.gameObject));
    }
}