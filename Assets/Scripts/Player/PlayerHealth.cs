using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PlayerHealth : MonoBehaviour
{
    [SerializeField] UnityEvent OnDamageTaken;

    public void TakeDamage()
    {       
        OnDamageTaken.Invoke();
        
    }
}
