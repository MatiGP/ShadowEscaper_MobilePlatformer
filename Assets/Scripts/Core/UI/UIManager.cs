using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Code.UI.Panels;

namespace Code.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Canvas m_MainCanvas;
        [SerializeField] private Camera m_UICamera;

        public static UIManager Instance { get; private set; }


        private Dictionary<EPanelID, UIPanel> m_UIPanels = new Dictionary<EPanelID, UIPanel>();

        private const string PANEL_NAME_FORMAT = "{0}PanelPrefab";
        private const string PANEL_PREFAB_PATH_FORMAT = "Prefabs/UI/{0}";

        public void Initialize()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            m_UICamera.transform.position = m_MainCanvas.transform.position;
        }

        public UIPanel CreatePanel(EPanelID panelID)
        {
            if (m_UIPanels.ContainsKey(panelID))
            {
                return m_UIPanels[panelID];
            }

            string panelName = string.Format(PANEL_NAME_FORMAT, panelID);
            string panelPath = string.Format(PANEL_PREFAB_PATH_FORMAT, panelName);

            UIPanel loadedPanel = Instantiate(Resources.Load<UIPanel>(panelPath));
            loadedPanel.transform.SetParent(m_MainCanvas.transform);
            loadedPanel.transform.localPosition = new Vector3(0, 0, 0);

            if (loadedPanel != null)
            {
                m_UIPanels.Add(panelID, loadedPanel);
                return loadedPanel;
            }

            return null;
        }

        public void ClosePanel(EPanelID panelID)
        {
            if (m_UIPanels.ContainsKey(panelID))
            {
                m_UIPanels[panelID].ClosePanel();
                m_UIPanels.Remove(panelID);
            }
        }

        public UIPanel GetPanel(EPanelID panelID)
        {
            return m_UIPanels[panelID];
        }      
    }
}
