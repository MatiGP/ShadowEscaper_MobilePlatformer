using Code.StateMachine;
using Code.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code
{
    public class ShadowRunApp : MonoBehaviour
    {
        public static ShadowRunApp Instance = null;

        public LevelLoader LevelLoader { get => m_LevelLoader; }
        public SoundManager SoundManager { get => m_SoundManager; }
        public GameManager GameManager { get => m_GameManager; }


        [SerializeField] private UIManager m_UIManager = null;
        [SerializeField] private LevelLoader m_LevelLoader = null;
        [SerializeField] private SoundManager m_SoundManager = null;

        private GameManager m_GameManager = null;
        private MenuStateMachine m_StateMachine = null;

        private EMenuState m_StartingState = EMenuState.MainMenu;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            m_StateMachine = new MenuStateMachine();
            m_StateMachine.Initialize();

            m_StateMachine.AddState(new MainMenuState());
            m_StateMachine.AddState(new GameState());
            m_StateMachine.AddState(new TutorialGameState());

            InitializeOtherSystems();
            LoadSettings();

            m_SoundManager.PauseGameplayMusic();

            BindEvents();
        }

        private void Start()
        {
            m_StateMachine.StartStateMachine(EMenuState.MainMenu);
        }

        private void Update()
        {
            m_StateMachine.UpdateState();
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
            SaveSystem.LoadPlayerProgress();
        }

        private void BindEvents()
        {
            m_LevelLoader.OnLevelLoaded += HandleLevelLoaded;
            m_LevelLoader.OnLevelSelected += HandleLevelSelected;
            m_GameManager.OnGameExit += HandleGameExit;


            m_LevelLoader.OnLevelDataLoaded += HandleLevelDataLoaded;
        }

        private void HandleLevelSelected(object sender, string loadedLevelName)
        {
            if (loadedLevelName == LevelLoader.LEVEL_TUTORIAL_NAME)
            {
                m_StateMachine.ChangeState(EMenuState.Tutorial);
                return;
            }

            m_StateMachine.ChangeState(EMenuState.Game);
        }

        private void HandleLevelDataLoaded(object sender, LevelData levelData)
        {
            m_GameManager.SetLevelData(levelData);
        }

        private void HandleGameExit(object sender, System.EventArgs e)
        {
            m_StateMachine.ChangeState(EMenuState.MainMenu);
            m_SoundManager.PauseGameplayMusic();
        }

        private void HandleLevelLoaded(object sender, System.EventArgs e)
        {
            m_SoundManager.PauseMainMenuMusic();
        }

        private void UnBindEvents()
        {
            m_LevelLoader.OnLevelLoaded -= HandleLevelLoaded;
            m_GameManager.OnGameExit -= HandleGameExit;
            m_LevelLoader.OnLevelSelected -= HandleLevelSelected;

            m_LevelLoader.OnLevelDataLoaded -= HandleLevelDataLoaded;
        }

        private void OnDestroy()
        {
            UnBindEvents();
        }
    }
}
