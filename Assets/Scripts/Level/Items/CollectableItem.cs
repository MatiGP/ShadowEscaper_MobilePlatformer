using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    [SerializeField] private ECollectableType m_CollectableItemType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerInventory playerInventory = collision.GetComponent<PlayerInventory>();
            
            playerInventory.AddItem(m_CollectableItemType);

            gameObject.SetActive(false);
        }
    }
}
