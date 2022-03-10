using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Code.UI.Panels
{
    public class UIEndLevelMenu : UIPanel
    {
        [SerializeField] private Image[] m_StarImages = null;
        
        [SerializeField] private GameObject m_NextLevelButtonBlocker = null;
        [SerializeField] private GameObject m_PreviousLevelButtonBlocker = null;
        
        [SerializeField] private TextMeshProUGUI m_RequiredTimeToGetReward = null;
        [SerializeField] private TextMeshProUGUI m_ItemsCollected = null;

        [SerializeField] private Button m_PreviousLevelButton = null;
        [SerializeField] private Button m_NextLevelButton = null;
        [SerializeField] private Button m_MenuButton = null;
        [SerializeField] private Button m_ShopButton = null;
        [SerializeField] private Button m_SettingsButton = null;

        private const string FINISH_BEFORE_FORMAT = "Finished Before \n {0}";
        private const string TIME_FORMAT = "{0:00}:{1:00}:{2:00}";
        private const string ITEMS_COLLECTED_FORMAT = "Items collected \n {0}/{1}";

        protected override void Awake()
        {
            base.Awake();

            BindEvents();
        }

        protected override void Start()
        {
            bool canPlayNextLevel = ShadowRunApp.Instance.LevelLoader.CanPlayNextLevel;
            bool canPlayPrevLevel = ShadowRunApp.Instance.LevelLoader.CanPlayPreviousLevel;
            int pointsObtained = ShadowRunApp.Instance.GameManager.CurrentPoints;

            SetFinishedBeforeText();
            SetCollectedItemsText();
            
            m_PreviousLevelButtonBlocker.SetActive(canPlayPrevLevel);
            m_NextLevelButtonBlocker.SetActive(canPlayNextLevel);

            for(int i = 0; i < pointsObtained; i++)
            {
                m_StarImages[i].gameObject.SetActive(true);
            }

        }

        private void SetCollectedItemsText()
        {
            int itemsCollected = ShadowRunApp.Instance.GameManager.CollectedItemsCount;
            int maxItems = ShadowRunApp.Instance.GameManager.CurrentLevelData.ItemsCount;

            string itemsFormat = string.Format(ITEMS_COLLECTED_FORMAT, itemsCollected, maxItems);
            m_ItemsCollected.text = itemsFormat;
        }

        private void SetFinishedBeforeText()
        {
            TimeSpan time = ShadowRunApp.Instance.GameManager.LevelTime;

            string timeString = string.Format(TIME_FORMAT, time.Minutes, time.Seconds, time.Milliseconds);

            m_RequiredTimeToGetReward.text = string.Format(FINISH_BEFORE_FORMAT, timeString);
        }

        public override void BindEvents()
        {
            m_PreviousLevelButton.onClick.AddListener(HandlePreviousLevelPressed);
            m_NextLevelButton.onClick.AddListener(HandleNextLevelPressed);
            m_MenuButton.onClick.AddListener(HandleMenuPressed);
            m_ShopButton.onClick.AddListener(HandleShopPressed);
            m_SettingsButton.onClick.AddListener(HandleSettingsPressed);
        }

        public override void UnBindEvents()
        {
            m_PreviousLevelButton.onClick.RemoveListener(HandlePreviousLevelPressed);
            m_NextLevelButton.onClick.RemoveListener(HandleNextLevelPressed);
            m_MenuButton.onClick.RemoveListener(HandleMenuPressed);
            m_ShopButton.onClick.RemoveListener(HandleShopPressed);
            m_SettingsButton.onClick.RemoveListener(HandleSettingsPressed);
        }

        private void HandlePreviousLevelPressed()
        {
            ClosePanel();
            ShadowRunApp.Instance.LevelLoader.LoadPreviousLevel();
        }

        private void HandleNextLevelPressed()
        {
            ClosePanel();
            ShadowRunApp.Instance.LevelLoader.LoadNextLevel();
        }

        private void HandleMenuPressed()
        {
            ClosePanel();
            ShadowRunApp.Instance.GameManager.InvokeOnGameExit();
        }

        private void HandleShopPressed()
        {

        }

        private void HandleSettingsPressed()
        {
            
        }

        private void OnDestroy()
        {
            UnBindEvents();
        }
    }
}
