using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Code.UI.Panels
{
    public class UIObjectives : UIPanel
    {
        [Header("Keys")]
        [SerializeField] private Image[] m_KeyImages = null;
        [SerializeField] private Sprite m_KeyCollectedSprite;
        [SerializeField] private Sprite m_KeyNotCollectedSprite;
        
        [Header("Stars")]
        [SerializeField] private Image[] m_StarImages = null;
        [SerializeField] private Sprite m_StarCollectedSprite;
        [SerializeField] private Sprite m_StarNotCollectedSprite;

        private int m_CurrentKeys = 0;
        private int m_CurrentStars = 0;

        protected override void Start()
        {
            base.Start();

            SetKeyImages();
            SetStarImages();
        }

        private void SetStarImages()
        {
            int amountOfStars = ShadowRunApp.Instance.GameManager.CurrentLevelData.ItemsCount;

            for (int i = 0; i < amountOfStars; i++)
            {
                m_StarImages[i].gameObject.SetActive(true);
                m_StarImages[i].sprite = m_StarNotCollectedSprite;
            }
        }

        private void SetKeyImages()
        {
            int amountOfKeys = ShadowRunApp.Instance.GameManager.CurrentLevelData.KeysCount;

            for (int i = 0; i < amountOfKeys; i++)
            {
                m_KeyImages[i].gameObject.SetActive(true);
                m_KeyImages[i].sprite = m_KeyNotCollectedSprite;
            }
        }

        public void ResetProgress()
        {
            SetStarImages();
            SetKeyImages();
        }

        private void CollectStar()
        {
            m_StarImages[m_CurrentStars].sprite = m_StarCollectedSprite;
            m_CurrentStars++;
        }

        private void CollectKey()
        {
            m_KeyImages[m_CurrentKeys].sprite = m_KeyCollectedSprite;
            m_CurrentKeys++;
        }

        public void CollectItem(ECollectableType collectableItem)
        {
            if(collectableItem == ECollectableType.Key)
            {
                CollectKey();
            }
            else
            {
                CollectStar();
            }
        }

        public override void BindEvents()
        {
            
        }

        public override void UnBindEvents()
        {
            
        }
    }
}
