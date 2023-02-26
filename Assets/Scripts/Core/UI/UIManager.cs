using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Code.UI.Panels;

namespace Code.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private RectTransform m_PanelParent;

        public static UIManager Instance { get; private set; }


        private Dictionary<EPanelID, UIPanel> m_UIPanels = new Dictionary<EPanelID, UIPanel>();

        private const string PANEL_NAME_FORMAT = "{0}PanelPrefab";
        private const string PANEL_PREFAB_PATH_FORMAT = "Prefabs/UI/{0}";

        private GlobalUILayer m_GlobalUILayer = new GlobalUILayer();
        public void Initialize()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }
        
        public UIPanel CreatePanel(EPanelID panelID)
        {
            if (m_UIPanels.ContainsKey(panelID))
            {
                return m_UIPanels[panelID];
            }

            string panelName = string.Format(PANEL_NAME_FORMAT, panelID);
            string panelPath = string.Format(PANEL_PREFAB_PATH_FORMAT, panelName);

            UIPanel loadedPanel = Instantiate(Resources.Load<UIPanel>(panelPath), m_PanelParent, false);
            loadedPanel.transform.localPosition = Vector3.zero;

            loadedPanel.SetPanelID(panelID);

            loadedPanel.OnPanelClose += LoadedPanel_OnPanelClose;
            
            m_UIPanels.Add(panelID, loadedPanel);
            
            if (loadedPanel != null)
            {
                return loadedPanel;
            }
            
            return null;
        }

        private void LoadedPanel_OnPanelClose(object sender, EPanelID e)
        {
            m_UIPanels.Remove(e);
        }

        public void ClosePanel(EPanelID panelID)
        {
            if (m_UIPanels.ContainsKey(panelID))
            {
                m_UIPanels[panelID].ClosePanel();
            }
        }

        public UIPanel GetPanel(EPanelID panelID)
        {
            return m_UIPanels[panelID];
        }      
    }
}
