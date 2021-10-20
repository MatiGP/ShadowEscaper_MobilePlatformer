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
        public event EventHandler OnKeyCollected;
        public event EventHandler OnStarCollected;

        [Header("Keys")]
        [SerializeField] private Image[] m_KeyImages = null;
        [SerializeField] private Color m_KeyCollectedColor;
       
        private int m_CurrentKeys = 0;

        public override void BindEvents()
        {
            OnKeyCollected += HandleKeyCollected;
            OnStarCollected += HandleStarCollected;
        }

        private void HandleStarCollected(object sender, EventArgs e)
        {
            
        }

        private void HandleKeyCollected(object sender, EventArgs e)
        {
            
        }

        public override void Initialize()
        {
            int amountOfKeys = ShadowRunApp.Instance.GameManager.CurrentLevelData.KeysCount;

            Debug.Log($"Amount of keys : {amountOfKeys}");

            for(int i = 0; i < amountOfKeys; i++)
            {
                m_KeyImages[i].gameObject.SetActive(true);
            }
        }

        public override void UnBindEvents()
        {
            OnKeyCollected -= HandleKeyCollected;
            OnStarCollected -= HandleStarCollected;
        }        

        public void CollectKey()
        {
            m_KeyImages[m_CurrentKeys].color = m_KeyCollectedColor;

            m_CurrentKeys++;

        }

        private void OnDestroy()
        {
            UnBindEvents();
        }
    }
}
