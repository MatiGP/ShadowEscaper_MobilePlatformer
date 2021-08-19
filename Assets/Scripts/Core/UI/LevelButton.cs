using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public event EventHandler<int> OnLevelPressed;

    [SerializeField] private TextMeshProUGUI m_LevelNum = null;
    [SerializeField] private EarnedPointsDisplayer m_PointDisplayer = null;
    [SerializeField] private Button m_Button = null;

    public Button Button { get => m_Button; }

    private int m_LevelIndex = 0;

    private void Awake()
    {
        BindEvents();
    }

    private void BindEvents()
    {
        m_Button.onClick.AddListener(HandleLevelPressed);
    }

    private void UnBindEvents()
    {
        m_Button.onClick.RemoveListener(HandleLevelPressed);
    }

    private void OnDestroy()
    {
        UnBindEvents();
    }

    private void HandleLevelPressed()
    {
        OnLevelPressed.Invoke(this, m_LevelIndex);
    }

    public void SetUpLevelButtonInfo(int levelNum, int earnedPoints)
    {
        m_LevelIndex = levelNum;
        m_LevelNum.text = levelNum.ToString();
        m_PointDisplayer.SetPointDisplayers(earnedPoints);
    }
}
