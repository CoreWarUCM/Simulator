using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class EditorManager : MonoBehaviour
{
    [SerializeField] 
    private TMP_InputField _editorField;

    [SerializeField] 
    private EditorIntellisense _intellisense;

    [SerializeField] private float editTime = 0.5f;

    [SerializeField]
    private bool _checkedEdit = false;
    [SerializeField]
    private float _lastEdit = 0;
    
    private string _lastText = "";
    
    
    void Start()
    {
        if (!_editorField || !_intellisense)
        {
            Debug.LogWarning("EditorManager no initialized");
            Destroy(this);
            return;
        }
        FullTextCheck();
        _lastEdit = Time.time;
        StartCoroutine(CheckChanges());
    }

    public void OnTextEdited()
    {
        _lastEdit = Time.time;
        _checkedEdit = false;
    }

    IEnumerator CheckChanges()
    {
        while (true)
        {
            yield return new WaitForSeconds(editTime);
            if(Time.time - _lastEdit >= editTime && !_checkedEdit)
                FullTextCheck();
        }
    }

    void FullTextCheck()
    {
        _checkedEdit = true;
        string text = _editorField.text;

        if(_lastText == text)
            return;
        
        _intellisense.CheckStyles(ref text, _lastText);

        _lastText = text;
        _editorField.text = text;
    }

    public void SaveWarrior()
    {
        string tagText = _editorField.text;
        string outText = Regex.Replace(tagText, "<.*?>", string.Empty);
        GameManager.instance.SaveWarrior(outText);
    }
}
