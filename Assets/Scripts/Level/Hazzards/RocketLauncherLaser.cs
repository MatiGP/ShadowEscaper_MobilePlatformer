using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class RocketLauncherLaser : MonoBehaviour
{
    public event EventHandler OnPlayerDetected;

    [SerializeField] private float m_LaserLength = 5f;
    [SerializeField] private Transform m_LaserPointTransform;

    private LineRenderer m_LineRenderer;
    private RaycastHit2D m_Hit;
    private Vector2 m_LaserPoint;
    private bool m_IsPlayerDetected;

    void Awake()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        SetLaserPoints();

        if (m_Hit && !m_IsPlayerDetected && m_Hit.collider.CompareTag("Player"))
        {
            OnPlayerDetected?.Invoke(this, EventArgs.Empty);
            DisableLaser();
        }
    }

    private void FixedUpdate()
    {     
        m_Hit = Physics2D.Raycast(transform.position, m_LaserPointTransform.right, m_LaserLength);
    }
    private void SetLaserPoints()
    {
        m_LineRenderer.SetPosition(0, transform.position);

        if (m_Hit)
        {
            m_LineRenderer.SetPosition(1, m_Hit.point);
        }
        else
        {
            m_LaserPoint.x = m_LaserLength * Mathf.Cos((m_LaserPointTransform.eulerAngles.z)
                * Mathf.Deg2Rad) + transform.position.x;
            m_LaserPoint.y = m_LaserLength * Mathf.Sin((m_LaserPointTransform.eulerAngles.z)
                * Mathf.Deg2Rad) + transform.position.y;
            m_LineRenderer.SetPosition(1, m_LaserPoint);
        }
    }

    public void EnableLaser()
    {
        m_IsPlayerDetected = false;
        m_LineRenderer.enabled = true;
    }

    private void DisableLaser()
    {
        m_IsPlayerDetected = true;
        m_LineRenderer.enabled = false;
    }



}
