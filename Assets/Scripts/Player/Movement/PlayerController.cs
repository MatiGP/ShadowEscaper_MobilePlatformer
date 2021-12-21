using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Code.UI.Panels;
using Code.UI;
using System;

public class PlayerController : MonoBehaviour
{
    public float Direction { get => direction; }
    public float FootSpeed { get => footSpeed; }
    public float Gravity { get => gravity; }
    public float WallJumpGravity { get => wallJumpGravity; }
    public float JumpHeight { get => jumpHeight; }
    public float WallJumpHeight { get => wallJumpHeight; }
    public float WallSlideSpeed { get => wallslideSpeed; }
    public float JumpOffWallForce { get => jumpOffWallForce; }
    public float FallMultiplier { get => fallMultiplier; }
    public float RemainingJumpForce { get => remainingJumpForce; }
    public float WallJumpDuration { get => wallJumpDuration; }
    public float FallingSpeedLimit { get => fallingSpeedLimit; }

    public float SlideSpeed { get => m_SlideSpeed; }
    public float SlideSpeedFallOff { get => m_SlideSpeedFallOff; }  
    public float SlideCollisionOffset { get => m_SlideCollisionOffset; }

    public bool IsTouchingGround { get => isGrounded; }
    public bool IsJumping { get => isJumping; }
    public bool IsTouchingRightWall { get => isTouchingRightWall; }
    public bool IsTouchingLeftWall { get => isTouchingLeftWall; }
    public bool IsFacingRight { get => facingRight; }
    public bool IsTouchingCeiling { get => isTouchingCeiling; }
    public bool IsSliding { get => m_IsSliding; }
    public bool IsTouchingWallWhileSliding { get => m_IsTouchingWallWhileSliding; }

    public Vector2 SlideCollisionSize { get => m_SlideCollisionSize; }
    public Vector2 NormalCollisionSize { get => m_NormalCollisionSize; }
    public Vector2 SlideDetectorSize { get => m_SlideDetectorSize; }

    [SerializeField] private CapsuleCollider2D m_CapsuleCollider;
    [SerializeField] private Vector2 m_NormalCollisionSize = new Vector2(1.53f, 4f);
    [Header("Ground Movement")]
    [SerializeField] private float footSpeed;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundDetectorTransform;
    [SerializeField] private Vector2 groundDetectorSize;
    [SerializeField] private float groundDetectionLineLength;
    [Header("Sliding")]
    [SerializeField] private float m_SlideSpeed = 36f;
    [SerializeField] private float m_SlideSpeedFallOff = 1f;
    [SerializeField] private float m_SlideCooldown = 2f;
    [SerializeField] private Vector2 m_SlideCollisionSize = new Vector2(4f, 1.53f);
    [SerializeField] private float m_SlideCollisionOffset = -1.6f;
    [SerializeField] private Vector2 m_SlideDetectorSize;
    [SerializeField] private Transform m_SlideDetectorPosition;

    [Space(1f)]
    [Header("Wall Detectors")]
    [SerializeField] private Transform wallDetectorRight;
    [SerializeField] private Transform wallDetectorLeft;
    [SerializeField] private float wallDetectorLineLength;
    [SerializeField] private Vector2 wallDetectorSize;
    [SerializeField] private LayerMask wallLayer;
    [Space(1f)]
    [Header("Vertical Movement")]
    [SerializeField] private float jumpHeight;
    [SerializeField] private float timeToJumpApex;
    [SerializeField] private float fallingSpeedLimit;
    [SerializeField] private float normalJumpMultiplier;
    [SerializeField] private float lowJumpMultiplier;
    [SerializeField] private float maxFallSpeed;
    [SerializeField] private float wallslideSpeed;
    [SerializeField] private float wallJumpHeight;
    [SerializeField] private float jumpOffWallForce;
    
    
    [Range(0.001f, 1f)]
    [SerializeField] private float wallJumpDuration;

    [Space(1f)]
    [Header("Ceiling Detection")]
    [SerializeField] private Vector2 ceilingDetectorSize;
    [SerializeField] private Transform ceilingDetectorTransform;
    [SerializeField] private LayerMask ceilingLayer;
    [Space(1f)]
    [Header("Player Model")]
    [SerializeField] private SpriteRenderer playerCharacterSR;
    [SerializeField] private Animator playerAnimator;

    [Header("Health")]
    [SerializeField] private PlayerHealth m_PlayerHealth = null;

    private float gravity;
    private float wallJumpGravity;

    private float direction;

    private float fallMultiplier;
    private float remainingJumpForce;
    private float slideCooldown;

    private bool isGrounded;
    private bool isJumping;
    private bool isTouchingRightWall;
    private bool isTouchingLeftWall;
    private bool facingRight;
    private bool isTouchingCeiling;
    private bool m_IsSliding;
    private bool m_IsTouchingWallWhileSliding;

    private StateMachine stateMachine;

    public IdleState IdleState { get; private set; }
    public RunningState RunningState { get; private set; }
    public JumpingState JumpingState { get; private set; }
    public FallingState FallingState { get; private set; }
    public GroundslidingState GroundSlidingState { get; private set; }
    public WallslidingState WallSlidingState { get; private set; }
    public WalljumpingState WallJumpingState { get; private set; }
    public DeathState DeathState { get; private set; }

    private UIPlayerControls m_UIPlayerControls = null;

    private void Awake()
    {      
        SetUpStates();

        fallMultiplier = normalJumpMultiplier;

        m_UIPlayerControls = UIManager.Instance.CreatePanel(EPanelID.PlayerUI) as UIPlayerControls;

        BindEvents();
    }

    private void SetUpStates()
    {
        stateMachine = new StateMachine();

        IdleState = new IdleState(this, stateMachine, playerAnimator);
        RunningState = new RunningState(this, stateMachine, playerAnimator);
        JumpingState = new JumpingState(this, stateMachine, playerAnimator);
        FallingState = new FallingState(this, stateMachine, playerAnimator);
        GroundSlidingState = new GroundslidingState(this, stateMachine, playerAnimator);
        WallSlidingState = new WallslidingState(this, stateMachine, playerAnimator);
        WallJumpingState = new WalljumpingState(this, stateMachine, playerAnimator);
        DeathState = new DeathState(this, stateMachine, playerAnimator);

        stateMachine.Initialize(IdleState);
    }

    void Start()
    {
        CalculateGravity();
    }

    void Update()
    {
        stateMachine.CurrentState.HandleAnimator();

        TickSlideCooldown();
    }

    private void LateUpdate()
    {
        stateMachine.CurrentState.HandleInput();
    }

    private void FixedUpdate()
    {
        isTouchingRightWall = 
            Physics2D.OverlapBox(wallDetectorRight.position, wallDetectorSize, 0f, wallLayer);
        
        isTouchingLeftWall = 
            Physics2D.OverlapBox(wallDetectorLeft.position, wallDetectorSize, 0f, wallLayer);
        
        isGrounded = 
            Physics2D.OverlapBox(groundDetectorTransform.position, groundDetectorSize, 0f, groundLayer);
        
        isTouchingCeiling =
            Physics2D.OverlapBox(ceilingDetectorTransform.position, ceilingDetectorSize, 0f, ceilingLayer);

        if (m_IsSliding)
        {
            m_IsTouchingWallWhileSliding =
                Physics2D.OverlapBox(m_SlideDetectorPosition.position, m_SlideDetectorSize, 0f, wallLayer);
        }

        stateMachine.CurrentState.HandleLogic();

    }

    private void BindEvents()
    {
        m_UIPlayerControls.OnJoystickMoved += ReadMoveInput;
        m_UIPlayerControls.OnJumpPressed += Jump;
        m_UIPlayerControls.OnJumpInterupted += HandleJumpInterupted;
        m_PlayerHealth.OnDamageTaken += Die;
        m_UIPlayerControls.OnSlidePressed += GroundSlide;
        m_UIPlayerControls.OnSlideInterupted += HandleSlideInterupted;      
    }

    private void UnBindEvents()
    {
        m_UIPlayerControls.OnJoystickMoved -= ReadMoveInput;
        m_UIPlayerControls.OnJumpPressed -= Jump;
        m_UIPlayerControls.OnJumpInterupted -= HandleJumpInterupted;
        m_PlayerHealth.OnDamageTaken -= Die;
        m_UIPlayerControls.OnSlidePressed -= GroundSlide;
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

    private void GroundSlide(object sender, EventArgs e)
    {
        if(slideCooldown <= 0)
        {
            m_IsSliding = true;
        }        
    }

    private void Jump(object sender, EventArgs e)
    {
        if (!isJumping)
        {
            isJumping = true;
        }       
    }

    public void InterruptJumping()
    {       
        isJumping = false;
    }

    public void InterruptSliding()
    {
        m_IsSliding = false;
    }

    public void ReadMoveInput(object sender, float newDirection)
    {
        if(newDirection > 0)
        {
            direction = 1;
        }
        else if(newDirection < 0)
        {
            direction = -1;
        }
        else
        {
            direction = 0;
        }             
    }

    public void ClearDirection()
    {
        direction = 0;
    }

    public void Flip()
    {
        if (direction > 0)
        {
            playerCharacterSR.flipX = false;
            facingRight = true;
        }
        else if (direction < 0)
        {
            playerCharacterSR.flipX = true;
            facingRight = false;
        }
    }

    public void AdjustPlayerDirection()
    {
        direction = IsTouchingLeftWall ? -1 : 1;
        Flip();
    }

    public void Die(object sender, EventArgs e)
    {
        stateMachine.ChangeState(DeathState);
    }   

    public void FixPlayerGroundPosition()
    {

        Collider2D floorDetector = Physics2D.OverlapBox(groundDetectorTransform.position, groundDetectorSize, 0f, groundLayer);
        
        float closestFloorPos = 0f;

        if (floorDetector)
        {
            closestFloorPos = floorDetector.ClosestPoint(transform.position).y;
            transform.position = new Vector3(transform.position.x, m_CapsuleCollider.size.y / 2 + closestFloorPos);
        }

        
    }

    public void FixPlayerWallPosition()
    {
        Collider2D rightWallDetector = Physics2D.OverlapBox(wallDetectorRight.position, wallDetectorSize, 0f, wallLayer);
        Collider2D leftWallDetector = Physics2D.OverlapBox(wallDetectorLeft.position, wallDetectorSize, 0f, wallLayer);

        float closestRightWallXPos = 0f;
        float closestLeftWallXPos = 0f;

        if (rightWallDetector)
        {
            closestRightWallXPos = rightWallDetector.ClosestPoint(transform.position).x;
            transform.position = new Vector3(closestRightWallXPos - m_CapsuleCollider.size.x / 2, transform.position.y);
        }

        if (leftWallDetector)
        {
            closestLeftWallXPos = leftWallDetector.ClosestPoint(transform.position).x;
            transform.position = new Vector3(closestLeftWallXPos + m_CapsuleCollider.size.x / 2, transform.position.y);
        }          
    }

    public void CalculateGravity()
    {
        gravity = (2 * jumpHeight) / (timeToJumpApex * timeToJumpApex);
        wallJumpGravity = (2 * wallJumpHeight) / (wallJumpDuration * wallJumpDuration);
    }

    public void SetNormalFallMultiplier()
    {
        fallMultiplier = normalJumpMultiplier;
    }

    public void SetQuickFallMultiplier()
    {
        fallMultiplier = lowJumpMultiplier;
    }

    public void SetJumpRemainingForce(float val)
    {
        remainingJumpForce = val;
    }

    public void FlipDirection()
    {
        if (isTouchingLeftWall)
        {
            direction = 1;
        }else if (isTouchingRightWall)
        {
            direction = -1;
        }

        Flip();
    }
    
    public void ModifySlidingDetectorPosition()
    {
        Vector3 slideDetectorPos = m_SlideDetectorPosition.localPosition;
        slideDetectorPos.x *= facingRight ? 1 : -1;

        m_SlideDetectorPosition.localPosition = slideDetectorPos;
    }

    public void SetSlidingCollisionSize()
    {
        m_CapsuleCollider.size = m_SlideCollisionSize;
        m_CapsuleCollider.offset = new Vector2(m_CapsuleCollider.offset.x, m_SlideCollisionOffset);
        m_CapsuleCollider.direction = CapsuleDirection2D.Horizontal;
    }

    public void SetNormalCollisionSize()
    {
        m_CapsuleCollider.size = m_NormalCollisionSize;
        m_CapsuleCollider.offset = Vector2.zero;
        m_CapsuleCollider.direction = CapsuleDirection2D.Vertical;
    }

    private void TickSlideCooldown()
    {
        if(slideCooldown > 0)
        {
            slideCooldown -= Time.deltaTime;
        }     
    }

    public void SetSlideCooldown()
    {
        slideCooldown = m_SlideCooldown;
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
    #endif
}
