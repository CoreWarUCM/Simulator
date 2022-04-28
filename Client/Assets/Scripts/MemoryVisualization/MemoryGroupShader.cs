using System;
using System.Collections;
using System.Collections.Generic;
using ComputeShaderUtility;
using UnityEngine;

public class MemoryGroupShader : MonoBehaviour
{
    [SerializeField] private ComputeShader computeShader;
    [SerializeField] private int cellWidth = 50;

    private ComputeBuffer _cellsBuffer;

    private RenderTexture _texture;

    private int _width, _height;

    private Color[] _cells;
    
    public void Init(int numCells, bool verticalMode, Vector2Int sides)
    {
        float hRatio = 1, vRatio = 1;
        
        _width = 50 * sides.x;
        _height = 50 * sides.y;

        ComputeHelper.CreateRenderTexture(ref _texture, _width, _height);
        computeShader.SetTexture(0, "Texture", _texture);
        GetComponent<MeshRenderer>().material.mainTexture = _texture;

        _cells = new Color[numCells];
       
        ComputeHelper.CreateAndSetBuffer<Color>(ref _cellsBuffer, _cells, computeShader, "cells");
        computeShader.SetInt("numColls",sides.x);
        computeShader.SetInt("width",_width);
        computeShader.SetInt("height",_height);
        computeShader.SetInt("cellWidth",cellWidth);
    }


    public void Update()
    {
        computeShader.SetFloat("time", Time.time);
        ComputeHelper.Dispatch(computeShader, _width, _height);
    }


    public void SetColor(int index, Color color)
    {
        _cells[index] = color;
        ComputeHelper.CreateAndSetBuffer<Color>(ref _cellsBuffer, _cells, computeShader, "cells");
    }


    private void OnDestroy()
    {
        _cellsBuffer.Release();
    }


    // struct Cell
    // {
    //     public Vector2Int startPos;
    //     public int width;
    //     public Color color; 
    //
    //     public Cell(Vector2Int startPos, int width, Color color)
    //     {
    //         this.startPos = startPos;
    //         this.width = width;
    //         this.color = color;
    //     }
    // }
}