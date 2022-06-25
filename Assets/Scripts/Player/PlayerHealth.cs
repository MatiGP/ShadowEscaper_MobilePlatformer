using Code.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{  
    public event EventHandler OnDamageTaken;

    private bool m_IsDead = false;

    public void TakeDamage()
    {
        if(!m_IsDead)
        {
            m_IsDead = true;
            OnDamageTaken?.Invoke( this, EventArgs.Empty );
            UIManager.Instance.CreatePanel( EPanelID.Death );
        }
    }

    private void OnDestroy()
    {
        OnDamageTaken = null;
    }

}
