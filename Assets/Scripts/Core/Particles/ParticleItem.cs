using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleItem : MonoBehaviour
{
    public event EventHandler<ParticleItem> OnParticleLifetimeEnded;

    public EParticleType ParticleType => m_EParticleType;
    public int MaxParticleCount => m_MaxParticleCount;

    [SerializeField] private EParticleType m_EParticleType;
    [SerializeField] private int m_MaxParticleCount = 2;
    [SerializeField] private float m_ParticleLifetime = 1f;  
   
    private void OnEnable()
    {
        StartCoroutine(ReturnToPool());
    }

    private IEnumerator ReturnToPool()
    {
        yield return m_ParticleLifetime;       
        OnParticleLifetimeEnded.Invoke(this, this);
        gameObject.SetActive(false);
    }
}
