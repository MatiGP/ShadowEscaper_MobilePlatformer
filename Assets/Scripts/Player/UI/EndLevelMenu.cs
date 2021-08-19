using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndLevelMenu : MonoBehaviour
{
    [SerializeField] GameObject earnedPointsHolder;
    [SerializeField] Image[] pointsImages;
    [SerializeField] LevelLoader levelLoader;
    [SerializeField] GameObject menu;
    [SerializeField] GameObject nextLevelButtonBlocker;
    [SerializeField] GameObject previousLevelButtonBlocker;
    [SerializeField] TextMeshProUGUI requiredTime;
    
    int pointsEarnedOnCurrentLevel;
    bool isOpen;

    private void Start()
    {
        int levelIndex = SceneManager.GetActiveScene().buildIndex - 1;
        int pointsObtained = ShadowRunApp.Instance.SaveSystem.GetObtainedPointsFromLevel(levelIndex);

        pointsEarnedOnCurrentLevel = pointsObtained;
        requiredTime.text += $"{(GameManager.instance.GetRequiredTimeToCompleteLevel() / 60) % 60}:{GameManager.instance.GetRequiredTimeToCompleteLevel() % 60}";
        
        if(SceneManager.GetActiveScene().buildIndex - 1 == 0)
        {
            previousLevelButtonBlocker.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenMenu();
        }
    }

    private void DisplayPlayerProgessOnLevelEnd()
    {
        if (pointsEarnedOnCurrentLevel == 0) return;

        if (!GameManager.instance.PlayerFinishedLevel())
        {           
            nextLevelButtonBlocker.SetActive(true);
            return;
        }
        else
        {         
            pointsImages[0].gameObject.SetActive(true);
        }

        if (GameManager.instance.PlayerFinishedLevelInTime())
        {
            pointsImages[1].gameObject.SetActive(true);
        }

        if (GameManager.instance.PlayerCollectedAllItems())
        {
            pointsImages[2].gameObject.SetActive(true);
        }
    }

    public void ExitLevel()
    {
        levelLoader.LoadLevel(0);
    }
    
    public void RetryCurrentLevel()
    {
        levelLoader.LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel()
    {
        levelLoader.LoadNextLevel();
    }

    public void LoadPreviousLevel()
    {
        levelLoader.LoadPreviousLevel();
    }

    public void OpenMenu()
    {
        if (isOpen)
        {
            menu.SetActive(false);
            isOpen = false;
        }
        else
        {
            menu.SetActive(true);
            isOpen = true;         

            if (!GameManager.instance.PlayerFinishedLevel())
            {
                for(int i = 0; i < pointsEarnedOnCurrentLevel; i++)
                {
                    pointsImages[i].gameObject.SetActive(true);
                }

                nextLevelButtonBlocker.SetActive(false);
            }
        }
    }

    public void LevelFinishMenu(float delay)
    {
        StartCoroutine(LevelFinishMenuDelay(delay));
    }

    IEnumerator LevelFinishMenuDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        menu.SetActive(true);
        isOpen = true;
        DisplayPlayerProgessOnLevelEnd();
    }
}
