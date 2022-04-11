using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Panels
{
    public class UITutorialNotification : UIPanel
    {
        public event EventHandler<bool> OnTutorialDecision; 

        [SerializeField] private Button m_NoButton = null;
        [SerializeField] private Button m_YesButton = null;

        protected override void Awake()
        {
            base.Awake();
            BindEvents();
        }

        private void OnDestroy()
        {
            UnBindEvents();
        }

        private void BindEvents()
        {
            m_NoButton.onClick.AddListener(HandleNoButtonClicked);
            m_YesButton.onClick.AddListener(HandleYesButtonClicked);
        }

        private void UnBindEvents()
        {
            m_NoButton.onClick.RemoveListener(HandleNoButtonClicked);
            m_YesButton.onClick.RemoveListener(HandleYesButtonClicked);
        }         

        private void HandleNoButtonClicked()
        {
            OnTutorialDecision.Invoke(this, false);
        }

        private void HandleYesButtonClicked()
        {
            OnTutorialDecision.Invoke(this, true);
        }
    }
}
