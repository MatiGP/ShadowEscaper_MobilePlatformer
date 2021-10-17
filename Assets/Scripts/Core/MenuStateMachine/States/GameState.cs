using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Code.UI.Panels;
using Code.UI;

public class GameState : BaseMenuState
{
    private UIPlayerControls m_PlayerControlPanel = null;
    private UIObjectives m_Objectives = null;
    

    public GameState() : base(EMenuState.Game)
    {

    }

    public override void EnterState()
    {
        LoadUI();
    }

    public override void LeaveState()
    {
        UnloadUI();
    }

    public override void UpdateState()
    {
        
    }

    private void LoadUI()
    {
        m_PlayerControlPanel = UIManager.Instance.CreatePanel(EPanelID.PlayerUI) as UIPlayerControls;
        m_PlayerControlPanel.Initialize();

        m_Objectives = UIManager.Instance.CreatePanel(EPanelID.Objectives) as UIObjectives;
        m_Objectives.Initialize();
    }

    private void UnloadUI()
    {
        m_PlayerControlPanel.ClosePanel();
        m_Objectives.ClosePanel();
    }
}
