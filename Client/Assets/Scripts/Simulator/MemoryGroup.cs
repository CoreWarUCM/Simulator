using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryGroup : MonoBehaviour
{
    [SerializeField] private MemoryCell memoryCell;
    private List<MemoryCell> _cells;

    private void Start()
    {
        SetupMemory(10000, 100);
    }


    public void SetupMemory(int size, int columns = 5)
    {
        if (columns <= 0 || !memoryCell || size < 100) 
        {
            Debug.LogWarning("Bad Layout Setup, check for null cell || length <= 0 || size <= 100");
            Destroy(gameObject);
            return;
        }

        _cells = new List<MemoryCell>();
        float mSizeX = memoryCell.GetComponent<Renderer>().bounds.size.x;
        float mSizeY = memoryCell.GetComponent<Renderer>().bounds.size.y;
        Vector3 startPos = transform.position;
        for (int i = 0; i < size; i++)
        {
            Vector3 pos = new Vector3(
                 startPos.x + (i % columns) * (mSizeX * 1.1f),
                 startPos.y + (int)(i / columns) * (mSizeY * 1.1f),
                 startPos.z);

            MemoryCell mCell = Instantiate(memoryCell.gameObject, pos, Quaternion.identity, transform)
                .GetComponent<MemoryCell>();
            mCell.SetupCell(i);
            _cells.Add(mCell);
        }
    }

    public MemoryCell GetCell(int index)
    {
        return _cells[index];
    }
}
