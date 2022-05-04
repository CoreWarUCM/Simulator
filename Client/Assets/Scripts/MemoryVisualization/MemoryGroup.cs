using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Simulator;

public class MemoryGroup : MonoBehaviour
{
    [SerializeField]
    private MemoryGroupShader _groupShader;

    private Renderer _groupShaderR;

    private float _ratio;

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
        bs.Subscribe((int)Simulator.MessageType.BlockModify, (BaseMessage bm) =>
        {
            SetColor(((BlockModifyMessage)bm).modifiedLcoation, bm.warrior == 1 ? Color.red : Color.blue);
        });
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
