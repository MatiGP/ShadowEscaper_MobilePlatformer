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
               
        [SerializeField] private Joystick m_Joystick = null;
        [SerializeField] private UIButton m_JumpButton = null;
        [SerializeField] private UIButton m_SlideButton = null;
        [SerializeField] private Button m_SettingsButton = null;

        public override void Initialize()
        {
            BindEvents();
        }

        public override void BindEvents()
        {
            
            m_SlideButton.OnButtonUp.AddListener(CancelSlide);
            m_SlideButton.OnButtonDown.AddListener(InvokeSliding);

            m_SettingsButton.onClick.AddListener(CreateSettingsPanel);

            m_JumpButton.OnButtonUp.AddListener(CancelJump);
            m_JumpButton.OnButtonDown.AddListener(InvokeJumping);


            m_Joystick.OnHorizontalJoystickMove += OnHorizontalJoystickMove;
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
            Debug.LogWarning("Canceling jump");

            OnJumpInterupted?.Invoke(this, EventArgs.Empty);
        }

        private void CancelSlide()
        {
            Debug.LogWarning("Canceling slide");

            OnSlideInterupted?.Invoke(this, EventArgs.Empty);
        }

        private void CreateSettingsPanel()
        {
            UIManager.Instance.CreatePanel(EPanelID.Settings);
        }    

        public override void UnBindEvents()
        {
            m_JumpButton.onClick.RemoveListener(InvokeJumping);
            m_SlideButton.onClick.RemoveListener(InvokeSliding);
            m_SettingsButton.onClick.RemoveListener(CreateSettingsPanel);

            m_Joystick.OnHorizontalJoystickMove -= OnHorizontalJoystickMove;          
        }

        private void OnDestroy()
        {
            UnBindEvents();
        }
    }
}