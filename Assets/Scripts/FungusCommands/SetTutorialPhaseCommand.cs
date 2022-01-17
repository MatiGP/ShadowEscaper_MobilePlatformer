using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using Code.UI.Panels;
using Code.UI;

[CommandInfo("ShadowRunCommands", "Set Tutorial Phase Command", "Sets tutorial phase")]
public class SetTutorialPhaseCommand : Command
{
    [SerializeField] private ETutorialPhase m_TutorialPhase = ETutorialPhase.None;
    [SerializeField] private bool m_IsEnabled = false;

    public override void OnEnter()
    {
        base.OnEnter();
        UITutorialPanel uITutorialPanel = UIManager.Instance.CreatePanel(EPanelID.EndLevelTutorial) as UITutorialPanel;
        uITutorialPanel.SetUpHand(m_TutorialPhase);
        uITutorialPanel.SetControlsEnabled(m_TutorialPhase, m_IsEnabled);
        Continue();
    }
}
