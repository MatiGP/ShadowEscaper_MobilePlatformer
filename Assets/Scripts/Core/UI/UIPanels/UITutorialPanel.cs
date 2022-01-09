using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Code.UI.Panels {
    public class UITutorialPanel : UIPanel
    {
        [SerializeField] private RectTransform m_HandTransform = null;

        [Header("Joystick Tweens")]
        [SerializeField] private float m_HandSwayDuration = 2f;
        [SerializeField] private float m_HandSwayDistance = 15f;
        [Header("Button Tweens")]
        [SerializeField] private float m_HandClickDuration = 1f;
        [SerializeField] private Vector2 m_HandClickOffset = Vector2.one;
        [SerializeField] private float m_HandScaleMaxSize = 1.2f;
        [SerializeField] private float m_HandScaleMinSize = 0.7f;

        private UIPlayerControls m_UIPlayerControls = null;
        private Sequence m_PressSequence = null;
        private Sequence m_HandSwaySequence = null;

        private float m_LeftSwayThreshold = 0f;
        private float m_RightSwayThreshold = 0f;

        protected override void Awake()
        {
            base.Awake();

            m_UIPlayerControls = UIManager.Instance.GetPanel(EPanelID.PlayerUI) as UIPlayerControls;
            DisablePlayerUIInputs();

            m_LeftSwayThreshold = m_UIPlayerControls.JoystickTransform.position.x - m_HandSwayDistance;
            m_RightSwayThreshold = m_UIPlayerControls.JoystickTransform.position.x + m_HandSwayDistance;

            BindEvents();
        }

        protected override void Start()
        {
            base.Start();
            m_PressSequence = DOTween.Sequence();
            m_HandSwaySequence = DOTween.Sequence();

            m_PressSequence.Append(m_HandTransform.DOScale(m_HandScaleMaxSize, m_HandClickDuration))
                .Append(m_HandTransform.DOScale(m_HandScaleMinSize, m_HandClickDuration))
                .SetLoops(-1, LoopType.Yoyo);

            m_HandSwaySequence.Append(m_HandTransform.DOMoveX(m_LeftSwayThreshold, m_HandSwayDuration))
                .Append(m_HandTransform.DOMoveX(m_RightSwayThreshold, m_HandSwayDuration))
                .SetLoops(-1, LoopType.Yoyo);
        }

        private void OnDestroy()
        {
            m_PressSequence.Kill();
            m_HandSwaySequence.Kill();

            UnBindEvents();
        }

        public void SetUpHand(ETutorialPhase tutorialPhase)
        {
            switch (tutorialPhase)
            {
                case ETutorialPhase.JoystickMovement:

                    break;
                case ETutorialPhase.Jumping:

                    break;
                case ETutorialPhase.Sliding:

                    break;
            }

            m_HandTransform.gameObject.SetActive(true);
        }

        private void DisablePlayerUIInputs()
        {
            m_UIPlayerControls.JoystickTransform.gameObject.SetActive(false);
            m_UIPlayerControls.JumpButtonTransform.gameObject.SetActive(false);
            m_UIPlayerControls.SlideButtonTransform.gameObject.SetActive(false);
        }

        public override void BindEvents()
        {
            m_UIPlayerControls.OnJoystickMoved += HandleJoystickMoved;
            m_UIPlayerControls.OnJumpPressed += HandleJumpPressed;
            m_UIPlayerControls.OnSlidePressed += HandleSlidePressed;
        }

        public override void UnBindEvents()
        {
            m_UIPlayerControls.OnJoystickMoved -= HandleJoystickMoved;
            m_UIPlayerControls.OnJumpPressed -= HandleJumpPressed;
            m_UIPlayerControls.OnSlidePressed -= HandleSlidePressed;
        }    

        private void HandleJoystickMoved(object sender, float e)
        {
            if (e > 0.2)
            {
                m_HandTransform.gameObject.SetActive(false);
            }
        }

        private void HandleSlidePressed(object sender, System.EventArgs e)
        {
            m_HandTransform.gameObject.SetActive(false);
        }

        private void HandleJumpPressed(object sender, System.EventArgs e)
        {
            m_HandTransform.gameObject.SetActive(false);
        }


    }

    public enum ETutorialPhase
    {
        None,
        JoystickMovement,
        Jumping,
        Sliding
    }
}
