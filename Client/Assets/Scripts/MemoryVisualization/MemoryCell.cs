using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCell : MonoBehaviour
{
    private int _cellPos = -1;

    private Renderer _renderer;

    public void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void SetupCell(int pos)
    {
        _cellPos = 0;
        _renderer.material.color = Color.white;
    }

    public void SetColor(Color col)
    {
        _renderer.material.color = col;
    }
}
