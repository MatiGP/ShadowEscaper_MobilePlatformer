using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
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
    public bool IsTouchingGround { get => isGrounded; }
    public bool IsJumping { get => isJumping; }
    public bool IsTouchingRightWall { get => isTouchingRightWall; }
    public bool IsTouchingLeftWall { get => isTouchingLeftWall; }
    public bool IsFacingRight { get => facingRight; }
    public bool IsTouchingCeiling { get => isTouchingCeiling; }
    public bool IsSliding { get => m_IsSliding; }
    public bool IsTouchingWallWhileSliding { get => m_IsTouchingWallWhileSliding; }
    public PersistantParticles PersistantParticles { get => m_PersistantParticles; }

    [SerializeField] protected CapsuleCollider2D m_CapsuleCollider;
    [SerializeField] protected Vector2 m_NormalCollisionSize = new Vector2(1.53f, 4f);
    [Header("Ground Movement")]
    [SerializeField] protected float footSpeed;
    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] protected Transform groundDetectorTransform;
    [SerializeField] protected Vector2 groundDetectorSize;
    [SerializeField] protected float groundDetectionLineLength;
    [Header("Sliding")]
    [SerializeField] protected float m_SlideSpeed = 36f;
    [SerializeField] protected float m_SlideSpeedFallOff = 1f;
    [SerializeField] protected float m_SlideCooldown = 2f;
    [SerializeField] protected Vector2 m_SlideCollisionSize = new Vector2(4f, 1.53f);
    [SerializeField] protected float m_SlideCollisionOffset = -1.6f;
    [SerializeField] protected Vector2 m_SlideDetectorSize;
    [SerializeField] protected Transform m_SlideDetectorPosition;

    [Space(1f)]
    [Header("Wall Detectors")]
    [SerializeField] protected Transform wallDetectorRight;
    [SerializeField] protected Transform wallDetectorLeft;
    [SerializeField] protected float wallDetectorLineLength;
    [SerializeField] protected Vector2 wallDetectorSize;
    [SerializeField] protected LayerMask wallLayer;
    [Space(1f)]
    [Header("Vertical Movement")]
    [SerializeField] protected float jumpHeight;
    [SerializeField] protected float timeToJumpApex;
    [SerializeField] protected float fallingSpeedLimit;
    [SerializeField] protected float normalJumpMultiplier;
    [SerializeField] protected float lowJumpMultiplier;
    [SerializeField] protected float maxFallSpeed;
    [SerializeField] protected float wallslideSpeed;
    [SerializeField] protected float wallJumpHeight;
    [SerializeField] protected float jumpOffWallForce;


    [Range(0.001f, 1f)]
    [SerializeField] protected float wallJumpDuration;

    [Space(1f)]
    [Header("Ceiling Detection")]
    [SerializeField] protected Vector2 ceilingDetectorSize;
    [SerializeField] protected Transform ceilingDetectorTransform;
    [SerializeField] protected LayerMask ceilingLayer;
    [Space(1f)]
    [Header("Player Model")]
    [SerializeField] protected SpriteRenderer playerCharacterSR;
    [SerializeField] protected Animator playerAnimator;

    protected float gravity;
    protected float wallJumpGravity;

    protected float direction;

    protected float fallMultiplier;
    protected float remainingJumpForce;
    protected float slideCooldown;

    protected bool isGrounded;
    protected bool isJumping;
    protected bool isTouchingRightWall;
    protected bool isTouchingLeftWall;
    protected bool facingRight;
    protected bool isTouchingCeiling;
    protected bool m_IsSliding;
    protected bool m_IsTouchingWallWhileSliding;

    protected StateMachine stateMachine;
    public Dictionary<EMovementStateType, BaseMovementState> MovementStates { get; protected set; }

    [Header("Persistant Particles")]
    [SerializeField] protected PersistantParticles m_PersistantParticles = null;


    protected virtual void SetUpStates()
    {
        stateMachine = new StateMachine();

        MovementStates = new Dictionary<EMovementStateType, BaseMovementState>() {
            { EMovementStateType.Idle, new IdleState(this, stateMachine, playerAnimator) },
            { EMovementStateType.Jumping, new JumpingState(this, stateMachine, playerAnimator) },
            { EMovementStateType.Running, new RunningState(this, stateMachine, playerAnimator) },
            { EMovementStateType.Walljumping, new WalljumpingState(this, stateMachine, playerAnimator) },
            { EMovementStateType.Wallsliding, new WallslidingState(this, stateMachine, playerAnimator) },
            { EMovementStateType.Death, new DeathState(this, stateMachine, playerAnimator) },
            { EMovementStateType.Falling, new FallingState(this, stateMachine, playerAnimator) },
            { EMovementStateType.Groundsliding, new GroundslidingState(this, stateMachine, playerAnimator) }
        };      
    }
    protected virtual void Awake()
    {
        SetUpStates();

        fallMultiplier = normalJumpMultiplier;      
    }


    protected void Start()
    {
        CalculateGravity();
    }

    protected void Update()
    {
        stateMachine.CurrentState.HandleAnimator();

        TickSlideCooldown();
    }

    protected private void LateUpdate()
    {
        stateMachine.CurrentState.HandleInput();
    }

    protected void FixedUpdate()
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

    public void Jump()
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

    public void GroundSlide()
    {
        if (slideCooldown <= 0)
        {
            m_IsSliding = true;
        }
    }

    public void AdjustPlayerDirection()
    {
        direction = IsTouchingLeftWall ? -1 : 1;
        Flip();
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
        }
        else if (isTouchingRightWall)
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

    protected void TickSlideCooldown()
    {
        if (slideCooldown > 0)
        {
            slideCooldown -= Time.deltaTime;
        }
    }

    public void SetSlideCooldown()
    {
        slideCooldown = m_SlideCooldown;
    }

}
