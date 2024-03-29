﻿using System;
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
        [SerializeField] private UICounter m_EarnedPointsCounter = null;

        [SerializeField] private Image[] m_StarImages = null;
        
        [SerializeField] private GameObject m_PremiumRoot = null;
        
        [SerializeField] private TextMeshProUGUI m_RequiredTimeToGetReward = null;
        [SerializeField] private TextMeshProUGUI m_ItemsCollected = null;

        [SerializeField] private Button m_PreviousLevelButton = null;
        [SerializeField] private Button m_NextLevelButton = null;
        [SerializeField] private Button m_MenuButton = null;

        [SerializeField] private Sprite m_GoldStarIcon = null;

        private const string FINISH_BEFORE_FORMAT = "Finished Before\n{0}";
        private const string TIME_FORMAT = "{0:00}:{1:00}:{2:00}";
        private const string ITEMS_COLLECTED_FORMAT = "Items collected\n{0}/{1}";
        private const int DEFAULT_ITEMS_COLLECTED = 0;

        private int m_ObtainedPoints = 0;

        private Coroutine m_DelayedCountUp = null;
        private WaitForSeconds m_CoroutineDelay = new WaitForSeconds(COUNT_UP_DELAY);

        private const float COUNT_UP_DELAY = 0.7f;

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

            m_EarnedPointsCounter.SetValues(0, m_ObtainedPoints);
            m_ObtainedPoints = pointsObtained * GameManager.POINTS_MULTIPLIER;

            SetFinishedBeforeText();
            SetCollectedItemsText();

            m_PreviousLevelButton.interactable = canPlayPrevLevel;
            m_NextLevelButton.interactable = canPlayNextLevel;

            for(int i = 0; i < pointsObtained; i++)
            {
                m_StarImages[i].sprite = m_GoldStarIcon;
            }

            int levelIndex = ShadowRunApp.Instance.LevelLoader.GetCurrentLevelIndex();
            int previousPoints = SaveSystem.GetObtainedPointsFromLevel(levelIndex);

            if (previousPoints == 0)
            {
                SaveSystem.AddPremiumCurrencyCount(m_ObtainedPoints);
                m_PremiumRoot.SetActive(true);
                m_DelayedCountUp = StartCoroutine(CountUp());
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

        private void BindEvents()
        {
            m_PreviousLevelButton.onClick.AddListener(HandlePreviousLevelPressed);
            m_NextLevelButton.onClick.AddListener(HandleNextLevelPressed);
            m_MenuButton.onClick.AddListener(HandleMenuPressed);
        }

        private void UnBindEvents()
        {
            m_PreviousLevelButton.onClick.RemoveListener(HandlePreviousLevelPressed);
            m_NextLevelButton.onClick.RemoveListener(HandleNextLevelPressed);
            m_MenuButton.onClick.RemoveListener(HandleMenuPressed);
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

        private IEnumerator CountUp()
        {
            yield return m_CoroutineDelay;
            m_EarnedPointsCounter.StartCountUp();        
        }

        private void OnDestroy()
        {
            if(m_DelayedCountUp != null)
            {
                StopCoroutine(m_DelayedCountUp);
                m_DelayedCountUp = null;
            }

            UnBindEvents();
        }
    }
}
