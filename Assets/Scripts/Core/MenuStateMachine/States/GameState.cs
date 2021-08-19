using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Code.UI.Panels;
using Code.UI;

public class GameState : BaseMenuState
{
    private UIPlayerControls m_PlayerControlPanel = null;
    

    public GameState() : base(EMenuState.Game)
    {

    }

    public override void EnterState()
    {
        LoadUI();
    }

    public override void LeaveState()
    {
        
    }

    public override void UpdateState()
    {
        
    }

    private void LoadUI()
    {
        m_PlayerControlPanel = UIManager.Instance.CreatePanel(EPanelID.PlayerUI) as UIPlayerControls;
        m_PlayerControlPanel.Initialize();
    }
}
