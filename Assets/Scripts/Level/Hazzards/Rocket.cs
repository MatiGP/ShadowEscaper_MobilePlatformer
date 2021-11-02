using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using DG.Tweening;

public class Rocket : MonoBehaviour
{
    public event EventHandler OnRockedDestroyed;
    [SerializeField] float m_RocketSpeedIncrease = 2f;

    bool m_LaunchRocket;
    private float m_RocketBaseSpeed = 5;
    private float m_RocketSpeed = 0;

    // Update is called once per frame
    void Update()
    {
        if (!m_LaunchRocket) return;
        m_RocketSpeed += m_RocketSpeedIncrease * Time.deltaTime;
        transform.Translate(Vector2.right * m_RocketSpeed * Time.deltaTime, Space.Self);
    }

    public void LaunchRocket()
    {
        transform.parent = null;
        m_LaunchRocket = true;
        m_RocketSpeed = m_RocketBaseSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        m_LaunchRocket = false;       
        OnRockedDestroyed.Invoke(this, EventArgs.Empty);
    }
}
