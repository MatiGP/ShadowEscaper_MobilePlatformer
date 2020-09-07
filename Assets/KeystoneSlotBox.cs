using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeystoneSlotBox : MonoBehaviour
{
    public bool HasKey { get; private set; }
    [SerializeField] Sprite activeKeystoneSlot;
    [SerializeField] SpriteRenderer spriteRenderer;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            KeystoneManager keystoneManager = collision.GetComponent<KeystoneManager>();

            if (keystoneManager.UseKey())
            {
                HasKey = true;
                spriteRenderer.sprite = activeKeystoneSlot;
            }
        }
    }

}
