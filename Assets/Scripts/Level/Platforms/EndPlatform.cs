using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Code.UI;

public class EndPlatform : MonoBehaviour
{
    [SerializeField] private SpriteRenderer m_SpriteRenderer = null;

    [SerializeField] private Sprite m_PadActiveSprite = null;
    
    private bool m_IsPlatformTurnedOn;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (m_IsPlatformTurnedOn)
        {
            UIManager.Instance.CreatePanel(EPanelID.EndLevelMenu);        
        }
    }

    public void TurnOnEndLevelPlatform()
    {
        m_IsPlatformTurnedOn = true;

        m_SpriteRenderer.sprite = m_PadActiveSprite;
    }
}
