#pragma warning disable 649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovmentSciprt : MonoBehaviour
{
    [SerializeField] private Transform endPoint  ;
    [SerializeField] private float speed = 1.0f;

    private Vector3 position;
    private float startTime;
    private float totalDistanceToEnd;

    void Start()
    {
        position = transform.position;
        startTime = Time.time;
        totalDistanceToEnd = Vector3.Distance(position, endPoint.position);
    }

    // Update is called once per frame
    void Update()
    {
        float currDuration = Time.time - startTime;
        float journeyFraction = currDuration / totalDistanceToEnd;
        transform.position = Vector3.Lerp(position, endPoint.position, speed * journeyFraction);
        
    }
}


