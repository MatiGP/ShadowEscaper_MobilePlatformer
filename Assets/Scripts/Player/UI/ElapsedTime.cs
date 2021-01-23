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

    string timeText;

    bool stopCounting = true;

    private void Update()
    {
        if (stopCounting) return;

        minutes = (int)(Time.timeSinceLevelLoad / 60f) % 60;        
        seconds = (int)(Time.timeSinceLevelLoad % 60f);
        miliseconds = (int)(Time.timeSinceLevelLoad * 1000f) % 1000;

        if(minutes < 10)
        {
            timeText = $"0{minutes}:";
        }
        else
        {
            timeText = $"{minutes}:";
        }

        if(seconds < 10)
        {
            timeText += $"0{seconds}:";
        }
        else
        {
            timeText += $"{seconds}:";
        }

        if (miliseconds < 10)
        {
            timeText += $"00{miliseconds.ToString()}";
        }else if(miliseconds >= 10 && miliseconds < 100)
        {
            timeText += $"0{miliseconds.ToString()}";
        }
        else
        {
            timeText += $"{miliseconds.ToString()}";
        }


        text.text = timeText;
    }

    public void StopCounting()
    {
        stopCounting = true;
    }

    public void StartCounting()
    {
        stopCounting = false;
    }
}
