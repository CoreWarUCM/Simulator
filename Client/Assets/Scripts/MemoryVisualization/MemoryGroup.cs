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

    [SerializeField] private float ratio = 0.7f;

    private void Awake()
    {
        _groupShaderR = _groupShader.GetComponent<Renderer>();
    }

    public void Start()
    {
        BattleSimulator bs = GetComponent<BattleSimulator>();
        bs.Subscribe((int)Simulator.MessageType.BlockModify, (BaseMessage bm) =>
        {
            SetColor(((BlockModifyMessage)bm).modifiedLcoation, bm.warrior == 1 ? Color.red : Color.blue);
        });
    }
    
    public void SetupMemory(int size, bool verticalMode)
    {
        if (size < 100)
        {
            Debug.LogWarning("Bad Layout Setup, check for null cell || length <= 0 || size <= 100");
            Destroy(gameObject);
            return;
        }

        _groupShader.Init(size,verticalMode,ratio);
        RegroupMemory(verticalMode);
    }


    public void RegroupMemory( bool verticalMode)
    {
        float hRatio = 1, vRatio = 1;

        if (verticalMode)
            hRatio = ratio;
        else
            vRatio = ratio;
        _groupShader.transform.localScale = new Vector3(100*hRatio, 1, 100*vRatio);
    }

    public void SetColor(int index, Color color)
    {
        
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
