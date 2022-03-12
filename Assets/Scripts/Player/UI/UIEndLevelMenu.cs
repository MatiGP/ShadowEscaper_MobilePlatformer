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
        [SerializeField] private TextMeshProUGUI m_EarnedPoints = null;

        [SerializeField] private Button m_PreviousLevelButton = null;
        [SerializeField] private Button m_NextLevelButton = null;
        [SerializeField] private Button m_MenuButton = null;

        [SerializeField] private float m_PointCountLerpDuration = 0.5f;
        [SerializeField] private float m_Delay = 1f;

        private const string FINISH_BEFORE_FORMAT = "Finished Before \n {0}";
        private const string TIME_FORMAT = "{0:00}:{1:00}:{2:00}";
        private const string ITEMS_COLLECTED_FORMAT = "Items collected \n {0}/{1}";

        private int m_CurrentPoints = 0;
        private int m_ObtainedPoints = 0;

        private Coroutine m_DelayedCountUp = null;

        private bool m_ShouldLerp = false;

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

            m_ObtainedPoints = pointsObtained * GameManager.POINTS_MULTIPLIER;

            SetFinishedBeforeText();
            SetCollectedItemsText();
            
            m_PreviousLevelButtonBlocker.SetActive(canPlayPrevLevel);
            m_NextLevelButtonBlocker.SetActive(canPlayNextLevel);

            for(int i = 0; i < pointsObtained; i++)
            {
                m_StarImages[i].gameObject.SetActive(true);
            }

            int levelIndex = ShadowRunApp.Instance.LevelLoader.GetCurrentLevelIndex();
            int previousPoints = SaveSystem.GetObtainedPointsFromLevel(levelIndex);

            if (previousPoints == 0)
            {
                SaveSystem.AddPremiumCurrencyCount(m_ObtainedPoints);
            }
            
            SaveSystem.SaveObtainedStarsFromLevel(levelIndex, pointsObtained);
            m_DelayedCountUp = StartCoroutine(CountUp());
        }

        protected override void Update()
        {
            base.Update();

            if (m_ShouldLerp)
            {
                m_CurrentPoints = (int)Mathf.Lerp(m_CurrentPoints, m_ObtainedPoints, m_PointCountLerpDuration * Time.deltaTime);
                m_EarnedPoints.text = m_CurrentPoints.ToString();
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
        }

        public override void UnBindEvents()
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
            yield return m_Delay;
            m_ShouldLerp = true;
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
