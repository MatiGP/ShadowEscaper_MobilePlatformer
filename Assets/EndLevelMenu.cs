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
    [SerializeField] GameObject nextLevelButton;

    bool isOpen;

    private void DisplayPlayerProgressOnLevel()
    {
        if (!GameManager.instance.PlayerFinishedLevel())
        {
            earnedPointsHolder.SetActive(false);
            nextLevelButton.SetActive(false);
            return;
        }
        else
        {
            earnedPointsHolder.SetActive(true);
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
            DisplayPlayerProgressOnLevel();
        }
    }
}
