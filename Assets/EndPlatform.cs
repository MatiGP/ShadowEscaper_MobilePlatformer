using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPlatform : MonoBehaviour
{
    bool isPlatformTurnedOn;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (isPlatformTurnedOn)
        {
            GameManager.instance.FinishLevel(Time.timeSinceLevelLoad);
        }
    }

    public void TurnOnEndLevelPlatform()
    {
        isPlatformTurnedOn = true;
    }
}
