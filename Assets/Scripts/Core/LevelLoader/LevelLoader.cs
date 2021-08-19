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

    private UILoadingScreen m_UILoadingScreen = null;
    public void LoadLevel(int sceneIndex)
    {  
        OnLevelLoaded.Invoke(this, EventArgs.Empty);
        m_UILoadingScreen = UIManager.Instance.CreatePanel(EPanelID.LoadLevel) as UILoadingScreen;
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadAsynchronously(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadPreviousLevel()
    {
        StartCoroutine(LoadAsynchronously(SceneManager.GetActiveScene().buildIndex - 1));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {            
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
        
        while (!operation.isDone)
        {
            m_UILoadingScreen.SetFill(operation.progress / 0.90f);

            yield return null;
        }

        m_UILoadingScreen.ClosePanel();
        
    }  
}
