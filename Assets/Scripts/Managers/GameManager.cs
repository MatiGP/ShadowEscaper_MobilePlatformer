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
    public event EventHandler OnGameExit;

    public int CollectedItemsCount { get; private set; }
    public int CurrentPoints { get; private set; }
    public TimeSpan LevelTime { get; private set; }
    public LevelData CurrentLevelData { get; private set; }

    private DateTime LevelStartingDate = DateTime.MinValue; 

    public void GameStart()
    {      
        LevelStartingDate = DateTime.UtcNow;
        CurrentPoints = 0;
        CollectedItemsCount = 0;
    }

    public void SummarizeLevel()
    {
        LevelTime = DateTime.UtcNow - LevelStartingDate;

        CurrentPoints = 1; //For Finishing.
        CurrentPoints += (CollectedItemsCount == CurrentLevelData.ItemsCount) ? 1 : 0;
        CurrentPoints += (LevelTime.TotalSeconds <= CurrentLevelData.LevelDurationInSeconds) ? 1 : 0;

        SaveSystem.SaveObtainedPointsFromLevel(CurrentLevelData.LevelIndex - 1, CurrentPoints);
    }

    public void SetCollectedItemsCount(int count)
    {
        CollectedItemsCount = count;
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


