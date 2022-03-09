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
    public event EventHandler<string> OnLevelSelected;
    public event EventHandler<LevelData> OnLevelDataLoaded;
    
    private UILoadingScreen m_UILoadingScreen = null;

    private const string LEVEL_NAME_FORMAT = "Level_{0}";
    private const string LEVEL_DATA_PATH = "LevelData/{0}";
    public const string LEVEL_TUTORIAL_NAME = "Level_Tutorial";

    public const int LEVEL_CAP = 25;
    private int m_CurrentLevelIndex = -1;

    private Dictionary<string, int> m_SceneIndexes = new Dictionary<string, int>();
    

    private void Awake()
    {
        SceneManager.sceneUnloaded += HandleSceneUnloaded;

        PrepareSceneIndexes();
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
        string levelNumber = sceneIndex < 10 ? $"0{sceneIndex}" : sceneIndex.ToString();
        StartCoroutine(LoadAsynchronously(levelNumber));
    }

    public void LoadLevel(string sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    public void LoadMainMenuScene()
    {
        StartCoroutine(LoadAsynchronously("0"));
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

    IEnumerator LoadAsynchronously(string sceneIndex)
    {
        OnLevelSelected.Invoke(this, sceneIndex);

        m_UILoadingScreen = UIManager.Instance.CreatePanel(EPanelID.LoadLevel) as UILoadingScreen;

        string levelName = string.Format(LEVEL_NAME_FORMAT, sceneIndex);
        string path = string.Format(LEVEL_DATA_PATH, levelName);

        AsyncOperation operation = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);  

        LevelData levelData = Resources.Load<LevelData>(path);
        OnLevelDataLoaded.Invoke(this, levelData);

        m_CurrentLevelIndex = m_SceneIndexes[levelData.LevelName];

        Debug.Log($"Current level index: {m_CurrentLevelIndex}");

        while (!operation.isDone)
        {
            m_UILoadingScreen.SetFill(operation.progress / 0.90f);

            yield return null;
        }

        OnLevelLoaded.Invoke(this, EventArgs.Empty);              
        m_UILoadingScreen.ClosePanel();      
    }

    private void PrepareSceneIndexes()
    {
        m_SceneIndexes.Add(LEVEL_TUTORIAL_NAME, 1);

        for (int i = 2; i <= LEVEL_CAP+2; i++)
        {
            string levelName = string.Format(LEVEL_NAME_FORMAT, i.ToString("D2"));
            m_SceneIndexes.Add(levelName, i);
        }    
    }

    public int GetLevelIndex(string levelName)
    {
        return m_SceneIndexes.ContainsKey(levelName) ? m_SceneIndexes[levelName]-1 : -1;
    }

}
