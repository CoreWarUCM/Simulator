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
    private int _cellAmount = 0;

    void Start()
    {
        _cellAmount = 8000;

        memoryGroup.SetupMemory(_cellAmount, verticalMode);

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
}