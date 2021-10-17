using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private Dictionary<ECollectableType, int> m_OwnedItems = new Dictionary<ECollectableType, int>();
    
    public void AddItem(ECollectableType item)
    {
        if (!HasItem(item))
        {
            m_OwnedItems.Add(item, 0);
        }
       
        m_OwnedItems[item]++;       
    }

    public int GetItemCount(ECollectableType item)
    {
        return HasItem(item) ? m_OwnedItems[item] : 0;
    }

    public void RemoveItemFromInventory(ECollectableType item)
    {
        if (HasItem(item))
        {
            m_OwnedItems[item]--;
        }      
    }

    private bool HasItem(ECollectableType item)
    {
        return m_OwnedItems.ContainsKey(item);
    }
}
