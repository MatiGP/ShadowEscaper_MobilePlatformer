using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EndPlatform : MonoBehaviour
{
    [SerializeField] private SpriteRenderer m_SpriteRenderer = null;

    [SerializeField] private Sprite m_PadActiveSprite = null;
    
    private bool isPlatformTurnedOn;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (isPlatformTurnedOn)
        {
            // Show end level menu.

            ShadowRunApp.Instance.LevelLoader.LoadNextLevel();
        }
    }

    public void TurnOnEndLevelPlatform()
    {
        isPlatformTurnedOn = true;

        m_SpriteRenderer.sprite = m_PadActiveSprite;
    }
}
