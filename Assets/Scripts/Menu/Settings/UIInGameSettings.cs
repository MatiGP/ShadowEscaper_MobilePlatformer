using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Panels
{
    public class UIInGameSettings : UISettings
    {
        [SerializeField] private Button m_QuitGameButton = null;

        private float m_DefaultTimeScale = 0.0f;

        protected override void Awake()
        {
            base.Awake();
            m_DefaultTimeScale = Time.timeScale;
        }

        protected override void BindEvents()
        {
            base.BindEvents();
            m_QuitGameButton.onClick.AddListener(HandleQuit);
            m_BackButton.onClick.AddListener(HandleInGamePause);
        }

        protected override void UnBindEvents()
        {
            base.UnBindEvents();
            m_QuitGameButton.onClick.RemoveListener(HandleQuit);
            m_BackButton.onClick.RemoveListener( HandleInGamePause);
        }

        private void HandleQuit()
        {
            ShadowRunApp.Instance.GameManager.InvokeOnGameExit();
            ClosePanel();           
        }

        private void HandleInGamePause()
        {
            
            Time.timeScale = m_DefaultTimeScale;
            ClosePanel();
        }
    }
}
