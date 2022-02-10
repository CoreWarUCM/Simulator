using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quitar : MonoBehaviour
{

    private GameObject warrior;

    public void ChangeWarrior(GameObject newW)
    {
        warrior = newW;
    }

    public void RemoveWarrior()
    {
        if (!warrior) return;
        
        GameObject.Destroy(warrior);
    }
}
