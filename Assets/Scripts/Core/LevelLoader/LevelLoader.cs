using Code.UI;
using Code.UI.Panels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public event EventHandler OnLevelLoaded;
    public event EventHandler<LevelData> OnLevelDataLoaded;

    private UILoadingScreen m_UILoadingScreen = null;

    private const string LEVEL_DATA_FORMAT = "Level_{0:00}";
    private const string LEVEL_DATA_PATH = "LevelData/{0}";
    public void LoadLevel(int sceneIndex)
    {  
        m_UILoadingScreen = UIManager.Instance.CreatePanel(EPanelID.LoadLevel) as UILoadingScreen;
        
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    public void LoadMainMenuScene()
    {
        m_UILoadingScreen = UIManager.Instance.CreatePanel(EPanelID.LoadLevel) as UILoadingScreen;

        StartCoroutine(LoadAsynchronously(0));
    }

    public void LoadNextLevel()
    {
        LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadPreviousLevel()
    {
        LoadLevel(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void ReloadLevel()
    {
        m_UILoadingScreen = UIManager.Instance.CreatePanel(EPanelID.LoadLevel) as UILoadingScreen;

        LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {            
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);

        string levelName = string.Format(LEVEL_DATA_FORMAT, sceneIndex);
        string path = string.Format(LEVEL_DATA_PATH, levelName);

        LevelData levelData = Resources.Load<LevelData>(path);
        
        while (!operation.isDone)
        {
            m_UILoadingScreen.SetFill(operation.progress / 0.90f);

            yield return null;
        }

        OnLevelLoaded.Invoke(this, EventArgs.Empty);
        OnLevelDataLoaded.Invoke(this, levelData);
        
        m_UILoadingScreen.ClosePanel();
        
    }



}
