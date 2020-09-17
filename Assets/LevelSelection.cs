using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] RectTransform levelPanel;
    [SerializeField] GameObject nextButton;
    [SerializeField] GameObject prevButton;

    int currentIndex;

    private void Start()
    {
        
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

    

    private void OnEnable()
    {
        currentIndex = 0;
        nextButton.SetActive(true);
        prevButton.SetActive(false);
        levelPanel.anchoredPosition = new Vector2(0f, 0f);

    }
}
