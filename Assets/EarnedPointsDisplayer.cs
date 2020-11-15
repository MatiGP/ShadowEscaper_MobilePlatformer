using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   

public class EarnedPointsDisplayer : MonoBehaviour
{

    [SerializeField] Image[] earnedPointIcons;

    public void DisplayIcons(int earnedPoints)
    {
        for(int i = 0; i < earnedPoints; i++)
        {
            Debug.Log("Displaying. . . ");
            earnedPointIcons[i].gameObject.SetActive(true);
        }
    }
}
