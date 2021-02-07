using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Direction { get => direction; }
    public float FootSpeed { get => footSpeed; }
    public float Gravity { get => gravity; }
    public float JumpHeight { get => jumpHeight; }
    public float WallJumpHeight { get => wallJumpHeight; }
    public float WallSlideSpeed { get => wallslideSpeed; }
    public float JumpOffWallForce { get => jumpOffWallForce; }
    public float NormalJumpFallMultiplier { get => normalJumpMultiplier; }
    public float LowJumpFallMultiplier { get => lowJumpMultiplier; }

    public bool IsTouchingGround { get => isGrounded; }
    public bool IsJumping { get => isJumping; }
    public bool IsTouchingRightWall { get => isTouchingRightWall; }
    public bool IsTouchingLeftWall { get => isTouchingLeftWall; }
    public bool IsFacingRight { get => facingRight; }

    [SerializeField] CapsuleCollider2D capsuleCollider;
    [Header("Ground Movement")]
    [SerializeField] float footSpeed;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundDetectorTransform;
    [SerializeField] Vector2 groundDetectorSize;
    [SerializeField] float groundLineDetectionLength;
    [SerializeField] float jumpOffWallForce;
    [Space(1f)]
    [Header("Wall Detectors")]
    [SerializeField] Transform wallDetectorRight;
    [SerializeField] Transform wallDetectorLeft;
    [SerializeField] Vector2 wallDetectorSize;
    [SerializeField] LayerMask wallLayer;
    [Space(1f)]
    [Header("Vertical Movement")]
    [SerializeField] float jumpHeight;
    [SerializeField] float timeToJumpApex;
    [SerializeField] float normalJumpMultiplier;
    [SerializeField] float lowJumpMultiplier;
    [SerializeField] float maxFallSpeed;
    [SerializeField] float wallslideSpeed;
    [SerializeField] float wallJumpHeight;
    [Space(1f)]
    [Header("Player Model")]
    [SerializeField] SpriteRenderer playerCharacterSR;
    [SerializeField] Animator playerAnimator;

    float gravity;
    float jumpVelocity;
    float direction;
    float floorPos;

    Vector3 movementVector;
    Vector3 positionFix;
    Vector2 groundDeterctorLeftSidePos;
    Vector2 groundDetectorRightSidePos;

    bool isGrounded;
    bool wasGrounded;
    bool isJumping;
    bool isTouchingRightWall;
    bool isTouchingLeftWall;
    bool isWallSliding;
    bool wallJumped;
    bool facingRight;

    private StateMachine stateMachine;

    public IdleState idleState;
    public RunningState runningState;
    public JumpingState jumpingState;
    public FallingState fallingState;
    public GroundslidingState groundslidingState;
    public WallslidingState wallslidingState;
    public WalljumpingState walljumpingState;

    private void Awake()
    {
        stateMachine = new StateMachine();
        
        idleState = new IdleState(this, stateMachine, playerAnimator);
        runningState = new RunningState(this, stateMachine, playerAnimator);
        jumpingState = new JumpingState(this, stateMachine, playerAnimator);
        fallingState = new FallingState(this, stateMachine, playerAnimator);
        groundslidingState = new GroundslidingState(this, stateMachine, playerAnimator);
        wallslidingState = new WallslidingState(this, stateMachine, playerAnimator);
        walljumpingState = new WalljumpingState(this, stateMachine, playerAnimator);
        
        stateMachine.Initialize(idleState);

        groundDetectorRightSidePos = new Vector2(groundDetectorTransform.position.x - 0.8f, groundDetectorTransform.position.y);
    }

    void Start()
    {
        CalculateGravity();
        jumpVelocity = gravity * timeToJumpApex;
    }

    void Update()
    {
        groundDeterctorLeftSidePos.x = transform.position.x + 0.8f;
        groundDeterctorLeftSidePos.y = transform.position.y;

        groundDetectorRightSidePos.x = transform.position.x - 0.8f;
        groundDetectorRightSidePos.y = transform.position.y;


        float floorPosLeft = Physics2D.Raycast(groundDeterctorLeftSidePos, Vector2.down, 9f, groundLayer).point.y;
              floorPos = Physics2D.Raycast(transform.position, Vector2.down, 9f, groundLayer).point.y;
        float floorPosRight = Physics2D.Raycast(groundDetectorRightSidePos, Vector2.down, 9f, groundLayer).point.y;

        floorPos = Mathf.Max(floorPosLeft, floorPos, floorPosRight);

        stateMachine.currentState.HandleAnimator();
        
        
        #region Old Movement
        /*
        #region Horizontal Movement
       
        movementVector.x = direction * footSpeed;
                                
        if (isTouchingRightWall && direction > 0)
        {
            movementVector.x = 0;
        }
        else if (isTouchingLeftWall && direction < 0)
        {
            movementVector.x = 0;
        }
        
        #endregion

        #region Vertical Movement

        movementVector.y -= gravity * Time.deltaTime;
        movementVector.y = Mathf.Clamp(movementVector.y, -maxFallSpeed, movementVector.y);
     

        if (isJumping)
        {
            movementVector.y -= gravity * (normalJumpMultiplier - 1) * Time.deltaTime;

        }
        else if (movementVector.y > 0 && !isJumping)
        {
            movementVector.y -= gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
  
        }


        if (movementVector.y < 0 && isGrounded)
        {
            movementVector.y = 0;         
        }

        if ((isTouchingRightWall || isTouchingLeftWall) && (movementVector.y < 0) )
        {
            movementVector.y = -wallslideSpeed * Time.deltaTime;
            isWallSliding = true;         
        }
        else
        {
            isWallSliding = false;
        }       
        #endregion
        
        Flip();
        transform.position += movementVector * Time.deltaTime;
         
         */
        #endregion

    }

    private void LateUpdate()
    {
        stateMachine.currentState.HandleInput();
    }

    private void FixedUpdate()
    {
        isTouchingRightWall = Physics2D.BoxCast(wallDetectorRight.position, wallDetectorSize, 0f, Vector2.right, 0f, wallLayer);
        isTouchingLeftWall = Physics2D.BoxCast(wallDetectorLeft.position, wallDetectorSize, 0f, Vector2.left, 0f, wallLayer);
        isGrounded = Physics2D.OverlapBox(groundDetectorTransform.position, groundDetectorSize, 0f, groundLayer);

        stateMachine.currentState.HandleLogic();

    }


    public void Jump(bool isButtonHeld)
    {
        if (isButtonHeld)
        {
            /*
            if (isGrounded)
            {
                //movementVector.y = jumpVelocity;
                isJumping = true;
            }
            */

            isJumping = true;
            /*
            if (isWallSliding)
            {
                Debug.Log("Should wall jump");
                movementVector.y = wallJumpHeight;             
                isJumping = true;
            }
            */
        }
        else
        {
            isJumping = false;         
        }
    }

    public void ReadMoveInput(float newDirection)
    {
        direction = newDirection;
        
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(groundDetectorTransform.position, groundDetectorSize);
        Gizmos.color = Color.green;
        Gizmos.DrawCube(wallDetectorRight.position, wallDetectorSize);
        Gizmos.color = Color.green;
        Gizmos.DrawCube(wallDetectorLeft.position, wallDetectorSize);
    }

    public void FixPlayerPosition()
    {       
        transform.position = new Vector3(transform.position.x, capsuleCollider.size.y / 2 + Mathf.Abs(floorPos));
    }

    public void CalculateGravity()
    {
        gravity = (2 * jumpHeight) / (timeToJumpApex * timeToJumpApex);
    }

    public void ModifySpeed(float val)
    {
        footSpeed = val;
    }

    public void ModifyJumpHeight(float val)
    {
        jumpHeight = val;
        CalculateGravity();
    }

    public void ModifyTimeToJumpApex(float val)
    {
        timeToJumpApex = val;
        CalculateGravity();
    }

    public void ModifyNormalJumpMulti(float val)
    {
        normalJumpMultiplier = val;
    }

    public void ModifyLowJumpMulti(float val)
    {
        lowJumpMultiplier = val;
    }

    public void ModifyWallJumpOffForce(float val)
    {
        jumpOffWallForce = val;
    }

    public void ModifyWalljumpHeight(float val)
    {
        wallJumpHeight = val;
    }

    public void ModifyWallSlideSpeed(float val)
    {
        wallslideSpeed = val;
    }
}
