using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakeFadeIn : MonoBehaviour
{
    public GameObject fadeIn;
    
    private void Awake()
    {
        fadeIn.SetActive(true);
    }
}
