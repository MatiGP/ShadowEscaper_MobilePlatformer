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
   
    public const int LEVEL_CAP = 25;
    public const int LEVEL_SCENE_STARTING_INDEX = 2;

    public bool CanPlayNextLevel => m_CurrentLevelIndex - LEVEL_SCENE_STARTING_INDEX < m_SceneIndexes.Count-1;
    public bool CanPlayPreviousLevel => m_CurrentLevelIndex > LEVEL_SCENE_STARTING_INDEX;

    private UILoadingScreen m_UILoadingScreen = null;

    public const string LEVEL_TUTORIAL_NAME = "Level_Tutorial";
    public const string LEVEL_NAME_FORMAT = "Level_{0}";
    private const string LEVEL_DATA_PATH = "LevelData/{0}";

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

    public void LoadLevel(int levelNumber)
    {      
        string levelName = GetLevelName(levelNumber);
        StartCoroutine(LoadAsynchronously(levelName));
    }

    public void LoadLevel(string sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    public void LoadNextLevel()
    {       
        UnloadCurrentLevel();
        LoadLevel(m_CurrentLevelIndex + 1);
    }

    public void LoadPreviousLevel()
    {
        UnloadCurrentLevel();
        LoadLevel(m_CurrentLevelIndex - 2);
    }

    public void ReloadLevel()
    {
        UnloadCurrentLevel();
        LoadLevel(m_CurrentLevelIndex-1);
    }

    public void LoadTutorialLevel()
    {
        LoadLevel( LEVEL_TUTORIAL_NAME );
    }

    public void UnloadCurrentLevel()
    {
        string levelName = GetLevelName(m_CurrentLevelIndex-1);
        SceneManager.UnloadSceneAsync(levelName);      
    }

    IEnumerator LoadAsynchronously(string sceneName)
    {
        OnLevelSelected.Invoke(this, sceneName);

        m_UILoadingScreen = UIManager.Instance.CreatePanel(EPanelID.LoadLevel) as UILoadingScreen;

        string path = string.Format(LEVEL_DATA_PATH, sceneName);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);  

        LevelData levelData = Resources.Load<LevelData>(path);
        OnLevelDataLoaded.Invoke(this, levelData);

        m_CurrentLevelIndex = m_SceneIndexes[levelData.LevelName];

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

        for (int i = LEVEL_SCENE_STARTING_INDEX; i < LEVEL_CAP + LEVEL_SCENE_STARTING_INDEX; i++)
        {
            string levelName = GetLevelName(i-1);

            m_SceneIndexes.Add(levelName, i);
        }       
    }

    public int GetLevelIndex(string levelName)
    {
        return m_SceneIndexes.ContainsKey(levelName) ? m_SceneIndexes[levelName] - LEVEL_SCENE_STARTING_INDEX : -1;
    }

    private string GetLevelName(int levelIndex)
    {
        return string.Format(LEVEL_NAME_FORMAT, levelIndex.ToString("D2"));
    }

    public int GetCurrentLevelIndex()
    {
        return m_CurrentLevelIndex;
    }

}
