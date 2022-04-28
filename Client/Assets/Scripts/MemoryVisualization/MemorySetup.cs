using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemorySetup : MonoBehaviour
{
    [SerializeField] private MemoryGroup memoryGroup;
    [SerializeField] private Camera _camera;

    [SerializeField] private bool verticalMode = true;

    private float _lastWidth = -1, _lastScreenWidth = -1;
    private float _lasthHeight = -1, _lastScreenHeiht = -1;
    [SerializeField]
    private int cellAmount = 8000;
    
    private float _ratio = 0.8f;

    void Start()
    {
        memoryGroup.SetupMemory(cellAmount, verticalMode, _ratio);
        ResetCamera();
    }

    private void Update()
    {
        Rect r = _camera.rect;

        if ((_lastWidth != r.width || _lasthHeight != r.height) ||
            (_lastScreenWidth != Screen.width || _lastScreenHeiht != Screen.height))
        {
            memoryGroup.RegroupMemory(verticalMode);
            ResetCamera();
        }
    }


    private void ResetCamera()
    {
        _camera.transform.position = memoryGroup.GroupCenter() + Vector3.forward * -900;
        _camera.orthographicSize = (memoryGroup.RenderSize()/2) * 1.05f;
    }


    private void FindPerfectRatio()
    {
        float tolerance = 0.001f;
        while (_ratio <= 0.8f)
        {
            _ratio = 0.5f;

            float fLongSide = (float)Math.Sqrt(cellAmount/_ratio);
            float fShortSide = fLongSide * _ratio;

            if (Math.Abs(Math.Floor(fLongSide) - fLongSide) < tolerance &&
                Math.Abs(Math.Floor(fShortSide) - fShortSide) < tolerance)
                break;

            _ratio += 0.1f;
        }
    }
}