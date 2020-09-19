using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] int itemsToCollect = 1;
    [SerializeField] float timeToCompleteLevel;
    [SerializeField] SaveSystem saveSystem;

    int collectedItems;
    int pointsToGain;
    float timePlayerFinishedLevel;

    private void Awake()
    {
        instance = this;
    }

    public void CollectItem()
    {
        collectedItems += 1;
    }

    public void FinishLevel(float timeOfFinishingLevel)
    {
        timePlayerFinishedLevel = timeOfFinishingLevel;

        SummarizeLevel();
        saveSystem.CompleteLevel(SceneManager.GetActiveScene().buildIndex - 1, pointsToGain);
        SceneManager.LoadScene(0);

    }

    void SummarizeLevel()
    {
        pointsToGain += 1; // For finishing;

        if (itemsToCollect == collectedItems)
        {
            pointsToGain += 1;
        }

        if(timePlayerFinishedLevel <= timeToCompleteLevel)
        {
            pointsToGain += 1;
        }
    }
}
