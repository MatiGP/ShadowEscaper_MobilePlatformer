﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

namespace Code.UI.Panels
{
    public class UIDeathPanel : UIPanel
    {
        [SerializeField] private Button m_TryAgainButton = null;
        
        [SerializeField] private CanvasGroup m_CanvasGroup = null;
        [SerializeField] private float m_FadeInDuration = 0.5f;

        protected override void Awake()
        {
            base.Awake();
            BindEvents();
        }

        protected override void Start()
        {
            base.Start();
            StartFadeIn();           
        }
        
        public override void BindEvents()
        {
            m_TryAgainButton.onClick.AddListener(StartFadeOut);
        }

        public override void UnBindEvents()
        {
            m_TryAgainButton.onClick.RemoveListener(StartFadeOut);
        }

        private void StartFadeIn()
        {
            m_CanvasGroup.DOFade(1, m_FadeInDuration).OnComplete(
                () =>
                {
                    m_TryAgainButton.interactable = true;
                }
                ).Play();
        }

        private void StartFadeOut()
        {
            m_TryAgainButton.interactable = false;

            m_CanvasGroup.DOFade(0, m_FadeInDuration).OnComplete(
                () =>
                {
                    ShadowRunApp.Instance.LevelLoader.ReloadLevel();
                    ClosePanel();
                }
                ).Play();
        }

        private void OnDestroy()
        {
            UnBindEvents();
        }
    }
}
