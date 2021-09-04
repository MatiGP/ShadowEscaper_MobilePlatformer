using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private Dictionary<ECollectableType, int> m_OwnedItems = new Dictionary<ECollectableType, int>();
    
    public void AddItem(ECollectableType item)
    {
        m_OwnedItems[item]++;
    }

    public int GetItemCount(ECollectableType item)
    {
        return m_OwnedItems[item];
    }

    public void RemoveItemFromInventory(ECollectableType item)
    {
        m_OwnedItems[item]--;
    }
}
