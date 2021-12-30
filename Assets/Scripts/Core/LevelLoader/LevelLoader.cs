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
    public event EventHandler OnLevelSelected;
    public event EventHandler<LevelData> OnLevelDataLoaded;
    
    private UILoadingScreen m_UILoadingScreen = null;

    private const string LEVEL_DATA_FORMAT = "Level_{0:00}";
    private const string LEVEL_DATA_PATH = "LevelData/{0}";

    public const int LEVEL_CAP = 25;

    private int m_CurrentLevelIndex = -1;

    private void Awake()
    {
        SceneManager.sceneUnloaded += HandleSceneUnloaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneUnloaded -= HandleSceneUnloaded;
    }

    private void HandleSceneUnloaded(Scene arg0)
    {
        Resources.UnloadUnusedAssets();
    }

    public void LoadLevel(int sceneIndex)
    {         
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    public void LoadMainMenuScene()
    {
        StartCoroutine(LoadAsynchronously(0));
    }

    public void LoadNextLevel()
    {       
        UnloadCurrentLevel();
        LoadLevel(m_CurrentLevelIndex + 1);
    }

    public void LoadPreviousLevel()
    {
        UnloadCurrentLevel();
        LoadLevel(m_CurrentLevelIndex - 1);
    }

    public void ReloadLevel()
    {
        UnloadCurrentLevel();
        LoadLevel(m_CurrentLevelIndex);
    }

    private void UnloadCurrentLevel()
    {
        SceneManager.UnloadSceneAsync(m_CurrentLevelIndex);      
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        OnLevelSelected.Invoke(this, EventArgs.Empty);

        m_UILoadingScreen = UIManager.Instance.CreatePanel(EPanelID.LoadLevel) as UILoadingScreen;

        string levelName = string.Format(LEVEL_DATA_FORMAT, sceneIndex);
        string path = string.Format(LEVEL_DATA_PATH, levelName);

        AsyncOperation operation = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);  

        LevelData levelData = Resources.Load<LevelData>(path);
        OnLevelDataLoaded.Invoke(this, levelData);

        m_CurrentLevelIndex = levelData.LevelIndex;

        while (!operation.isDone)
        {
            m_UILoadingScreen.SetFill(operation.progress / 0.90f);

            yield return null;
        }

        OnLevelLoaded.Invoke(this, EventArgs.Empty);              
        m_UILoadingScreen.ClosePanel();      
    }
}
