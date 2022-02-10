using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clear : MonoBehaviour
{
    public GameObject list;

    public void ClearList()
    {
        // Feisimo pero me sirve para hacer un ejemplo de prototipo xD
        for (int i = list.transform.childCount - 1; i >= 0; i--)
        {
            GameObject.Destroy(list.transform.GetChild(i));
        }
    }
}
