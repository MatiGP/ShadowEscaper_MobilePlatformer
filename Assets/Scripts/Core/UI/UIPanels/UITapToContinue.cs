using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Code.UI.Panels
{
    public class UITapToContinue : UIPanel
    {
        [SerializeField] private Button m_TapToStart = null;
        [SerializeField] private CanvasGroup m_CanvasGroup = null;
        [SerializeField] private float m_FadeDuration = 0.75f;

        protected override void Awake()
        {
            base.Awake();
            BindEvents();
        }

        protected override void Start()
        {
            base.Start();
            m_CanvasGroup.DOFade(1, m_FadeDuration).OnComplete(() => m_CanvasGroup.interactable = true).Play();
        }

        private void BindEvents()
        {
            m_TapToStart.onClick.AddListener(HandleStartButtonPressed);
        }

        private void UnBindEvents()
        {
            m_TapToStart.onClick.RemoveListener(HandleStartButtonPressed);
        }

        private void HandleStartButtonPressed()
        {
            ShadowRunApp.Instance.GameManager.GameStart();
            ClosePanel();
        }

        private void OnDestroy()
        {
            UnBindEvents();
        }
    }
}