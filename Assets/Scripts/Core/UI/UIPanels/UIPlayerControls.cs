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
        public event EventHandler OnSlidePressed;
               
        [SerializeField] private Joystick m_Joystick = null;
        [SerializeField] private Button m_JumpButton = null;
        [SerializeField] private Button m_SlideButton = null;
        [SerializeField] private Button m_SettingsButton = null;
    
        public override void BindEvents()
        {
            m_JumpButton.onClick.AddListener(InvokeJumping);
            m_SlideButton.onClick.AddListener(InvokeSliding);
            m_SettingsButton.onClick.AddListener(CreateSettingsPanel);

            m_Joystick.OnHorizontalJoystickMove += OnHorizontalJoystickMove;
        }

        private void OnHorizontalJoystickMove(object sender, float e)
        {
            Debug.Log("JoystickMoved pressed");
            OnJoystickMoved?.Invoke(sender, e);
        }

        private void InvokeJumping()
        {
            Debug.Log("Jumping pressed");
            OnJumpPressed?.Invoke(this, EventArgs.Empty);
            
        }
        
        private void InvokeSliding()
        {
            Debug.Log("Sliding pressed");
            OnSlidePressed?.Invoke(this, EventArgs.Empty);
        }

        private void CreateSettingsPanel()
        {
            UIManager.Instance.CreatePanel(EPanelID.Settings);
        }

        public override void Initialize()
        {
            BindEvents();
        }

        public override void UnBindEvents()
        {
            
        }

        private void OnDestroy()
        {
            UnBindEvents();
        }
    }
}