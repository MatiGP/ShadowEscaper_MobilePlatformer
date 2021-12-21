using Code.StateMachine;
using Code.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowRunApp : MonoBehaviour
{
    public static ShadowRunApp Instance = null;
    
    public LevelLoader LevelLoader { get => m_LevelLoader; }
    public SoundManager SoundManager { get => m_SoundManager; }
    public GameManager GameManager { get => m_GameManager; }

    [SerializeField] private EMenuState m_StartingState;
    [SerializeField] private UIManager m_UIManager = null;
    [SerializeField] private LevelLoader m_LevelLoader = null;
    [SerializeField] private SoundManager m_SoundManager = null;
       
    private GameManager m_GameManager = null;   
    private MenuStateMachine m_StateMachnie = null;
    
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        m_StateMachnie = new MenuStateMachine();
        m_StateMachnie.Initialize();
        m_StateMachnie.AddState(new MainMenuState());
        m_StateMachnie.AddState(new GameState());
        
        InitializeOtherSystems();
        LoadSettings();

        m_SoundManager.PauseGameplayMusic();
        
        BindEvents(); 
    }

    private void Start()
    {
        m_StateMachnie.StartStateMachine(m_StartingState);
    }

    private void Update()
    {
        m_StateMachnie.UpdateState();
    }

    private void InitializeOtherSystems()
    {
        m_UIManager.Initialize();
        m_SoundManager.Initialize();

        m_GameManager = new GameManager();

    }

    private void LoadSettings()
    {
        m_SoundManager.ApplySoundFXVolume();
        m_SoundManager.ApplyMusicVolume();
        Application.targetFrameRate = SaveSystem.GetTargetFramerate();
    }

    private void BindEvents()
    {
        m_LevelLoader.OnLevelLoaded += HandleLevelLoaded;
        m_GameManager.OnGameExit += HandleGameExit;



        m_LevelLoader.OnLevelDataLoaded += HandleLevelDataLoaded;
    }

    private void HandleLevelDataLoaded(object sender, LevelData levelData)
    {
        m_GameManager.SetLevelData(levelData);
    }

    private void HandleGameExit(object sender, System.EventArgs e)
    {
        m_StateMachnie.ChangeState(EMenuState.MainMenu);       
        m_SoundManager.PauseGameplayMusic();
    }

    private void HandleLevelLoaded(object sender, System.EventArgs e)
    {
        m_StateMachnie.ChangeState(EMenuState.Game);       
        m_SoundManager.PauseMainMenuMusic();
    }

    private void UnBindEvents()
    {
        m_LevelLoader.OnLevelLoaded -= HandleLevelLoaded;
        m_GameManager.OnGameExit -= HandleGameExit;

        m_LevelLoader.OnLevelDataLoaded -= HandleLevelDataLoaded;
    }

    private void OnDestroy()
    {
        UnBindEvents();
    }
}
