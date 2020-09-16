using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ElapsedTime : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    float minutes;
    float seconds;
    float miliseconds;

    private void Update()
    {
        minutes = (int)(Time.timeSinceLevelLoad / 60f) % 60;        
        seconds = (int)(Time.timeSinceLevelLoad % 60f);
        miliseconds = (int)(Time.timeSinceLevelLoad * 1000f) % 1000;

        text.text = $"{minutes.ToString()}:{seconds.ToString()}:{miliseconds.ToString()}";
    }
}
