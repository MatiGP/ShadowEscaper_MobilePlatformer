using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Code.UI.Panels
{
    public class EndLevelMenu : UIPanel
    {
        [SerializeField] private Image[] m_PointsImages;
        [SerializeField] private GameObject m_NextLevelButtonBlocker;
        [SerializeField] private GameObject m_PreviousLevelButtonBlocker;
        [SerializeField] private TextMeshProUGUI m_RequiredTimeToGetReward;

        [SerializeField] private Button m_PreviousLevelButton = null;
        [SerializeField] private Button m_NextLevelButton = null;
        [SerializeField] private Button m_MenuButton = null;
        [SerializeField] private Button m_ShopButton = null;
        [SerializeField] private Button m_SettingsButton = null;

        private const string FINISH_BEFORE_FORMAT = "Finished Before {0}";
        private const string TIME_FORMAT = "{0:00}:{1:00}:{2:00}";
        private const string ITEMS_COLLECTED_FORMAT = "Items collected{0}:{1}";

        int pointsEarnedOnCurrentLevel;
        bool isOpen;

        protected override void Start()
        {
            int levelIndex = SceneManager.GetActiveScene().buildIndex - 1;
            int pointsObtained = ShadowRunApp.Instance.SaveSystem.GetObtainedPointsFromLevel(levelIndex);

            pointsEarnedOnCurrentLevel = pointsObtained;
            //requiredTime.text += $"{(GameManager.instance.GetRequiredTimeToCompleteLevel() / 60) % 60}:{GameManager.instance.GetRequiredTimeToCompleteLevel() % 60}";

            if (levelIndex - 1 == 0)
            {
                m_PreviousLevelButtonBlocker.SetActive(true);
            }
        }

        private void DisplayPlayerProgessOnLevelEnd()
        {

        }

        public void OpenMenu()
        {           
                /*
                if (!GameManager.instance.PlayerFinishedLevel())
                {
                    for(int i = 0; i < pointsEarnedOnCurrentLevel; i++)
                    {
                        pointsImages[i].gameObject.SetActive(true);
                    }

                    nextLevelButtonBlocker.SetActive(false);
                }
                */
            
        }
     
        IEnumerator LevelFinishMenuDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            isOpen = true;
            DisplayPlayerProgessOnLevelEnd();
        }

        public override void Initialize()
        {
            BindEvents();
        }

        public override void BindEvents()
        {
            m_PreviousLevelButton.onClick.AddListener(HandlePreviousLevelPressed);
            m_NextLevelButton.onClick.AddListener(HandleNextLevelPressed);
            m_MenuButton.onClick.AddListener();
            m_ShopButton.onClick.AddListener();
            m_SettingsButton.onClick.AddListener();
        }

        public override void UnBindEvents()
        {
            m_PreviousLevelButton.onClick.RemoveListener(HandlePreviousLevelPressed);
        }

        private void HandlePreviousLevelPressed()
        {
            ShadowRunApp.Instance.LevelLoader.LoadPreviousLevel();
        }

        private void HandleNextLevelPressed()
        {
            ShadowRunApp.Instance.LevelLoader.LoadNextLevel();
        }

        private void HandleMenuPressed()
        {

        }

        private void OnDestroy()
        {
            UnBindEvents();
        }
    }
}
