using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstelaGenerator : MonoBehaviour
{
    public enum DIR
    {
        TOP = 0,
        RIGHT = 1,
        BOT = 2,
        LEFT = 3
    };

    [SerializeField] private int numStars = 5;
    [SerializeField] private Color[] colors;
    [SerializeField] private GameObject starPrefab;
    [SerializeField] private Transform[] corners;
    [SerializeField] private DIR initDir;
    [Tooltip("Distancia entre cada estrella")]
    [SerializeField] private float distStars = 5;
    private void Start()
    {
        if (colors.Length == 0)
        {
            colors = new Color[1];
            colors[0] = Color.red;
        }

        Vector3 pos = transform.position;
        float alpha = 1.0f;
        float offset = distStars;
        bool horizontal = false;
        switch (initDir)
        {
            case DIR.TOP:
                offset = -distStars;
                break;
            case DIR.RIGHT:
                offset = -distStars;
                horizontal = true;
                break;
            case DIR.BOT:
                offset = distStars;
                break;
            case DIR.LEFT:
                offset = distStars;
                horizontal = true;
                break;
        }
        
        for (int i = 0; i < numStars; i++)
        {
            var newStar = Instantiate(starPrefab, pos, Quaternion.identity, transform);
            newStar.GetComponent<EstelaMovement>().Init(alpha, corners, colors);
            // Si movimiento inicial es horizontal, entonces hay que colocar en x
            if (horizontal) pos.x += offset;
            else pos.y += offset;
            
            alpha -= 1.0f / numStars;
        }
    }
}
