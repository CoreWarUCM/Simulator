using System;
using System.Collections;
using System.Collections.Generic;
using ComputeShaderUtility;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class that manages the creation and communication with the shader that draws the memory
/// </summary>
public class MemoryGroupShader : MonoBehaviour
{
    // Shadeer
    [SerializeField] private ComputeShader computeShader;
    
    // This boolean set the shader drawing mode (better to keep it on)
    [SerializeField] private bool grid = true;

    // Reference to canvas in scene to check size in screen
    [SerializeField] private Canvas canvas;

    // Component in scene that the shader will draw in 
    [SerializeField] private RawImage image;

    // Number of target columns to draw
    [SerializeField] private uint numCols = 80;
    
    private uint numRows;
   
    private RectTransform _transform;

    private ComputeBuffer _cellsBuffer;

    private RenderTexture _texture;
    
    private int _width;
    private int _height;

    private Color[] _cells;
    
    /// <summary>
    /// Initialization of the class and shader.
    /// Calls auxiliary method to get proportions, then checks them.
    /// Initializes the shader and the cell buffer.
    /// </summary>
    /// <param name="numCells"></param>
    public void Init(uint numCells)
    {
        Vector4 proportions = GetPixelProportion(numCells);

        _width = (int)proportions.x;
        _height = (int)proportions.y;

        if (proportions.z * numCols != _width)
            Debug.LogError("Error width. Operation: " + proportions.z + " * " + numCols + ". Expected: " + _width + " --- Got: " + proportions.z * numCols);

        if (proportions.w * numRows != _height)
            Debug.LogError("Error height. Operation: " + proportions.w + " * " + numRows + ". Expected: " + _height + " --- Got: " + proportions.w * numRows);

        ComputeHelper.CreateRenderTexture(ref _texture, _width, _height);
        
        computeShader.SetTexture(0, "Texture", _texture);
        _texture.name = "ShaderTexture";
        image.texture = _texture;
        
        _cells = new Color[numCells];

        for (int i = 0; i < numCells; i++)
        {
            _cells[i] = Color.black;
        }
        
        ComputeHelper.CreateAndSetBuffer<Color>(ref _cellsBuffer, _cells, computeShader, "cells");
        computeShader.SetInt("numColls",(int)numCols);
        computeShader.SetInt("numRows",(int)numRows);
        computeShader.SetInt("numCells",(int)numCells);
        computeShader.SetInt("width",_width);
        computeShader.SetInt("height",_height);
        computeShader.SetInt("cellWidth",(int)proportions.z);
        computeShader.SetInt("cellHeight",(int)proportions.w);
        computeShader.SetBool("grid",grid);
    }

    /// <summary>
    /// Returns a Vector 4 containing the data needed for the texture in
    /// function of the size and screeCap of the UI element
    /// </summary>
    /// <param name="size"></param>
    /// <returns>Vector4(pixelWidth, pixelHeigth, cellWidth, cellHeigth)</returns>
    Vector4 GetPixelProportion(uint numCells)
    {
        _transform = image.rectTransform;
        
        Vector2 anchor = _transform.anchorMax - _transform.anchorMin;

        RectTransform t = _transform.parent.GetComponent<RectTransform>();

        while (t.gameObject != canvas.gameObject)
        {
             Vector2 a = t.anchorMax - t.anchorMin;
             anchor = Vector2.Scale(anchor, a); 

             t = t.parent.GetComponent<RectTransform>();
        }

        Vector2 screen = new Vector2(Screen.width, Screen.height);

        Vector2 textureSize = new Vector2(anchor.x * screen.x, anchor.y * screen.y) * 5;

        numRows =(numCells - 1) / numCols + 1;

        uint cellWidth = (uint)(textureSize.x  / numCols);
        uint cellHeight = (uint)(textureSize.y / numRows);

        textureSize = new Vector2(cellWidth * numCols, cellHeight * numRows);

        return new Vector4(textureSize.x, textureSize.y, cellWidth, cellHeight);
    }
    
    /// <summary>
    /// Calls the compute shader
    /// </summary>
    public void Update()
    {
        ComputeHelper.Dispatch(computeShader, _width, _height);
    }


    /// <summary>
    /// Update a cell color and sends it to the buffer
    /// </summary>
    /// <param name="index">cell index</param>
    /// <param name="color">new color</param>
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