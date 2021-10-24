using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Panels
{
    public class UITapToContinue : UIPanel
    {
        [SerializeField] private Button m_TapToStart = null;

        protected override void Awake()
        {
            base.Awake();
            BindEvents();
        }

        public override void BindEvents()
        {
            m_TapToStart.onClick.AddListener(HandleStartButtonPressed);
        }      

        public override void UnBindEvents()
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