using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_LevelNum = null;
    [SerializeField] private EarnedPointsDisplayer m_PointDisplayer = null;
    
    public void SetUp(int levelNum, int earnedPoints)
    {
        m_LevelNum.text = levelNum.ToString();
        m_PointDisplayer.SetPointDisplayers(earnedPoints);
    }
}
