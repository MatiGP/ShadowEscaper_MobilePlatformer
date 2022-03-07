using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundOnClickHandler : MonoBehaviour
{
    private Button m_Button = null;

    private void Awake()
    {
        m_Button = GetComponent<Button>();

        BindEvents();
    }

    private void OnDestroy()
    {
        UnBindEvents();
    }

    private void UnBindEvents()
    {
        m_Button.onClick.RemoveListener(HandleButtonPressed);
    }

    private void BindEvents()
    {
        m_Button.onClick.AddListener(HandleButtonPressed);
    }

    private void HandleButtonPressed()
    {
        ShadowRunApp.Instance.SoundManager.PlaySoundEffect(ESoundType.BUTTON_PRESSED);
    }
}
