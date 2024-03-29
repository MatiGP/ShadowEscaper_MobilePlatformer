﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalUILayer
{
    private List<EPanelID> m_PanelOrder = new List<EPanelID>()
    {
        //MENU
        EPanelID.Background,
        EPanelID.MainMenu,
        EPanelID.TutorialNotification,
        EPanelID.LevelSelection,
        EPanelID.Fashion,
        EPanelID.DailyGift,
        EPanelID.Challenge,
        EPanelID.Shop,
        EPanelID.Settings,
        
        //GAME
        EPanelID.PlayerUI,
        EPanelID.TutorialOverlay,
        EPanelID.Objectives,
        EPanelID.InGameSettings,
        EPanelID.EndLevelMenu,
        EPanelID.EndLevelTutorial,       
              
        EPanelID.TapToContinue,

        EPanelID.LoadLevel,
        EPanelID.Death
    };

    public List<EPanelID> PanelOrder => m_PanelOrder;
}
