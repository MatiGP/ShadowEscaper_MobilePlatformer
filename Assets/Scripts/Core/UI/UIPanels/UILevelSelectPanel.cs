using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Panels
{
    public class UILevelSelectPanel : UIPanel
    {
        public event EventHandler<int> OnLevelSelected;
        public event EventHandler<int> OnNextLevelPagePressed;

        [SerializeField] private CanvasGroup m_CanvasGroup = null;

        [SerializeField] private Button m_NextPage = null;
        [SerializeField] private Button m_PreviousPage = null;

        [SerializeField] private LevelButton[] m_LevelButtons = null;

        private int m_LevelPageNum = 0;
        private const int LEVEL_BUTTONS_ON_EACH_PAGE_COUNT = 25;


        [Header("Tween settings")]
        private Tween m_FadeTween = null;
        [SerializeField] private float m_FadeDuration = 0.5f;

        public override void BindEvents()
        {
            OnLevelSelected += HandleLevelSelected;
        }

        private void HandleLevelSelected(object sender, int levelIndex)
        {
            ShadowRunApp.Instance.LevelLoader.LoadLevel(levelIndex);                
        }

        public override void ClosePanel()
        {
            m_FadeTween = m_CanvasGroup.DOFade(0, m_FadeDuration).OnComplete(() => base.ClosePanel());            
        }

    
        public override void Initialize()
        {
            m_FadeTween = m_CanvasGroup.DOFade(1, m_FadeDuration);

            PrepareLevelPage();

            BindEvents();
        }
        
        public override void UnBindEvents()
        {
            OnLevelSelected -= HandleLevelSelected;
        }

        private void PrepareLevelPage()
        {
            int startingLevelNum = LEVEL_BUTTONS_ON_EACH_PAGE_COUNT * m_LevelPageNum;

            for (int i = 0; i < LEVEL_BUTTONS_ON_EACH_PAGE_COUNT; i++)
            {
                int levelNum = startingLevelNum + i + 1;
                int earnedPoints = ShadowRunApp.Instance.SaveSystem.GetObtainedPointsFromLevel(i);
                m_LevelButtons[i].SetUpLevelButtonInfo(levelNum, earnedPoints);
                m_LevelButtons[i].OnLevelPressed += HandleLevelPressed;
                
            }
        }

        private void HandleLevelPressed(object sender, int levelIndex)
        {
            OnLevelSelected.Invoke(this, levelIndex);
        }

        private void OnDestroy()
        {
            UnBindEvents();
        }
    }
}
