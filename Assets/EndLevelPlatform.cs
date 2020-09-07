using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelPlatform : MonoBehaviour
{
    [SerializeField] List<KeystoneSlotBox> keystoneSlotBoxes;
    [SerializeField] SpriteRenderer padSpriteRenderer;
    [SerializeField] Sprite activePadSprite;
    
    public void CheckSlotBoxes()
    {
        foreach(KeystoneSlotBox slotBox in keystoneSlotBoxes)
        {
            if (!slotBox.HasKey)
            {
                return;
            }
        }

        padSpriteRenderer.sprite = activePadSprite;

    }
}
