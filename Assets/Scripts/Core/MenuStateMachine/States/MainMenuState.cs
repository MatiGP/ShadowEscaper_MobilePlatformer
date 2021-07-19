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
            
        }

        private void UnBindEvents()
        {

        }
    }
}
