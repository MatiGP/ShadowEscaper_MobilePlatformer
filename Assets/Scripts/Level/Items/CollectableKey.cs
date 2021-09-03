using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableKey : MonoBehaviour
{
    public event EventHandler OnKeyCollected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnKeyCollected.Invoke(this, EventArgs.Empty);
        }
    }
}
