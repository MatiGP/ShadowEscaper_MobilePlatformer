using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   

public class EarnedPointsDisplayer : MonoBehaviour
{
    [SerializeField] private Image[] pointDisplayers = new Image[3];

    public void SetPointDisplayers(int numOfPoints)
    {
        for(int i = 0; i < numOfPoints; i++)
        {
            pointDisplayers[i].gameObject.SetActive(true);
        }
    }
    
}
