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
    int levelIndex = 0;

    private void Start()
    {
        LoadButtonTexts();
        SetButtonLevelText();
        EnableLevelButtons();
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
        for(int i = 0; i < worlds.Length; i++)
        {
            Button[] levelButton = worlds[i].GetComponentsInChildren<Button>();

            for(int j = 0; j < levelButton.Length; j++) 
            {
                int[] earnedPoints = saveSystem.GetObtainedPointsFromLevel(levelIndex);

                if (j == 0)
                {
                    levelButton[j].interactable = true;
                }
                else
                {                  
                    if (earnedPoints.Length > 0)
                    {
                        levelButton[j].interactable = true;
                        earnedPointsDisplayers[j].DisplayIcons(earnedPoints);
                    }
                    else
                    {
                        
                        levelButton[j].interactable = true;
                        break;
                    }
                }

                levelIndex++;
            }
        }

        worlds[0].transform.GetChild(0).GetComponent<Button>().interactable = true;
        if(saveSystem.GetObtainedPointsFromLevel(0) != null)
            earnedPointsDisplayers[0].DisplayIcons(saveSystem.GetObtainedPointsFromLevel(0));
    }

    void SetButtonLevelText()
    {
        for(int i = 0; i < buttonTexts.Count; i++)
        {
            buttonTexts[i].text = "" + (i + 1);
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
