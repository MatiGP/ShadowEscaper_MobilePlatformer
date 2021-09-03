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

        private float m_ScreenAspectRatio = 1.777f;

        public void Initialize()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        private void Start()
        {
            m_ScreenAspectRatio = (float)Screen.height / (float)Screen.width;
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
            
            RecalculatePanelSizeDelta(loadedPanel);

            loadedPanel.SetPanelID(panelID);

            loadedPanel.OnPanelClose += LoadedPanel_OnPanelClose;

            if (loadedPanel != null)
            {
                m_UIPanels.Add(panelID, loadedPanel);
                return loadedPanel;
            }

            return null;
        }

        private void RecalculatePanelSizeDelta(UIPanel loadedPanel)
        {
            RectTransform rectTransform = loadedPanel.GetComponent<RectTransform>();
            float newHeight = rectTransform.sizeDelta.x * m_ScreenAspectRatio;
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, newHeight);
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
