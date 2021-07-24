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
        [SerializeField] private float m_FadeDuration = 0.8f;

        public override void BindEvents()
        {
           
        }

        public override void ClosePanel()
        {
            m_FadeTween = m_CanvasGroup.DOFade(0, m_FadeDuration).OnComplete(() =>
            {
                UIManager.Instance.ClosePanel(EPanelID.LevelSelection);
            });
        }

    

        public override void Initialize()
        {
            m_FadeTween = m_CanvasGroup.DOFade(1, m_FadeDuration);

            PrepareLevelPage();
        }

       

        public override void UnBindEvents()
        {
            
        }

        private void PrepareLevelPage()
        {
            int startingLevelNum = LEVEL_BUTTONS_ON_EACH_PAGE_COUNT * m_LevelPageNum;

            for (int i = 0; i < LEVEL_BUTTONS_ON_EACH_PAGE_COUNT; i++)
            {
                m_LevelButtons[i].SetUp(startingLevelNum + i + 1, 0);
            }
        }

        public void InvokeLevelSelect(int levelIndex)
        {
            OnLevelSelected.Invoke(this, levelIndex);
        }

        private void OnDestroy()
        {
            UnBindEvents();
        }
    }
}
