using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;
using Code.UI.Panels;
using Code.UI;

public class GameManager
{
    public event EventHandler OnGameStarted;
    public event EventHandler OnKeyCollected;
    public event EventHandler OnItemCollected;

    public LevelData CurrentLevelData { get; private set; }

    

   
}


