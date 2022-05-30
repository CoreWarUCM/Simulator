using System;
using System.Collections;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the whole functionality of the
/// in game editor, is connected with the editor.
/// Now is mainly unused because the intellisense is
/// deactivated right now.
/// It also manages the load and save functionalities.
/// </summary>
public class EditorManager : MonoBehaviour
{
    // Reference to input field in scene
    [SerializeField] private TMP_InputField _editorField;
    [SerializeField] private RawImage image;
    [SerializeField] private Texture defaultImage;

    // Support class for intellisense UNUSED
    private EditorIntellisense _intellisense;

    // UNUSED Edit time cycle UNUSED
    [SerializeField] private float editTime = 0.5f;
    
    // UNUSED Last edit time UNUSED
    private float _lastEdit = 0;

    // UNUSED Boolean to check if text has already been checked, prevents extra calls UNUSED
    private bool _checkedEdit = false;
    
    // UNUSED Last text store after last edit check UNUSED
    private string _lastText = "";
    
    // Boolean to activate intellisense DO NOT USE 
    [SerializeField] private bool intellisense = false;

    private byte[] _sprite = null;

    private void Awake()
    {
        _intellisense = new EditorIntellisense();
    }

    /// <summary>
    /// Checks if is initialized correctly 
    /// </summary>
    void Start()
    {
        if (!_editorField || _intellisense == null)
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

    /// <summary>
    /// Callback from editor when text changes
    /// </summary>
    public void OnTextEdited()
    {
        _lastEdit = Time.time;
        _checkedEdit = false;
    }

    /// <summary>
    /// Coroutine that constantly checks for next changes
    /// </summary>
    /// <returns></returns>
    IEnumerator CheckChanges()
    {
        while (true)
        {
            yield return new WaitForSeconds(editTime);
            if (Time.time - _lastEdit >= editTime && !_checkedEdit)
                FullTextCheck();
        }
    }

    /// <summary>
    /// UNUSED
    /// Calls support class to modify
    /// the text to use tags and check for errors
    /// UNUSED
    /// </summary>
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

    /// <summary>
    /// Cleans the text of tags and calls IO class to save the virus
    /// </summary>
    public void SaveVirus()
    {
        string outText = _editorField.text;
        if (intellisense)
             outText = Regex.Replace(outText, "<.*?>", string.Empty);
        
        GameManager.Instance.SaveVirus(outText, _sprite);
    }

    /// <summary>
    /// Callback when loading a virus from file so it can be modified from editor
    /// </summary>
    /// <param name="v">Virus with data to load in the text</param>
    private void LoadCallback(Virus v)
    {
        _editorField.verticalScrollbar.value = 0;
        _editorField.text = string.Join("\n", v.GetRawData());

        if (v.GetImageData() != null)
            LoadImageCallBack(v.GetImageData());
        else
        {
            _sprite = null;
            image.texture = defaultImage;
        }

    }

    private void LoadImageCallBack(byte[] sprite)
    {
        _sprite = sprite;
        Texture2D text2D = new Texture2D(2, 2);
        if (text2D.LoadImage(sprite))
        {
            Sprite spr = Sprite.Create(text2D, new Rect(0, 0, text2D.width, text2D.height),new Vector2(0,0), 100);

            image.texture = spr.texture;
        }
    }

    /// <summary>
    /// Calls the coroutine to load virus from file to the editor
    /// </summary>
    public void LoadToEdit()
    {
        StartCoroutine(GameManager.Instance.GetVirusIO().LoadVirus(-1,null,null,LoadCallback));
    }

    public void LoadImage()
    {
        StartCoroutine(GameManager.Instance.GetVirusIO().LoadVirusImage(LoadImageCallBack));
    }
}