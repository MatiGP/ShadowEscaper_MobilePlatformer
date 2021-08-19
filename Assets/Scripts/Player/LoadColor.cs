using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadColor : MonoBehaviour
{
    SpriteRenderer sr;
    Color c;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        //c = SaveSystem.instance.GetColorData();
        Debug.Log("Loading color...");
        sr.color = c;
    } 
}
