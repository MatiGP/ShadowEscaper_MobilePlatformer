using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HiddenSpikes : MonoBehaviour
{
    [SerializeField] protected float m_SpikeMaxHeight = 1.4f;
    [SerializeField] protected float m_SpikeMinHeight = -0.81f;
    [SerializeField] protected float m_SpikeShowUpDuration = 0.4f;
    [SerializeField] protected float m_SpikeHideDuration = 0.2f;
    [SerializeField] protected float m_SpikeActivationTime = 0.2f;
    [SerializeField] protected float m_SpikeStayTime = 0.5f;

    [SerializeField] protected Transform m_SpikeTransform = null;

    protected Tween m_ShowSpikes = null;
    protected Tween m_HideSpikes = null;
    protected Sequence m_SpikeSequence;

    protected void Awake()
    {
        m_SpikeSequence = DOTween.Sequence();
        m_HideSpikes = m_SpikeTransform.DOLocalMoveY(m_SpikeMinHeight, m_SpikeHideDuration).SetDelay(m_SpikeStayTime);
        m_ShowSpikes = m_SpikeTransform.DOLocalMoveY(m_SpikeMaxHeight, m_SpikeShowUpDuration);

        PrepareSpikeSequence();
    }

    protected void PrepareSpikeSequence()
    {
        m_SpikeSequence.Append(m_ShowSpikes).Append(m_HideSpikes).SetDelay(m_SpikeActivationTime).OnComplete(() => m_SpikeSequence.Rewind());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PrepareSpikeSequence();
            m_SpikeSequence.Play();
            
        }
    }


}
