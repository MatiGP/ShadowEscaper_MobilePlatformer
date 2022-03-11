using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Code.UI.Panels;
using Code.UI;
using System;

public class PlayerController : CharacterController
{
    public const float JOYSTICK_DEADZONE = 0.15f;   
    [Header("Health")]
    [SerializeField] private PlayerHealth m_PlayerHealth = null;
   
    private UIPlayerControls m_UIPlayerControls = null;

    protected override void Awake()
    {
        base.Awake();

        m_UIPlayerControls = UIManager.Instance.CreatePanel(EPanelID.PlayerUI) as UIPlayerControls;

        BindEvents();
    }

    protected override void SetUpStates()
    {
        base.SetUpStates();
        stateMachine.Initialize(MovementStates[EMovementStateType.Idle]);
    }

    private void BindEvents()
    {
        m_UIPlayerControls.OnJoystickMoved += ReadMoveInput;
        m_UIPlayerControls.OnJumpPressed += Jump;
        m_UIPlayerControls.OnJumpInterupted += HandleJumpInterupted;
        m_PlayerHealth.OnDamageTaken += Die;
        m_UIPlayerControls.OnSlidePressed += HandleGroundSlide;
        m_UIPlayerControls.OnSlideInterupted += HandleSlideInterupted;      
    }

    private void UnBindEvents()
    {
        m_UIPlayerControls.OnJoystickMoved -= ReadMoveInput;
        m_UIPlayerControls.OnJumpPressed -= Jump;
        m_UIPlayerControls.OnJumpInterupted -= HandleJumpInterupted;
        m_PlayerHealth.OnDamageTaken -= Die;
        m_UIPlayerControls.OnSlidePressed -= HandleGroundSlide;
        m_UIPlayerControls.OnSlideInterupted -= HandleSlideInterupted;
    }

    private void OnDestroy()
    {
        UnBindEvents();
    }

    private void HandleSlideInterupted(object sender, EventArgs e)
    {
        if (m_IsSliding)
        {
            m_IsSliding = false;
        }       
    }

    private void HandleJumpInterupted(object sender, EventArgs e)
    {
        InterruptJumping();
    }

    private void HandleGroundSlide(object sender, EventArgs e)
    {
        GroundSlide();
    }

    private void Jump(object sender, EventArgs e)
    {
        Jump(); 
    }

    private void ReadMoveInput(object sender, float newDirection)
    {
        if(newDirection >= JOYSTICK_DEADZONE)
        {
            direction = 1;
            m_PersistantParticles.transform.localScale = new Vector3(1, 1, 1);
        }
        else if(newDirection <= -JOYSTICK_DEADZONE)
        {
            direction = -1;
            m_PersistantParticles.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            direction = 0;
        }             
    }

    public void Die(object sender, EventArgs e)
    {
        stateMachine.ChangeState(MovementStates[EMovementStateType.Death]);
    }   

    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(groundDetectorTransform.position, groundDetectorSize);
        Gizmos.color = Color.green;
        Gizmos.DrawCube(wallDetectorRight.position, wallDetectorSize);
        Gizmos.color = Color.green;
        Gizmos.DrawCube(wallDetectorLeft.position, wallDetectorSize);
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(ceilingDetectorTransform.position, ceilingDetectorSize);
        Gizmos.color = Color.magenta;
        Gizmos.DrawCube(m_SlideDetectorPosition.position, m_SlideDetectorSize);
        
    }

    protected override void Update()
    {
        base.Update();

        float keyboardDirection = Input.GetAxisRaw("Horizontal");

        if (keyboardDirection > 0)
        {
            direction = 1;
            m_PersistantParticles.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (keyboardDirection < 0)
        {
            direction = -1;
            m_PersistantParticles.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            direction = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (IsJumping && Input.GetKeyUp(KeyCode.Space))
        {
            InterruptJumping();
        }
      
        if (Input.GetKeyDown(KeyCode.K))
        {
            GroundSlide();
        }

        if(IsSliding && Input.GetKeyUp(KeyCode.K))
        {
            InterruptSliding();
        }
    }

#endif
}
