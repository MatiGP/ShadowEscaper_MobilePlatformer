using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   

public class EarnedPointsDisplayer : MonoBehaviour
{

    [SerializeField] Image[] earnedPointIcons;

    public void DisplayIcons(int[] earnedPoints)
    {
        if (earnedPoints == null) return;

        for(int i = 0; i < earnedPoints.Length; i++)
        {
            if(earnedPoints[i] != 0)
            {
                earnedPointIcons[i].gameObject.SetActive(true);
            }
            else
            {
                earnedPointIcons[i].gameObject.SetActive(false);
            }          
        }
    }
}
