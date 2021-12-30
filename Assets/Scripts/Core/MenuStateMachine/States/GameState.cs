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
        BindEvents();

    }

    public override void LeaveState()
    {
        UnloadUI();
        UnBindEvents();
    }

    public override void UpdateState()
    {
        
    }

    private void BindEvents()
    {
        ShadowRunApp.Instance.LevelLoader.OnLevelLoaded += SpawnTapToContinue;
        ShadowRunApp.Instance.LevelLoader.OnLevelLoaded += ResetProgress;
    }

    private void ResetProgress(object sender, System.EventArgs e)
    {
        m_Objectives.ResetProgress();
    }

    private void UnBindEvents()
    {
        ShadowRunApp.Instance.LevelLoader.OnLevelLoaded -= SpawnTapToContinue;
    }

    private void SpawnTapToContinue(object sender, System.EventArgs e)
    {
        Debug.Log("Should spawn tap to continue");
        UIManager.Instance.CreatePanel(EPanelID.TapToContinue);
    }

    private void LoadUI()
    {       
        m_PlayerControlPanel = UIManager.Instance.CreatePanel(EPanelID.PlayerUI) as UIPlayerControls;
        m_Objectives = UIManager.Instance.CreatePanel(EPanelID.Objectives) as UIObjectives;
    }

    private void UnloadUI()
    {
        m_PlayerControlPanel.ClosePanel();
        m_Objectives.ClosePanel();
    }
}
