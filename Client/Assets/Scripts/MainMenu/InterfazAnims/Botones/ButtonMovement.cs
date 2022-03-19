using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class ButtonMovement : MonoBehaviour
{
    [SerializeField] private RectTransform startPos;
    [SerializeField] private RectTransform targetPos;
    
    // Tiempo inicial en el que se empieza a mover el objeto
    private float startTime;
    // Distancia total entre los puntos
    private float distance;
    // Punto inicial del movimiento
    private Vector3 startMarker;
    // Punto final del movimiento
    private Vector3 endMarker;
    // Tiempo que dura la transición
    private float time = 2.0F;
    // Velocidad del movimiento
    private float speed;

    public void Init(float t)
    {
        // Keep a note of the time the movement started.
        startTime = Time.time;
        startMarker = startPos.position;
        endMarker = targetPos.position;
        // Calculate the journey length.
        distance = Vector3.Distance(startMarker, endMarker);
        // vel = dist / time
        time = t;
        speed = distance / time;
        InvokeRepeating(nameof(AnimationLoop), 0.0f, Time.deltaTime);
    }

    // Move to the target end position.
    private void AnimationLoop()
    {
        // Distance moved equals elapsed time times speed..
        float distCovered = (Time.time - startTime) * speed;

        // Fraction of journey completed equals current distance divided by total distance.
        float fracJourney = distCovered / distance;

        // Set our position as a fraction of the distance between the markers.
        transform.position = Vector3.Lerp(startMarker, endMarker, fracJourney);
        
        if (fracJourney < 1.0f) return;
        CancelInvoke();
    }
    
    
}
