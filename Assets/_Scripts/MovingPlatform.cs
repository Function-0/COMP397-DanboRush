/*
 * @Author: Tzu-Ting Wu 
 * @Date: 2021-02-14 16:04:05
 * @Last Modified by: Tzu-Ting Wu
 * @Last Modified time: 2021-02-14 22:42:30
 */
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3[] points;
    public float tolerance;
    public float speed;
    public float delayTime;

    private int pointIndex;
    private int startPointIndex = 0;
    private Vector3 currentPoint;
    private float delayStart;


    // Start is called before the first frame update
    void Start()
    {
        // Initiate currentPoint to the first point of points
        if (points.Length > 0) {
            currentPoint = points[startPointIndex];
        }
        tolerance = speed * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != currentPoint) {
            MovePlatform();
        }
        else {
            // update currentPoint to the next point
            UpdatePoint();
        }
    }
    // move a step
    void MovePlatform() {
        Vector3 direction = currentPoint - transform.position;
        transform.position += (direction / direction.magnitude) * speed * Time.deltaTime;

        if (direction.magnitude < tolerance) {
            transform.position = currentPoint; // snap platform to the destination smoothly
            delayStart = Time.time;
        }
    }

    void UpdatePoint() {
        if (Time.time - delayStart > delayTime) {
            pointIndex++;
            if (pointIndex >= points.Length) {
                pointIndex = startPointIndex;
            }
            currentPoint = points[pointIndex];
        }
    }

    private void OnTriggerEnter(Collider other) {
        // Move the Player with the platform
        if (other.tag == "Player") {
            Debug.Log("Player enters on the platform.");
            other.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            Debug.Log("Player leaves the platform.");
            other.transform.parent = null;
        }
    }
}
