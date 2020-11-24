using System.Collections;
using System.Collections.Generic;
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
    int pointsEarnedOnCurrentLevel;
    bool isOpen;

    private void Start()
    {
        pointsEarnedOnCurrentLevel = SaveSystem.instance.GetObtainedPointsFromEachLevel()[SceneManager.GetActiveScene().buildIndex-1];
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
