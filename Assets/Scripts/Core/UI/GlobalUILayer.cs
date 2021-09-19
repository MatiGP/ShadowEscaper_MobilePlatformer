using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalUILayer
{
    private List<EPanelID> m_PanelOrder = new List<EPanelID>()
    {
        //MENU
        EPanelID.Background,
        EPanelID.MainMenu,
        EPanelID.LevelSelection,
        EPanelID.Fashion,
        EPanelID.DailyGift,
        EPanelID.Challenge,
        EPanelID.Shop,
        
        //GAME
        EPanelID.PlayerUI,
        EPanelID.Objectives,
        EPanelID.Settings,
        EPanelID.Death,
        EPanelID.LoadLevel
    };

    public List<EPanelID> PanelOrder => m_PanelOrder;
}
