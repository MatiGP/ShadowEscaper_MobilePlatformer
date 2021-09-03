using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private int m_CollectedKeys = 0;
    public bool HasKey => m_CollectedKeys > 0;
    public int m_CollectedItems { get; private set; } = 0;

    public void CollectKey()
    {
        m_CollectedKeys++;
    }

    public void CollectItem()
    {
        m_CollectedItems++;
    }

    public void UseKey()
    {
        m_CollectedKeys--;
    }

    
}
