using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Spikes : MonoBehaviour
{
    [SerializeField] private float m_SpikeHeight = 1.4f;
    [SerializeField] private float m_SpikeActivationTime;
    [SerializeField] private float m_SpikeStayTime;
    [SerializeField] private BoxCollider2D m_SpikeCollider;

    private Tween m_ShowSpikes = null;
    private Tween m_HideSpikes = null;

    private void Awake()
    {
        
    }
}
