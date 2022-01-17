using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using Code.UI.Panels;
using Code.UI;

[CommandInfo("ShadowRunCommands", "Set Player Controls", "Set Players Controls Enabled")]
public class SetPlayerControlsCommand : Command
{
    [SerializeField] private bool m_ShouldReceiveInput = true;
    public override void OnEnter()
    {
        base.OnEnter();

        UIPlayerControls uIPlayerControls = UIManager.Instance.GetPanel(EPanelID.PlayerUI) as UIPlayerControls;
        uIPlayerControls.SetReceiveInput(m_ShouldReceiveInput);

        Continue();
    }
}
