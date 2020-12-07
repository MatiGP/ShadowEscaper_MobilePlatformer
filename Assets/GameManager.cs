using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] int numOfKeysInLevel = 0;
    [SerializeField] int numOfItemsToCollect = 1;
    [SerializeField] float timeToCompleteLevel;
    [SerializeField] SaveSystem saveSystem;
    [SerializeField] UnityEvent OnLevelCompleted;

    int numOfCollectedItemsByPlayer  = 0;
    int pointsToGainOnLevelCompletion;
    float timePlayerFinishedLevel;
    bool playerFinishedLevel;

    private void Awake()
    {
        instance = this;
    }

    public void CollectItem()
    {
        numOfCollectedItemsByPlayer += 1;
        SoundManager.instance.PlaySoundEffect(SoundType.Coin_Collect);
    }

    public void FinishLevel(float timeOfFinishingLevel)
    {
        timePlayerFinishedLevel = timeOfFinishingLevel;

        SummarizeLevel();

        int completedLevelNum = SceneManager.GetActiveScene().buildIndex - 1;

        saveSystem.CompleteLevel(completedLevelNum, pointsToGainOnLevelCompletion);
        

    }

    void SummarizeLevel()
    {
        playerFinishedLevel = true;
        pointsToGainOnLevelCompletion += 1; // For finishing;

        if (PlayerCollectedAllItems())
        {
            pointsToGainOnLevelCompletion += 1;
        }

        if (PlayerFinishedLevelInTime())
        {
            pointsToGainOnLevelCompletion += 1;
        }

        OnLevelCompleted.Invoke();
    }

    public bool PlayerFinishedLevelInTime()
    {
        return timePlayerFinishedLevel <= timeToCompleteLevel;       
    }

    public bool PlayerCollectedAllItems()
    {
        return numOfCollectedItemsByPlayer == numOfItemsToCollect;
    }

    public bool PlayerFinishedLevel()
    {
        return playerFinishedLevel;
    }

    public int GetNumOfKeysInLevel()
    {
        return numOfKeysInLevel;
    }
}
