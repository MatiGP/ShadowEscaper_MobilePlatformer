using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Code.UI.Panels;
using Code.UI;

namespace Code.StateMachine
{
    public class MainMenuState : BaseMenuState
    {
        private UIBackground m_BackgroundPanel = null;
        private UIMainMenuPanel m_MainMenuPanel = null;
        private UILevelSelectPanel m_LevelSelectPanel = null;
        private UISettings m_SettingsPanel = null;

        public MainMenuState() : base(EMenuState.MainMenu)
        {

        }

        public override void EnterState()
        {
            LoadUI();
            BindEvents();
        }

        public override void LeaveState()
        {
            UnBindEvents();
            UnLoadUI();
        }

        public override void UpdateState()
        {
            
        }

        private void LoadUI()
        {
            m_BackgroundPanel = UIManager.Instance.CreatePanel(EPanelID.Background) as UIBackground;
            
            m_MainMenuPanel = UIManager.Instance.CreatePanel(EPanelID.MainMenu) as UIMainMenuPanel;
            if(m_MainMenuPanel != null)
            {
                m_MainMenuPanel.Initialize();
                m_MainMenuPanel.ShowPanel();
            }          
        }

        private void BindEvents()
        {
            m_MainMenuPanel.OnPlayPressed += HandlePlayPressed;
            m_MainMenuPanel.OnSettingsPressed += HandleSettingsPressed;
        }

        private void HandleSettingsPressed(object sender, System.EventArgs e)
        {
            m_SettingsPanel = UIManager.Instance.CreatePanel(EPanelID.Settings) as UISettings;
            m_SettingsPanel.Initialize();
        }

        private void HandlePlayPressed(object sender, System.EventArgs e)
        {
            m_LevelSelectPanel = UIManager.Instance.CreatePanel(EPanelID.LevelSelection) as UILevelSelectPanel;
            m_LevelSelectPanel.Initialize();
        }

        private void UnLoadUI()
        {
            m_BackgroundPanel.ClosePanel();
            m_MainMenuPanel.ClosePanel();
            m_LevelSelectPanel.ClosePanel();
        }

        private void UnBindEvents()
        {
            m_MainMenuPanel.OnPlayPressed -= HandlePlayPressed;
        }
    }
}
