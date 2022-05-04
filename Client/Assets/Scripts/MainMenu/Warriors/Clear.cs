using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clear : MonoBehaviour
{
    public GameObject list;

    public void ClearList()
    {
        for (var i = list.transform.childCount - 1; i >= 0; i--)
        {
            var go = list.transform.GetChild(i).gameObject;
            go.GetComponent<VirusState>().Reset();
        }
    }
}
