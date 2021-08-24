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
    public SaveSystem SaveSystem { get => m_SaveSystem; }
    public GameManager GameManager { get => m_GameManager; }


    [SerializeField] private EMenuState m_StartingState;
    [SerializeField] private UIManager m_UIManager = null;
    [SerializeField] private LevelLoader m_LevelLoader = null;
    [SerializeField] private SoundManager m_SoundManager = null;
    [SerializeField] private GameManager m_GameManager = null;
    
    private MenuStateMachine m_StateMachnie = null;
    private SaveSystem m_SaveSystem = null;
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

        BindEvents();

        InitializeOtherSystems();
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
        m_SaveSystem = new SaveSystem();
        m_SaveSystem.Initialize();

        m_UIManager.Initialize();
        
    }

    private void BindEvents()
    {
        m_LevelLoader.OnLevelLoaded += HandleLevelLoaded;
    }

    private void HandleLevelLoaded(object sender, System.EventArgs e)
    {
        m_StateMachnie.ChangeState(EMenuState.Game);
    }

    private void UnBindEvents()
    {
        m_LevelLoader.OnLevelLoaded -= HandleLevelLoaded;
    }

    private void OnDestroy()
    {
        UnBindEvents();
    }
}
