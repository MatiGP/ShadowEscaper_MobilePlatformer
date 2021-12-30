using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class KeystoneSlotBox : MonoBehaviour
{
    public bool HasKey { get; private set; }

    [SerializeField] private Sprite m_ActiveSlotBox;
    [SerializeField] private SpriteRenderer m_SpriteRenderer;
    [SerializeField] private EndLevelPlatform m_EndLevelPlatform;
    [SerializeField] private SpriteRenderer m_KeyIndicator = null;

    [SerializeField] private float m_IndicatorMoveDuration = 1f;
    [SerializeField] private float m_IndicatorMoveHeight = 0.4f;

    private float m_StartingIndicatorHeight = 0f;

    private void Awake()
    {
        m_StartingIndicatorHeight = m_KeyIndicator.transform.localPosition.y;
        m_KeyIndicator.transform.DOLocalMoveY(m_IndicatorMoveHeight, m_IndicatorMoveDuration).SetLoops(-1, LoopType.Yoyo).Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerInventory playerInventory = collision.GetComponent<PlayerInventory>();

            if (playerInventory.GetItemCount(ECollectableType.Key) > 0)
            {
                playerInventory.RemoveItemFromInventory(ECollectableType.Key);
                
                HasKey = true;
                
                m_SpriteRenderer.sprite = m_ActiveSlotBox;

                m_KeyIndicator.DOKill();
                m_KeyIndicator.gameObject.SetActive(false);

                m_EndLevelPlatform.CheckSlotBoxes();
            }
        }
    }

}
