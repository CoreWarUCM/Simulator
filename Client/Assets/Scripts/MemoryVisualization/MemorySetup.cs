using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemorySetup : MonoBehaviour
{
    [SerializeField] private MemoryGroup _memoryGroup;
    [SerializeField] private Camera _camera;

    private float _lastWidth = -1;
    private float _lasthHeight = -1;
    private int _cellAmount = 0;
    
    void Start()
    {

        _cellAmount = 8000;

        _memoryGroup.SetupMemory(_cellAmount, GetColumns());
        
        ResetCamera();
    }

    private void Update()
    {
        Rect r = _camera.rect;
        if (_lastWidth != r.width || _lasthHeight != r.height)
        {
            _memoryGroup.RegroupMemory(GetColumns());
            ResetCamera();
        }
    }


    private void ResetCamera()
    {
        _camera.transform.position = _memoryGroup.GroupCenter() + Vector3.forward * -10;
        _camera.orthographicSize = (_memoryGroup.VerticalSize() / 2) * 1.05f;
    }


    private int GetColumns()
    {
        Rect r = _camera.rect;
        _lastWidth = r.width;
        _lasthHeight = r.height;
        float rWidth = Screen.width * r.width;
        float rHeight = Screen.height * r.height;
        float ratio = rWidth / rHeight;

        
        return (int)Math.Sqrt(_cellAmount * ratio);

    }
}
