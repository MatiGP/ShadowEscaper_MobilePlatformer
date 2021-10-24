using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;
using Code.UI.Panels;
using Code.UI;

public class GameManager
{
    public event EventHandler OnItemCollected;
    public event EventHandler OnGameExit;
    public int CollectedItemsCount { get; private set; }
    public int CurrentPoints { get; private set; }
    public TimeSpan LevelTime { get; set; }
    public LevelData CurrentLevelData { get; private set; }

    private DateTime LevelStartingDate = DateTime.MinValue; 

    public void GameStart()
    {      
        LevelStartingDate = DateTime.UtcNow;
        CurrentPoints = 0;
    }

    public void SummarizeLevel()
    {
        LevelTime = DateTime.UtcNow - LevelStartingDate;

        CurrentPoints = 1; //For Finishing.
        CurrentPoints += CollectedItemsCount == CurrentLevelData.ItemsCount ? 1 : 0;
        CurrentPoints += LevelTime <= CurrentLevelData.LevelDuration ? 1 : 0;

        ShadowRunApp.Instance.SaveSystem.SaveLevelProgress(CurrentPoints, CurrentLevelData.LevelIndex-1);
    }
   
    public void SetLevelData(LevelData levelData)
    {
        CurrentLevelData = levelData;
    }

    public void InvokeOnGameExit()
    {
        OnGameExit.Invoke(this, EventArgs.Empty);
    }


   
}


