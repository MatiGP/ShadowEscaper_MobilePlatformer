using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Panels
{
    public class UITapToContinue : UIPanel
    {
        [SerializeField] private Button m_TapToStart = null;

        public override void BindEvents()
        {
            m_TapToStart.onClick.AddListener(ClosePanel);
        }

        public override void Initialize()
        {
            BindEvents();
        }

        public override void UnBindEvents()
        {
            m_TapToStart.onClick.RemoveListener(ClosePanel);
        }

        private void OnDestroy()
        {
            UnBindEvents();
        }
    }
}