using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace Code
{
    public class EndPlatform : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer m_SpriteRenderer = null;

        [SerializeField] private Sprite m_PadActiveSprite = null;

        private bool m_IsPlatformTurnedOn;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (m_IsPlatformTurnedOn)
            {
                PlayerInventory playerInventory = collision.GetComponent<PlayerInventory>();
                int starCount = playerInventory.GetItemCount(ECollectableType.Star);

                ShadowRunApp.Instance.GameManager.SetCollectedItemsCount(starCount);
                ShadowRunApp.Instance.GameManager.SummarizeLevel();
            }
        }

        public void TurnOnEndLevelPlatform()
        {
            m_IsPlatformTurnedOn = true;

            m_SpriteRenderer.sprite = m_PadActiveSprite;
        }
    }
}
