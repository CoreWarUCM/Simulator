using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemorySetup : MonoBehaviour
{
    [SerializeField] private MemoryGroup memoryGroup;
    [SerializeField] private Camera _camera;

    [SerializeField] private bool verticalMode = true;
    
    [SerializeField]
    private int cellAmount = 8000;
    
    [SerializeField]
    private float _ratio = 0.8f;

    void Start()
    {
        memoryGroup.SetupMemory(cellAmount, verticalMode, _ratio);
    }
}