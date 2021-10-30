using Code.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{  
    public event EventHandler OnDamageTaken;

    private void Awake()
    {
        OnDamageTaken += PlayerHealth_OnDamageTaken;
    }

    private void PlayerHealth_OnDamageTaken(object sender, EventArgs e)
    {
        UIManager.Instance.CreatePanel(EPanelID.Death);
    }

    public void TakeDamage()
    {
        OnDamageTaken.Invoke(this, EventArgs.Empty);
    }

    private void OnDestroy()
    {
        OnDamageTaken = null;
    }

}
