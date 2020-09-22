using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] int numOfItemsToCollect = 1;
    [SerializeField] float timeToCompleteLevel;
    [SerializeField] SaveSystem saveSystem;

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
    }

    public void FinishLevel(float timeOfFinishingLevel)
    {
        timePlayerFinishedLevel = timeOfFinishingLevel;

        SummarizeLevel();
        saveSystem.CompleteLevel(SceneManager.GetActiveScene().buildIndex - 1, pointsToGainOnLevelCompletion);
        SceneManager.LoadScene(0);

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
}
