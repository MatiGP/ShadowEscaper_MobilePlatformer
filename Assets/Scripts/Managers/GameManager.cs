using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;
using Code.UI.Panels;
using Code.UI;

namespace Code
{
    public class GameManager
    {
        public event EventHandler OnGameExit;
        public event EventHandler OnGameCompleted;
        public event EventHandler OnGameStart;

        public const int POINTS_MULTIPLIER = 10;
        public int CollectedItemsCount { get; private set; }
        public int CurrentPoints { get; private set; }
        public TimeSpan LevelTime { get; private set; }
        public TimeSpan CurrentLevelTime { get { return DateTime.UtcNow - m_LevelStartingTime; } }
        public LevelData CurrentLevelData { get; private set; }

        private DateTime m_LevelStartingTime = DateTime.MinValue;

        public void GameStart()
        {
            m_LevelStartingTime = DateTime.UtcNow;
            CurrentPoints = 0;
            CollectedItemsCount = 0;
            OnGameStart?.Invoke(this, EventArgs.Empty);
        }

        public void SummarizeLevel()
        {
            LevelTime = DateTime.UtcNow - m_LevelStartingTime;

            CurrentPoints = 1; //For Finishing.
            CurrentPoints += (CollectedItemsCount == CurrentLevelData.ItemsCount) ? 1 : 0;
            CurrentPoints += ((LevelTime.TotalSeconds <= CurrentLevelData.LevelDurationInSeconds) || (LevelTime.TotalSeconds == -1)) ? 1 : 0;
   
            SaveSystem.SaveObtainedStarsFromLevel(CurrentLevelData.LevelName, CurrentPoints);
            
            OnGameCompleted?.Invoke(this, EventArgs.Empty);
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
}


