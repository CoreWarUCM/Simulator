using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EstelaMovement : MonoBehaviour
{
    [SerializeField] private Image starImage;
    [SerializeField] private Transform[] corners;
    [SerializeField] private float speed = 500.0F;
    [SerializeField] private float beginTime = 0.0f;
    [SerializeField] Transform startPosition;
    [SerializeField] float alpha;
    [SerializeField] Color startColor;

    private int index = 0;
    private Vector3 startMarker;
    private Vector3 endMarker;
    private float startTime;
    private float journeyLength;

    private void Start()
    {
        startTime = Time.time;
        startColor.a = alpha;
        starImage.color = startColor;
        startMarker = startPosition.position;
        endMarker = corners[index + 1].position;
        journeyLength = Vector3.Distance(startMarker, endMarker);
        
        InvokeRepeating(nameof(AnimationLoop), beginTime, Time.deltaTime);
    }

    private void AnimationLoop()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(startMarker, endMarker, fracJourney);
        
        if (fracJourney < 1.0f) return;
        ChangePath();
    }

    private void ChangePath()
    {
        index++;
        startTime = Time.time;
        if (index == corners.Length) index = 0;
        startMarker = corners[index].position;
        endMarker = index + 1 == corners.Length ? corners[0].position : corners[index + 1].position;
        journeyLength = Vector3.Distance(startMarker, endMarker);
        //Color c = new Color(Random.Range(0, 1.0f), Random.Range(0, 1.0f), Random.Range(0, 1.0f), alpha);
        //starImage.color = c;
    }
}