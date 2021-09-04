using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelPlatform : MonoBehaviour
{
    [SerializeField] List<KeystoneSlotBox> keystoneSlotBoxes;
    [SerializeField] SpriteRenderer padSpriteRenderer;
    [SerializeField] Sprite activePadSprite;
    [SerializeField] EndPlatform platform;
    
    public void CheckSlotBoxes()
    {
        for(int i = 0; i < keystoneSlotBoxes.Count; i++)
        {
            if (!keystoneSlotBoxes[i].HasKey)
            {
                return;
            }
        }

        padSpriteRenderer.sprite = activePadSprite;
        platform.TurnOnEndLevelPlatform();


    }
}
