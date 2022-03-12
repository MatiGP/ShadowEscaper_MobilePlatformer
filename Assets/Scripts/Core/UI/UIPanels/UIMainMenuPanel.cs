using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Code.UI.Panels
{
    public class UIMainMenuPanel : UIPanel
    {
        public event EventHandler OnPlayPressed;
        public event EventHandler OnFashionPressed;
        public event EventHandler OnShopPressed;
        public event EventHandler OnSettingsPressed;

        [SerializeField] private Button m_Play = null;
        [SerializeField] private Button m_Fashion = null;
        [SerializeField] private Button m_Shop = null;
        [SerializeField] private Button m_Settings = null;

        [SerializeField] private TextMeshProUGUI m_StarCount = null;
        [SerializeField] private TextMeshProUGUI m_PremiumCurrencyCount;

        protected override void Awake()
        {
            base.Awake();
            BindEvents();
        }

        protected override void Start()
        {
            base.Start();
            m_StarCount.text = SaveSystem.GetSumOfStars().ToString();
            m_PremiumCurrencyCount.text = SaveSystem.GetPremiumCurrencyCount().ToString();
        }

        public override void BindEvents()
        {
            m_Play.onClick.AddListener(HandlePlayPressed);
            m_Fashion.onClick.AddListener(HandleFashionPressed);
            m_Settings.onClick.AddListener(HandleSettingsPressed);
            m_Shop.onClick.AddListener(HandleShopPressed);
        }     

        public override void UnBindEvents()
        {
            m_Play.onClick.RemoveListener(HandlePlayPressed);
            m_Fashion.onClick.RemoveListener(HandleFashionPressed);
            m_Settings.onClick.RemoveListener(HandleSettingsPressed);
            m_Shop.onClick.RemoveListener(HandleShopPressed);
        }

        private void HandlePlayPressed()
        {
            OnPlayPressed?.Invoke(this, EventArgs.Empty);
        }

        private void HandleFashionPressed()
        {
            OnFashionPressed?.Invoke(this, EventArgs.Empty);
        }

        private void HandleShopPressed()
        {
            OnShopPressed?.Invoke(this, EventArgs.Empty);
        }

        private void HandleSettingsPressed()
        {
            OnSettingsPressed?.Invoke(this, EventArgs.Empty);
        }

        private void OnDestroy()
        {
            UnBindEvents();
        }

    }
}