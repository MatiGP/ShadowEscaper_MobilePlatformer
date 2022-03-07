﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Code.UI.Panels;
using Code.UI;
using System;

public class TutorialGameState : GameState
{
    UITutorialPanel m_UITutorialPanel = null;

    public TutorialGameState() : base()
    {
        m_StateType = EMenuState.Tutorial;
    }

    public override void EnterState()
    {
        Debug.Log("Entering tutorial game state");
        base.EnterState();
        BindEvents();
    }

    private void BindEvents()
    {
        ShadowRunApp.Instance.GameManager.OnGameCompleted += HandleGameCompleted;
    }

    private void UnBindEvents()
    {
        ShadowRunApp.Instance.GameManager.OnGameCompleted -= HandleGameCompleted;
    }

    private void HandleGameCompleted(object sender, EventArgs e)
    {
        SaveSystem.IsTutorialCompleted = true;
    }

    protected override void LoadUI()
    {
        base.LoadUI();
        Debug.Log("Loading in tutorial Game state");
        m_UITutorialPanel = UIManager.Instance.CreatePanel(EPanelID.TutorialOverlay) as UITutorialPanel;
    }

    protected override void UnloadUI()
    {
        base.UnloadUI();
        m_UITutorialPanel.ClosePanel();
    }

    public override void LeaveState()
    {
        UnBindEvents();
        base.LeaveState();
    }
}
