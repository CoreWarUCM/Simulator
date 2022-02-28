using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleAnimation : MonoBehaviour
{
    [SerializeField] private Color initColor;
    [SerializeField] private Color targetColor;
    [SerializeField] private Text text;
    [SerializeField] private float colorRate = 0.1f;
    
    private bool reverse = false;

    void Start()
    {
        text.color = initColor;
        InvokeRepeating(nameof(ColorAnimation), 1.0f, colorRate);
    }

    private void ColorAnimation()
    {
        text.color = Color.Lerp(initColor, targetColor, Mathf.PingPong(Time.time, 1));
    }
}