using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Code.UI.Panels
{
    public class UILoadingScreen : UIPanel
    {
        [SerializeField] private Image m_LoadingSymbol = null;
        [SerializeField] private CanvasGroup m_CanvasGroup = null;

        private Tween m_FadeTween = null;

        [SerializeField] private float m_FadeDuration = 1.1f;

        private void BindEvents()
        {
            
        }

        private void UnBindEvents()
        {
            
        }

        public override void ClosePanel()
        {
            m_FadeTween = m_CanvasGroup.DOFade(0, m_FadeDuration).OnComplete(() => base.ClosePanel()).Play();
        }

        public void SetFill(float fill)
        {
            m_LoadingSymbol.fillAmount = fill;
        }
    }
}
