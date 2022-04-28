using System;
using System.Collections;
using System.Collections.Generic;
using ComputeShaderUtility;
using UnityEngine;

public class MemoryGroupShader : MonoBehaviour
{
    [SerializeField] private ComputeShader computeShader;

    private ComputeBuffer _cellsBuffer;

    private RenderTexture _texture;

    private int _width, _heigth;

    public void Init(int numCells, bool verticalMode, float ratio)
    {
        float hRatio = 1, vRatio = 1;

        if (verticalMode)
            hRatio = ratio;
        else
            vRatio = ratio;


        _width = (int)(5000 * hRatio);
        _heigth = (int)(5000 * vRatio);

        ComputeHelper.CreateRenderTexture(ref _texture, _width, _heigth);
        computeShader.SetTexture(0, "Texture", _texture);
        GetComponent<MeshRenderer>().material.mainTexture = _texture;

        Cell[] cells = new Cell[numCells];

        int cellWidth = 0;
        
        for (int i = 0; i < numCells; i++)
        {
            cells[i] = new Cell(i, cellWidth);
        }

        ComputeHelper.CreateAndSetBuffer<Cell>(ref _cellsBuffer, cells, computeShader, "cells", 0);
        computeShader.SetInt("numCells",numCells);
        computeShader.SetInt("width",_width);
        computeShader.SetInt("height",_heigth);
    }


    public void FixedUpdate()
    {
        computeShader.SetFloat("time", Time.time);
        ComputeHelper.Dispatch(computeShader, _width, _heigth);
    }


    struct Cell
    {
        public int index;
        public int width;

        public Cell(int index, int width)
        {
            this.index = index;
            this.width = width;
        }
    }
}