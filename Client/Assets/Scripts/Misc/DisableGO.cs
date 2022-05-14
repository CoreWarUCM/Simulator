using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableGO : MonoBehaviour
{
    public GameObject objToDisable;

    public void Disable()
    {
        objToDisable.SetActive(false);
    }

    public void DestroyGO()
    {
        Destroy(objToDisable);
    }
}
