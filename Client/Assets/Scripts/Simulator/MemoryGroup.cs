using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryGroup : MonoBehaviour
{
    [SerializeField] private MemoryCell memoryCell;
    private List<MemoryCell> _cells;
    private Vector3 _groupCenter;

    // private void Update()
    // {
    //     GetCell(UnityEngine.Random.Range(0,_cells.Count)).SetColor(Color.red);
    // }

    public void SetupMemory(int size, int columns = 5)
    {
        if (columns <= 0 || !memoryCell || size < 100) 
        {
            Debug.LogWarning("Bad Layout Setup, check for null cell || length <= 0 || size <= 100");
            Destroy(gameObject);
            return;
        }
        _groupCenter = Vector3.zero;
        _cells = new List<MemoryCell>();
        float mSizeX = memoryCell.GetComponent<Renderer>().bounds.size.x;
        float mSizeY = memoryCell.GetComponent<Renderer>().bounds.size.y;
        Vector3 startPos = transform.position;
        for (int i = 0; i < size; i++)
        {
            Vector3 pos = new Vector3(
                 startPos.x + (i % columns) * (mSizeX * 1.1f),
                 startPos.y - (int)(i / columns) * (mSizeY * 1.1f),
                 startPos.z);

            MemoryCell mCell = Instantiate(memoryCell.gameObject, pos, Quaternion.identity, transform)
                .GetComponent<MemoryCell>();
            mCell.SetupCell(i);
            _cells.Add(mCell);
            _groupCenter += pos;
        }

        _groupCenter /= _cells.Count;
        VerticalSize();
    }


    public void RegroupMemory(int columns = 5)
    {
        _groupCenter = Vector3.zero;
        float mSizeX = memoryCell.GetComponent<Renderer>().bounds.size.x;
        float mSizeY = memoryCell.GetComponent<Renderer>().bounds.size.y;
        Vector3 startPos = transform.position;
        for (int i = 0; i < _cells.Count; i++)
        {
            Vector3 pos = new Vector3(
                startPos.x + (i % columns) * (mSizeX * 1.1f),
                startPos.y - (int)(i / columns) * (mSizeY * 1.1f),
                startPos.z);

            _cells[i].transform.position = pos;
            _groupCenter += pos;
        }

        _groupCenter /= _cells.Count;
    }
    
    public MemoryCell GetCell(int index)
    {
        return _cells[index];
    }

    public Vector3 GroupCenter()
    {
        return _groupCenter;
    }

    public float VerticalSize()
    {
        MemoryCell first = _cells[0];
        MemoryCell last = _cells[_cells.Count - 1];

        float offset = first.GetComponent<Renderer>().bounds.size.y;
        
        return first.transform.position.y - last.transform.position.y + offset;
    }
}
