using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Panels
{
    public class UIObjectives : UIPanel
    {
        [Header("Keys")]
        [SerializeField] private Image[] m_KeyImages = null;
        [SerializeField] private Color m_KeyCollectedColor;
       
        private int m_CurrentKeys = 0;

        public override void BindEvents()
        {
            
        }

        public override void Initialize()
        {
            
        }

        public override void UnBindEvents()
        {
            
        }

        public void SetUpKeysCount(int count)
        {
            for(int i = count; i < m_KeyImages.Length; i++)
            {
                m_KeyImages[i].gameObject.SetActive(false);
            }
        }

        public void CollectKey()
        {
            m_KeyImages[m_CurrentKeys].color = m_KeyCollectedColor;

            m_CurrentKeys++;

        }
    }
}
