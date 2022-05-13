using System;
using System.Collections;
using System.Collections.Generic;
using ComputeShaderUtility;
using UnityEngine;
using UnityEngine.UI;

public class MemoryGroupShader : MonoBehaviour
{
    [SerializeField] private ComputeShader computeShader;
    [SerializeField] private int cellWidth = 50;
    [SerializeField] private bool grid = false;

    private ComputeBuffer _cellsBuffer;

    private RenderTexture _texture;

    private int _width, _height;

    private Color[] _cells;
    
    public void Init(int numCells, bool verticalMode, Vector2Int sides)
    {
        _width = 50 * sides.x;
        _height = 50 * sides.y;

        ComputeHelper.CreateRenderTexture(ref _texture, _width, _height);

        computeShader.SetTexture(0, "Texture", _texture);
        _texture.name = "ShaderTexture";
        GetComponent<RawImage>().texture = _texture;

        _cells = new Color[numCells];

        Color c = grid ? Color.white : Color.black;
        
        for (int i = 0; i < numCells; i++)
        {
            _cells[i] = c;
        }
       
        ComputeHelper.CreateAndSetBuffer<Color>(ref _cellsBuffer, _cells, computeShader, "cells");
        computeShader.SetInt("numColls",sides.x);
        computeShader.SetInt("width",_width);
        computeShader.SetInt("height",_height);
        computeShader.SetInt("cellWidth",cellWidth);
        computeShader.SetBool("grid",grid);
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
}