using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExclamationMark : MonoBehaviour
{
    [SerializeField] Color32 showColor;
    [SerializeField] Color32 hideColor;
    [SerializeField] SpriteRenderer spriteRenderer;


    public void ShowMark()
    {
        spriteRenderer.color = showColor;
    }

    public void HideMark()
    {
        spriteRenderer.color = hideColor;
    }
}
