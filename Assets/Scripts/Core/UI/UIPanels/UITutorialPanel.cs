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
        [SerializeField] private Vector2 m_HandSwayOffset = Vector2.one;
        [Header("Button Tweens")]
        [SerializeField] private float m_HandClickDuration = 1f;
        [SerializeField] private Vector2 m_HandClickOffset = Vector2.one;
        [SerializeField] private float m_HandScaleMaxSize = 1.2f;
        [SerializeField] private float m_HandScaleMinSize = 0.7f;

        private UIPlayerControls m_UIPlayerControls = null;
        private Sequence m_HandClickSequence = null;
        private Sequence m_HandSwaySequence = null;

        private float m_LeftSwayThreshold = 0f;
        private float m_RightSwayThreshold = 0f;

        #region MonoBehaviour
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
            m_HandClickSequence = DOTween.Sequence();
            m_HandSwaySequence = DOTween.Sequence();

            m_HandClickSequence.Append(m_HandTransform.DOScale(m_HandScaleMaxSize, m_HandClickDuration))
                .Append(m_HandTransform.DOScale(m_HandScaleMinSize, m_HandClickDuration))
                .SetLoops(-1, LoopType.Yoyo);

            m_HandSwaySequence.Append(m_HandTransform.DOMoveX(m_LeftSwayThreshold, m_HandSwayDuration))
                .Append(m_HandTransform.DOMoveX(m_RightSwayThreshold, m_HandSwayDuration))
                .SetLoops(-1, LoopType.Yoyo);
        }

        private void OnDestroy()
        {
            if (m_HandClickSequence != null)
            {
                m_HandClickSequence.Kill();
            }

            if (m_HandSwaySequence != null)
            {
                m_HandSwaySequence.Kill();
            }
            
            UnBindEvents();
        }
        #endregion

        public void SetUpHand(ETutorialPhase tutorialPhase)
        {
            switch (tutorialPhase)
            {
                case ETutorialPhase.JoystickMovement:
                    m_HandTransform.localPosition = (Vector2)m_UIPlayerControls.JoystickTransform.position + m_HandSwayOffset;
                    m_HandSwaySequence.Play();                  
                    break;
                case ETutorialPhase.Jumping:
                    m_HandTransform.localPosition = (Vector2)m_UIPlayerControls.JumpButtonTransform.position + m_HandClickOffset;
                    m_HandClickSequence.Play();
                    break;
                case ETutorialPhase.Sliding:
                    m_HandTransform.localPosition = (Vector2)m_UIPlayerControls.SlideButtonTransform.position + m_HandClickOffset;
                    m_HandClickSequence.Play();
                    break;
            }

            m_HandTransform.gameObject.SetActive(true);
        }

        public void SetControlsEnabled(ETutorialPhase tutorialPhase, bool enabled)
        {
            switch (tutorialPhase)
            {
                case ETutorialPhase.JoystickMovement:
                    m_UIPlayerControls.JoystickTransform.gameObject.SetActive(enabled);
                    break;
                case ETutorialPhase.Jumping:
                    m_UIPlayerControls.JumpButtonTransform.gameObject.SetActive(enabled);
                    break;
                case ETutorialPhase.Sliding:
                    m_UIPlayerControls.SlideButtonTransform.gameObject.SetActive(enabled);
                    break;
            }
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

        private void HandleJoystickMoved(object sender, float e)
        {
            if (e > PlayerController.JOYSTICK_DEADZONE)
            {
                m_HandTransform.gameObject.SetActive(false);
                
                m_UIPlayerControls.OnJoystickMoved -= HandleJoystickMoved;
                m_HandSwaySequence.Kill();                           
            }
        }

        private void HandleSlidePressed(object sender, System.EventArgs e)
        {
            m_HandTransform.gameObject.SetActive(false);
            m_UIPlayerControls.OnSlidePressed -= HandleSlidePressed;
            m_HandClickSequence.Kill();
        }

        private void HandleJumpPressed(object sender, System.EventArgs e)
        {
            m_HandTransform.gameObject.SetActive(false);
            m_UIPlayerControls.OnJumpPressed -= HandleJumpPressed;
            m_HandClickSequence.Pause();
        }    

        public override void UnBindEvents()
        {
            
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
