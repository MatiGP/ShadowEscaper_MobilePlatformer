using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] RectTransform levelPanel;
    [SerializeField] GameObject nextButton;
    [SerializeField] GameObject prevButton;
    [SerializeField] SaveSystem saveSystem;
    [SerializeField] List<TextMeshProUGUI> buttonTexts;
    [SerializeField] GameObject[] worlds;
    [SerializeField] List<EarnedPointsDisplayer> earnedPointsDisplayers;
    int currentIndex;
    int[] points;

    private void Start()
    {
        LoadButtonTexts();
        SetButtonLevelText();
        EnableLevelButtons();
        EnableEarnedPoints();

    }

    private void LoadButtonTexts()
    {
        for (int i = 0; i < worlds.Length; i++)
        {
            GameObject world = worlds[i];
            foreach (TextMeshProUGUI text in world.GetComponentsInChildren<TextMeshProUGUI>())
            {
                buttonTexts.Add(text);
            }
            foreach(EarnedPointsDisplayer earnedPointsDisplayer in world.GetComponentsInChildren<EarnedPointsDisplayer>())
            {
                earnedPointsDisplayers.Add(earnedPointsDisplayer);
            }
        }
    }

    public void NextWorldPage()
    {
        currentIndex++;

        if(currentIndex == levelPanel.transform.childCount - 1)
        {
            nextButton.SetActive(false);
        }
        prevButton.SetActive(true);

        MoveLevelPanel(backwards: false);

        
    }

    public void PreviousWorldPage()
    {
        currentIndex--;

        if (currentIndex == 0)
        {
            prevButton.SetActive(false);
        }
        nextButton.SetActive(true);

        MoveLevelPanel(backwards: true);

        
    }

    void MoveLevelPanel(bool backwards)
    {
        if (backwards)
        {
            levelPanel.anchoredPosition = new Vector2(levelPanel.anchoredPosition.x + 1900, levelPanel.anchoredPosition.y);
        }
        else
        {
            levelPanel.anchoredPosition = new Vector2(levelPanel.anchoredPosition.x - 1900, levelPanel.anchoredPosition.y);
        }       
    }

    void EnableLevelButtons()
    {
        int levelIndex = 0;

        for(int i = 0; i < worlds.Length; i++)
        {
            foreach (Button levelButton in worlds[i].GetComponentsInChildren<Button>())
            {

                if (levelIndex == 0)
                {
                    levelButton.interactable = true;
                }
                else
                {
                    if (points[levelIndex] != 0)
                    {
                        levelButton.interactable = true;
                    }
                    else
                    {
                        levelButton.interactable = false;
                    }
                }
                
                levelIndex++;
            }
        }

        worlds[0].transform.GetChild(0).GetComponent<Button>().interactable = true;
    }

    void SetButtonLevelText()
    {
        points = saveSystem.GetObtainedPointsFromEachLevel();

        for(int i = 0; i < points.Length; i++)
        {
            buttonTexts[i].text = (i+1).ToString();
        }
    }

    void EnableEarnedPoints()
    {
        points = saveSystem.GetObtainedPointsFromEachLevel();

        for(int i = 0; i < points.Length; i++)
        {
            if (i > earnedPointsDisplayers.Count - 1) break;

            earnedPointsDisplayers[i].DisplayIcons(points[i]);
        }
    }

    private void OnEnable()
    {
        currentIndex = 0;
        nextButton.SetActive(true);
        prevButton.SetActive(false);
        levelPanel.anchoredPosition = new Vector2(0f, 0f);
    }
}
