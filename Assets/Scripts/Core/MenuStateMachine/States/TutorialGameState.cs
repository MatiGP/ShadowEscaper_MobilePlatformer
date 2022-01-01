using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Code.UI.Panels;
using Code.UI;
using System;

public class TutorialGameState : GameState
{  
    protected override void HandleGameCompleted(object sender, EventArgs e)
    {
        UIManager.Instance.CreatePanel(EPanelID.Tutorial);
    }
}
