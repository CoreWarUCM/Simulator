using System.Collections;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class EditorManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField _editorField;

    [SerializeField] private EditorIntellisense _intellisense;

    [SerializeField] private float editTime = 0.5f;

    [SerializeField] private bool _checkedEdit = false, intellisense = false;
    [SerializeField] private float _lastEdit = 0;

    private string _lastText = "";

    void Start()
    {
        if (!_editorField || !_intellisense)
        {
            Debug.LogWarning("EditorManager no initialized");
            Destroy(this);
            return;
        }

        _lastEdit = Time.time;
        if (intellisense)
        {
            FullTextCheck();
            StartCoroutine(CheckChanges());
        }
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
            if (Time.time - _lastEdit >= editTime && !_checkedEdit)
                FullTextCheck();
        }
    }

    void FullTextCheck()
    {
        _checkedEdit = true;
        string text = _editorField.text;

        if (_lastText == text)
            return;

        _intellisense.CheckStyles(ref text, _lastText);

        _lastText = text;
        _editorField.text = text;
    }

    public void SaveWarrior()
    {
        string outText = _editorField.text;
        if (intellisense)
             outText = Regex.Replace(outText, "<.*?>", string.Empty);
        
        GameManager.Instance.SaveVirus(outText);
    }

    private void LoadCallback(Virus v)
    {
        _editorField.text = string.Join("\n", v.GetRawData());
    }

    public void LoadToEdit()
    {
        StartCoroutine(GameManager.Instance.GetVirusIO().LoadVirus(-1,null,null,LoadCallback));
    }
}