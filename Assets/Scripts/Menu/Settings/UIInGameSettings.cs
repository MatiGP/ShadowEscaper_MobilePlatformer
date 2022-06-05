using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Panels
{
    public class UIInGameSettings : UISettings
    {
        [SerializeField] private Button m_QuitGameButton = null;

        protected override void BindEvents()
        {
            base.BindEvents();
            m_QuitGameButton.onClick.AddListener(HandleQuit);
        }

        protected override void UnBindEvents()
        {
            base.UnBindEvents();
            m_QuitGameButton.onClick.RemoveListener(HandleQuit);
        }

        private void HandleQuit()
        {
            ClosePanel();
            ShadowRunApp.Instance.GameManager.InvokeOnGameExit();
        }
    }
}
