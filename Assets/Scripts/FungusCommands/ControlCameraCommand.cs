using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using DG.Tweening;
using Cinemachine;

[CommandInfo("ShadowRunCommands", "Control Camera", "Controls camera")]
public class ControlCameraCommand : Command
{
    [SerializeField] private ECameraAction m_CameraAction = ECameraAction.None;

    private CameraController m_CameraController = null;
    public override void OnEnter()
    {
        base.OnEnter();
        m_CameraController = GetFlowchart().GetGameObjectVariable("Camera").GetComponent<CameraController>();

        switch (m_CameraAction)
        {           
            case ECameraAction.FollowPlayerCharacter:
                m_CameraController.ChangeViewToPlayerCharacter();
                break;
            case ECameraAction.FollowTutorialCharacter:
                m_CameraController.ChangeViewToTutorialCharacter();
                break;
            case ECameraAction.ZoomIn:
                m_CameraController.ZoomIn();
                break;
            case ECameraAction.ZoomOut:
                m_CameraController.ZoomOut();
                break;
        }
        Continue();
    }

    private enum ECameraAction
    {   
        None,
        FollowTutorialCharacter,
        FollowPlayerCharacter,
        ZoomIn,
        ZoomOut
    }

}


