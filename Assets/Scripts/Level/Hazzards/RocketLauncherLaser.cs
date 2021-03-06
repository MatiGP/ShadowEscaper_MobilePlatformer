﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RocketLauncherLaser : MonoBehaviour
{
    [SerializeField] float laserLenght;
    [SerializeField] Transform laserPoint;
    [SerializeField] UnityEvent OnPlayerDetected;

    LineRenderer lineRenderer;
    RaycastHit2D hit;
    Vector2 laserEndPoint;

    // Start is called before the first frame update
    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        lineRenderer.SetPosition(0, transform.position);

        if (hit)
        {
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            laserEndPoint.x = laserLenght * Mathf.Cos((laserPoint.eulerAngles.z) * Mathf.Deg2Rad) + transform.position.x;
            laserEndPoint.y = laserLenght * Mathf.Sin((laserPoint.eulerAngles.z) * Mathf.Deg2Rad) + transform.position.y;
            lineRenderer.SetPosition(1, laserEndPoint);
        }

        if (hit && hit.collider.CompareTag("Player"))
        {           
            OnPlayerDetected.Invoke();
        }            
        
    }

    private void FixedUpdate()
    {
        hit = Physics2D.Raycast(transform.position, laserPoint.right, laserLenght);       
    }
  


}
