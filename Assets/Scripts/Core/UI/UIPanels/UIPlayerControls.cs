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
        public event EventHandler<int> OnKeyCollected;
               
        [SerializeField] private Joystick m_Joystick = null;
        [SerializeField] private Button m_JumpButton = null;
        [SerializeField] private Button m_SlideButton = null;
        [SerializeField] private Button m_SettingsButton = null;

        [SerializeField] private Image[] m_CollectedKeysIcon = null;

        private Color KEY_COLLECTED_COLOR = Color.white;

        public override void Initialize()
        {
            BindEvents();
        }

        public override void BindEvents()
        {
            m_JumpButton.onClick.AddListener(InvokeJumping);
            m_SlideButton.onClick.AddListener(InvokeSliding);
            m_SettingsButton.onClick.AddListener(CreateSettingsPanel);

            m_Joystick.OnHorizontalJoystickMove += OnHorizontalJoystickMove;

            OnKeyCollected += UpdateCollectedKeysUI;
        }

        private void UpdateCollectedKeysUI(object sender, int keyIndex)
        {
            m_CollectedKeysIcon[keyIndex].color = KEY_COLLECTED_COLOR;
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

            OnKeyCollected -= UpdateCollectedKeysUI;
        }

        private void OnDestroy()
        {
            UnBindEvents();
        }
    }
}