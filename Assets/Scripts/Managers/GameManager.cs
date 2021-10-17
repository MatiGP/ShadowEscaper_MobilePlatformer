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
    public event EventHandler OnGameStarted;
    public event EventHandler OnItemCollected;
    public event EventHandler OnGameCompleted;
    public event EventHandler OnGameExit;
    public int CollectedItemsCount { get; private set; }
    public int CurrentPoints { get; private set; }
    public TimeSpan LevelTime { get; set; }
    public LevelData CurrentLevelData { get; private set; }

    private DateTime LevelStartingDate = DateTime.MinValue;

    public GameManager()
    {
        OnGameStarted += HandleGameStarted;
        OnItemCollected += HandleItemCollected;
        OnGameCompleted += SummarizeLevel;
    }

    private void HandleItemCollected(object sender, EventArgs e)
    {
        ResetLevel();
    }

    private void HandleGameStarted(object sender, EventArgs e)
    {
        LevelStartingDate = DateTime.UtcNow;
    }

    private void SummarizeLevel(object sender, EventArgs e)
    {
        LevelTime = LevelStartingDate - DateTime.UtcNow;

        CurrentPoints = 1; //For Finishing.
        CurrentPoints += CollectedItemsCount == CurrentLevelData.ItemsCount ? 1 : 0;
        CurrentPoints += LevelTime <= CurrentLevelData.LevelDuration ? 1 : 0;       
    }

    private void ResetLevel()
    {
        CurrentPoints = 0;
        LevelTime = TimeSpan.Zero;
    }

    public void SetLevelData(LevelData levelData)
    {
        CurrentLevelData = levelData;
    }
    
    public void InvokeOnGameStarted()
    {
        OnGameStarted.Invoke(this, EventArgs.Empty);
    }

    public void InvokeOnItemCollected()
    {
        OnItemCollected.Invoke(this, EventArgs.Empty);
    }

    public void InvokeOnGameExit()
    {
        OnGameExit.Invoke(this, EventArgs.Empty);
    }

    public void InvokeOnGameCompleted()
    {
        OnGameCompleted.Invoke(this, EventArgs.Empty);
    }



   
}


