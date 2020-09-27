﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    [Header("Rocket Manager")]
    [SerializeField] Rocket rocket;
    Vector3 rocketStartPosition;

    [Header("Launcher Rotation")]
    [SerializeField] float rotateSpeed;
    [SerializeField] float minRotation;
    [SerializeField] float maxRotation;
    [SerializeField] float rotationStopDuration;
    
    float currentAngleZ;
    bool rotateClockwise = true;
    bool rocketLaunched;
    bool stopRotating;
    // Update is called once per frame
    void Update()
    {
        if (stopRotating) return;

        if (currentAngleZ >= maxRotation)
        {
            rotateClockwise = false;
        }
        else if(currentAngleZ <= minRotation)
        {
            rotateClockwise = true;
        }

        if (rotateClockwise)
        {
            currentAngleZ += rotateSpeed * Time.deltaTime;
            transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);                       
        }

        if (!rotateClockwise)
        {
            currentAngleZ -= rotateSpeed * Time.deltaTime;
            transform.Rotate(0f, 0f, -rotateSpeed * Time.deltaTime);
        }
        
    }

    public void ResetRocket()
    {
        rocket.transform.position = rocketStartPosition;
        rocket.gameObject.SetActive(true);
    }

    public void LaunchRocket()
    {
        if (rocketLaunched) return;
        StartCoroutine(StopRotating());
        rocketLaunched = true;
        rocket.LaunchRocket();
    }

    IEnumerator StopRotating()
    {
        stopRotating = true;
        yield return new WaitForSeconds(rotationStopDuration);
        stopRotating = false;
    }
}
