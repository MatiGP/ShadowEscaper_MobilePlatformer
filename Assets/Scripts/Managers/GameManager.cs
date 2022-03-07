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
    public event EventHandler OnGameCompleted;
    public event EventHandler OnGameStart;

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
        OnGameStart?.Invoke(this, EventArgs.Empty);
    }

    public void SummarizeLevel()
    {
        LevelTime = DateTime.UtcNow - LevelStartingDate;

        CurrentPoints = 1; //For Finishing.
        CurrentPoints += (CollectedItemsCount == CurrentLevelData.ItemsCount) ? 1 : 0;
        CurrentPoints += ((LevelTime.TotalSeconds <= CurrentLevelData.LevelDurationInSeconds) || (LevelTime.TotalSeconds == -1)) ? 1 : 0;
        
        if(CurrentLevelData.LevelIndex > 0)
        {
            SaveSystem.SaveObtainedPointsFromLevel(CurrentLevelData.LevelIndex - 1, CurrentPoints);
        }
        
        OnGameCompleted.Invoke(this, EventArgs.Empty);
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

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1f;
    }

}


