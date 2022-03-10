using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EstelaMovement : MonoBehaviour
{
    [SerializeField] private Image starImage;
    [SerializeField] private float speed = 200.0F;
    [SerializeField] private float beginTime = 0.0f;

    // Para invocar a ChangePath
    private bool changed = false;
    // Index del array de esquinas
    private int indCorner = 0;
    // Index del array de colores
    private int indColor = 0;
    // Momento en el que empieza el movimiento
    private float startTime;
    // Distancia del camino
    private float journeyLength;
    // Valor alpha del color del objeto
    private float alpha = 1.0f;
    // Punto inicial del movimiento
    private Vector3 startMarker;
    // Punto final del movimiento
    private Vector3 endMarker;
    // Array de esquinas
    private Transform [] corners;
    // Color inicial del objeto
    private Color startColor;
    // Array de transición de colores
    private Color [] colors;
    
    public void Init(float a, Transform [] corns, Color [] cols)
    {
        // Tiempo
        startTime = Time.time;
        // Colores
        colors = cols;
        startColor = colors[0];
        startColor.a = alpha = a;
        starImage.color = startColor;
        // Posiciones
        corners = corns;
        startMarker = transform.position;
        endMarker = corners[indCorner + 1].position;
        journeyLength = Vector3.Distance(startMarker, endMarker);
        
        InvokeRepeating(nameof(AnimationLoop), beginTime, Time.deltaTime);
    }
    
    private void AnimationLoop()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(startMarker, endMarker, fracJourney);
        
        if (fracJourney < 1.0f || changed) return;
        changed = true;
        Invoke(nameof(ChangePath), 0.0f);
    }

    private void ChangePath()
    {
        // Reset del tiempo
        startTime = Time.time;
        changed = false;
        // Cambio de posiciones
        indCorner = indCorner == corners.Length - 1 ? 0 : indCorner + 1;
        startMarker = corners[indCorner].position;
        endMarker = indCorner + 1 == corners.Length ? corners[0].position : corners[indCorner + 1].position;
        journeyLength = Vector3.Distance(startMarker, endMarker);
        // Cambio de color
        indColor = indColor == colors.Length - 1 ? 0 : indColor + 1;
        Color c = colors[indColor];
        c.a = alpha;
        starImage.color = c;
    }
}