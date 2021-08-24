using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;


public class GameManager : MonoBehaviour
{
    public event EventHandler<int> OnKeyCollected;
    public event EventHandler OnItemCollected;
    public event EventHandler OnLevelCompleted;
    
    public LevelData CurrentLevelData { get; private set; }


    private void Awake()
    {
        BindEvents();
    }

    private void BindEvents()
    {

    }

    private void UnBindEvents()
    {

    }

    private void OnDestroy()
    {
        UnBindEvents();
    }

}


