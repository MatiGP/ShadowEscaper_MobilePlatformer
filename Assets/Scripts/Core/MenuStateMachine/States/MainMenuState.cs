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
        private UITutorialNotification m_TutorialNotification = null;

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

            Debug.Log("Is Tutorial Completed: " + SaveSystem.IsTutorialCompleted);

            if (!SaveSystem.IsTutorialCompleted)
            {
                m_TutorialNotification = UIManager.Instance.CreatePanel(EPanelID.TutorialNotification) as UITutorialNotification;                
            }
            
            if (m_MainMenuPanel != null)
            {
                m_MainMenuPanel.ShowPanel();
            }
            

        }

        private void HandleTutorialDecision(object sender, bool decision)
        {
            if (decision)
            {
                ShadowRunApp.Instance.LevelLoader.LoadTutorialLevel();
            }
            else
            {
                m_TutorialNotification.ClosePanel();
            }
        }

        private void BindEvents()
        {
            m_MainMenuPanel.OnPlayPressed += HandlePlayPressed;
            m_MainMenuPanel.OnSettingsPressed += HandleSettingsPressed;

            if (!SaveSystem.IsTutorialCompleted)
            {
                m_TutorialNotification.OnTutorialDecision += HandleTutorialDecision;
            }
        }

        private void HandleSettingsPressed(object sender, System.EventArgs e)
        {
            UIManager.Instance.CreatePanel(EPanelID.Settings);
        }

        private void HandlePlayPressed(object sender, System.EventArgs e)
        {
            m_LevelSelectPanel = UIManager.Instance.CreatePanel(EPanelID.LevelSelection) as UILevelSelectPanel;
        }

        private void UnLoadUI()
        {
            m_BackgroundPanel?.ClosePanel();
            m_MainMenuPanel?.ClosePanel();
            m_LevelSelectPanel?.ClosePanel();
            
            if (m_TutorialNotification)
            {
                m_TutorialNotification.ClosePanel();
            }
        }

        private void UnBindEvents()
        {
            m_MainMenuPanel.OnPlayPressed -= HandlePlayPressed;
            m_MainMenuPanel.OnSettingsPressed -= HandleSettingsPressed;

            if (!SaveSystem.IsTutorialCompleted)
            {
                m_TutorialNotification.OnTutorialDecision -= HandleTutorialDecision;
            }
        }
    }
}
