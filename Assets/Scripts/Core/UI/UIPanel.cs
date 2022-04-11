using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.UI.Panels
{
    public abstract class UIPanel : MonoBehaviour
    {
        public event EventHandler<EPanelID> OnPanelClose;

        protected EPanelID m_PanelID;

        protected virtual void Awake()
        {

        }

        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {

        }

        public void SetPanelID(EPanelID panelID)
        {
            m_PanelID = panelID;
        }

        public virtual void ClosePanel()
        {
            OnPanelClose.Invoke(this, m_PanelID);
            OnPanelClose = null;
            Destroy(gameObject);
        }
       
        public void ShowPanel()
        {
            gameObject.SetActive(true);
        }
    }
}