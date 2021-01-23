using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] GameObject levelLoaderPanel;
    [SerializeField] Image loadingImage;

    public void LoadLevel(int sceneIndex)
    {
        levelLoaderPanel.SetActive(true);
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    public void LoadNextLevel()
    {
        levelLoaderPanel.SetActive(true);
        StartCoroutine(LoadAsynchronously(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadPreviousLevel()
    {
        levelLoaderPanel.SetActive(true);
        StartCoroutine(LoadAsynchronously(SceneManager.GetActiveScene().buildIndex - 1));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {      
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        

        while (!operation.isDone)
        {
            loadingImage.fillAmount = operation.progress / 0.9f;

            yield return null;
        }
    }
}
