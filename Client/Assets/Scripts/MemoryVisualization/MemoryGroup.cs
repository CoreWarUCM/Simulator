using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Simulator;
using TMPro;

public class MemoryGroup : MonoBehaviour
{
    [SerializeField]
    private MemoryGroupShader _groupShader;

    [SerializeField]
    private Color _warrior1Color;

    [SerializeField]
    private Color _warrior2Color;

    [SerializeField]
    private Color _warrior1ExecuteColor;

    [SerializeField]
    private Color _warrior2ExecuteColor;

    private Renderer _groupShaderR;

    private float _ratio;

    [SerializeField]
    private TextMeshProUGUI _virus1Text;
    [SerializeField]
    private TextMeshProUGUI _virus2Text;
    [SerializeField]
    private TextMeshProUGUI _virus1Author;
    [SerializeField]
    private TextMeshProUGUI _virus2Author;
    
    private void Awake()
    {
        _groupShaderR = _groupShader.GetComponent<Renderer>();
    }

    public void SetupMemory(int size, bool verticalMode, float ratio)
    {
        if (size < 100)
        {
            Debug.LogWarning("Bad Layout Setup, check for null cell || length <= 0 || size <= 100");
            Destroy(gameObject);
            return;
        }

        _ratio = ratio;

        int x = (int)Math.Ceiling(Math.Sqrt(size / ratio));
        int y = (int)(x * ratio);
        
        _groupShader.Init(size,verticalMode,new Vector2Int(x,y));
        RegroupMemory(verticalMode);
        
        BattleSimulator bs = GetComponent<BattleSimulator>();
        bs.Subscribe(Simulator.MessageType.BlockModify, (BaseMessage bm) =>
        {
            SetColor(((BlockModifyMessage)bm).modifiedLcoation, (bm.warrior == 1 ? _warrior1Color : _warrior2Color));
        });

        bs.Subscribe(Simulator.MessageType.BlockExecuted, (BaseMessage bm) =>
        {
            SetColor(((BlockExecutedMessage)bm).modifiedLcoation, (bm.warrior == 1 ? _warrior1ExecuteColor : _warrior2ExecuteColor));
        });

        _virus1Author.color = _warrior1ExecuteColor;
        _virus1Text.color = _warrior1Color;
        
        _virus2Author.color = _warrior2ExecuteColor;
        _virus2Text.color = _warrior2ExecuteColor;

        _virus1Author.text = GameManager.Instance._virus[0].GetAuthor();
        _virus1Text.text = GameManager.Instance._virus[0].GetName();
        
        _virus2Author.text = GameManager.Instance._virus[1].GetAuthor();
        _virus2Text.text = GameManager.Instance._virus[1].GetName();
    }


    public void RegroupMemory( bool verticalMode)
    {
        float hRatio = 1, vRatio = 1;

        if (verticalMode)
            hRatio = _ratio;
        else
            vRatio = _ratio;
        _groupShader.transform.localScale = new Vector3(100*hRatio, 1, 100*vRatio);
    }

    public void SetColor(int index, Color color)
    {
        _groupShader.SetColor(index,color);
    }

    public Vector3 GroupCenter()
    {
        return _groupShader.transform.position;
    }

    public float RenderSize()
    {
        return _groupShaderR.bounds.size.y;
    }
}
