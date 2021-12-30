using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("ShadowRunCommands", "Enable Tutorial Character", "Enables tutorial character")]
public class EnableTutorialCharacterCommand : Command
{
    [SerializeField] private bool m_Enable = false;

    public override void OnEnter()
    {
        base.OnEnter();
        GetFlowchart().GetGameObjectVariable("TutorialCharacter").SetActive(m_Enable);
        Continue();
    }
}
