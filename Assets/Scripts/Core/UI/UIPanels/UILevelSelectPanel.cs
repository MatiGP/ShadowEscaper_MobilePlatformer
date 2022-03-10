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

        protected override void Awake()
        {
            base.Awake();
            BindEvents();
        }

        protected override void Start()
        {
            base.Start();
            m_FadeTween = m_CanvasGroup.DOFade(1, m_FadeDuration).Play();

            PrepareLevelPage();        
        }
        private void OnDestroy()
        {

        }


        public override void ClosePanel()
        {
            m_FadeTween = m_CanvasGroup.DOFade(0, m_FadeDuration).OnComplete(() => base.ClosePanel()).Play();            
        } 
        
        public override void UnBindEvents()
        {
            
        }

        private void PrepareLevelPage()
        {
            int startingLevelNum = LEVEL_BUTTONS_ON_EACH_PAGE_COUNT * m_LevelPageNum;

            for (int i = startingLevelNum; i < LEVEL_BUTTONS_ON_EACH_PAGE_COUNT; i++)
            {
                int levelNum = i + 1;

                Debug.Log($"Setting leven number : {levelNum}");

                int earnedPoints = SaveSystem.GetObtainedPointsFromLevel(i);
                m_LevelButtons[i].SetUpLevelButtonInfo(levelNum, earnedPoints);
                m_LevelButtons[i].OnLevelPressed += HandleLevelPressed;                
            }
        }

        private void HandleLevelPressed(object sender, int levelIndex)
        {
            ShadowRunApp.Instance.LevelLoader.LoadLevel(levelIndex);
        }

   
        public override void BindEvents()
        {
            
        }
    }
}
