using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrivateMemoryManager
{
    int[] _firstVirusSpace;
    int[] _secondVirusSpace;
    int privateSize;

    public PrivateMemoryManager(int coreSize = 8000)
    {
        privateSize = (coreSize / 16);

        _firstVirusSpace = new int[privateSize];
        _secondVirusSpace = new int[privateSize];

        _firstVirusSpace[0] = -1;
        _secondVirusSpace[0] = -1;
    }

    public int getPSpace(int location, int warrior)
    {
        if (warrior == 1)
            return _firstVirusSpace[location % privateSize];
        else if (warrior == 2)
            return _secondVirusSpace[location % privateSize];
        else 
            throw new System.Exception("Unsupported virus identificator");
    }
    public void setPSpace(int location, int warrior, int value)
    {
        if (warrior == 1)
            _firstVirusSpace[location % privateSize] = value;
        else if (warrior == 2)
            _secondVirusSpace[location % privateSize] = value;
        else
            throw new System.Exception("Unsupported virus identificator");
    }
}
