using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Panels
{
    public class UIPlayerControls : UIPanel
    {
        public event EventHandler<float> OnJoystickMoved;
        public event EventHandler OnJumpPressed;
        public event EventHandler OnJumpInterupted;
        public event EventHandler OnSlidePressed;
        public event EventHandler OnSlideInterupted;

        public Transform JoystickTransform { get => m_Joystick.transform; }
        public Transform JumpButtonTransform { get => m_JumpButton.transform; }
        public Transform SlideButtonTransform { get => m_SlideButton.transform; }

        [SerializeField] private CanvasGroup m_CanvasGroup = null;
        [SerializeField] private Joystick m_Joystick = null;
        [SerializeField] private UIButton m_JumpButton = null;
        [SerializeField] private UIButton m_SlideButton = null;
        [SerializeField] private Button m_SettingsButton = null;
        
      
        protected override void Awake()
        {
            base.Awake();
            BindEvents();
            
        }

       
        public void SetReceiveInput(bool value)
        {
            m_CanvasGroup.interactable = value;
            OnJoystickMoved.Invoke(this, 0f);
        }

        private void BindEvents()
        {
            m_SlideButton.OnButtonUp.AddListener(CancelSlide);
            m_SlideButton.OnButtonDown.AddListener(InvokeSliding);

            m_SettingsButton.onClick.AddListener(CreateSettingsPanel);

            m_JumpButton.OnButtonUp.AddListener(CancelJump);
            m_JumpButton.OnButtonDown.AddListener(InvokeJumping);

            m_Joystick.OnHorizontalJoystickMove += OnHorizontalJoystickMove;
           
        }
        private void UnBindEvents()
        {
            m_SlideButton.OnButtonUp.RemoveListener(CancelSlide);
            m_SlideButton.OnButtonDown.RemoveListener(InvokeSliding);

            m_SettingsButton.onClick.RemoveListener(CreateSettingsPanel);

            m_JumpButton.OnButtonUp.RemoveListener(CancelJump);
            m_JumpButton.OnButtonDown.RemoveListener(InvokeJumping);

            m_Joystick.OnHorizontalJoystickMove -= OnHorizontalJoystickMove;
        }

        private void OnHorizontalJoystickMove(object sender, float e)
        {
            OnJoystickMoved?.Invoke(sender, e);
        }

        private void InvokeJumping()
        {
            OnJumpPressed?.Invoke(this, EventArgs.Empty);           
        }
        
        private void InvokeSliding()
        {
            OnSlidePressed?.Invoke(this, EventArgs.Empty);
        }

        private void CancelJump()
        {
            OnJumpInterupted?.Invoke(this, EventArgs.Empty);
        }

        private void CancelSlide()
        { 
            OnSlideInterupted?.Invoke(this, EventArgs.Empty);
        }

        private void CreateSettingsPanel()
        {
            UIManager.Instance.CreatePanel(EPanelID.InGameSettings);
        }    

        private void OnDestroy()
        {
            UnBindEvents();
        }
    }
}