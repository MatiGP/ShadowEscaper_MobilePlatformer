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
    
    enum Rewards { COMPLETION, IN_TIME, COLLECTED_ITEM }

    int numOfCollectedItemsByPlayer  = 0;
    int[] pointsToGainOnLevelCompletion = new int[3];
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

      
        

    }

    void SummarizeLevel()
    {
        playerFinishedLevel = true;
        pointsToGainOnLevelCompletion[(int)Rewards.COMPLETION] += 1; // For finishing;

        if (PlayerCollectedAllItems())
        {
            pointsToGainOnLevelCompletion[(int)Rewards.COLLECTED_ITEM] += 1;
        }

        if (PlayerFinishedLevelInTime())
        {
            pointsToGainOnLevelCompletion[(int)Rewards.IN_TIME] += 1;
        }

        
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

    public float GetRequiredTimeToCompleteLevel()
    {
        return timeToCompleteLevel;
    }
}


