﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public float JumpForceRemaining { get => remainingJumpForce; }
    public float WallJumpDuration { get => wallJumpDuration; }

    public bool IsTouchingGround { get => isGrounded; }
    public bool IsJumping { get => isJumping; }
    public bool IsTouchingRightWall { get => isTouchingRightWall; }
    public bool IsTouchingLeftWall { get => isTouchingLeftWall; }
    public bool IsFacingRight { get => facingRight; }
    public bool IsTouchingCeiling { get => isTouchingCeiling; }

    [SerializeField] CapsuleCollider2D capsuleCollider;
    [Header("Ground Movement")]
    [SerializeField] float footSpeed;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundDetectorTransform;
    [SerializeField] Vector2 groundDetectorSize;
    [SerializeField] float groundDetectionLineLength;
    [SerializeField] float jumpOffWallForce;
    [Space(1f)]
    [Header("Wall Detectors")]
    [SerializeField] Transform wallDetectorRight;
    [SerializeField] Transform wallDetectorLeft;
    [SerializeField] float wallDetectorLineLength;
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
    [SerializeField] float wallJumpGravity;
    [Range(0.1f, 2f)]
    [SerializeField] float wallJumpDuration;

    [Space(1f)]
    [Header("Ceiling Detection")]
    [SerializeField] Vector2 ceilingDetectorSize;
    [SerializeField] Transform ceilingDetectorTransform;
    [SerializeField] LayerMask ceilingLayer;
    [Space(1f)]
    [Header("Player Model")]
    [SerializeField] SpriteRenderer playerCharacterSR;
    [SerializeField] Animator playerAnimator;

    float gravity;
    
    float jumpVelocity;
    float direction;
    float floorPos;
    float wallPosRight;
    float wallPosLeft;
    float fallMultiplier;
    float remainingJumpForce;

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
    bool isTouchingCeiling;

    private StateMachine stateMachine;

    public IdleState idleState;
    public RunningState runningState;
    public JumpingState jumpingState;
    public FallingState fallingState;
    public GroundslidingState groundslidingState;
    public WallslidingState wallslidingState;
    public WalljumpingState walljumpingState;
    public DeathState deathState;

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
        deathState = new DeathState(this, stateMachine, playerAnimator);
        
        stateMachine.Initialize(idleState);

        fallMultiplier = normalJumpMultiplier;
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

        stateMachine.currentState.HandleAnimator();       

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
        isTouchingCeiling = Physics2D.OverlapBox(ceilingDetectorTransform.position, ceilingDetectorSize, 0f, ceilingLayer);

        stateMachine.currentState.HandleLogic();

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

    public void ReadMoveInput(float newDirection)
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

    public void Die()
    {
        stateMachine.ChangeState(deathState);
    }   

    public void FixPlayerGroundPos()
    {
        float floorPosLeft = Physics2D.Raycast(groundDeterctorLeftSidePos, Vector2.down, groundDetectionLineLength, groundLayer).point.y;
        floorPos = Physics2D.Raycast(transform.position, Vector2.down, groundDetectionLineLength, groundLayer).point.y;
        float floorPosRight = Physics2D.Raycast(groundDetectorRightSidePos, Vector2.down, groundDetectionLineLength, groundLayer).point.y;

        floorPos = Mathf.Max(floorPosLeft, floorPos, floorPosRight);

        transform.position = new Vector3(transform.position.x, capsuleCollider.size.y / 2 + floorPos);
    }

    public void FixPlayerWallPos()
    {

        Collider2D rightWallDetector = Physics2D.OverlapBox(wallDetectorRight.position, wallDetectorSize, 0f, wallLayer);
        Collider2D leftWallDetector = Physics2D.OverlapBox(wallDetectorLeft.position, wallDetectorSize, 0f, wallLayer);

        Debug.Log($"{rightWallDetector?.ClosestPoint(transform.position)} Right | Left {leftWallDetector?.ClosestPoint(transform.position)}");

        float closestRightWallXPos = 0;
        float closestLeftWallXPos = 0;

        if (rightWallDetector)
        {
            closestRightWallXPos = rightWallDetector.ClosestPoint(transform.position).x;
            transform.position = new Vector3(closestRightWallXPos - capsuleCollider.size.x / 2, transform.position.y);
        }

        if (leftWallDetector)
        {
            closestLeftWallXPos = leftWallDetector.ClosestPoint(transform.position).x;
            transform.position = new Vector3(closestLeftWallXPos + capsuleCollider.size.x / 2, transform.position.y);
        }          
    }

    public void CalculateGravity()
    {
        gravity = (2 * jumpHeight) / (timeToJumpApex * timeToJumpApex);
    }
    #region Debugs
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
#endregion
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

    private void OnDrawGizmos()
    {
        #if UNITY_EDITOR
        Gizmos.color = Color.red;
        Gizmos.DrawCube(groundDetectorTransform.position, groundDetectorSize);
        Gizmos.color = Color.green;
        Gizmos.DrawCube(wallDetectorRight.position, wallDetectorSize);
        Gizmos.color = Color.green;
        Gizmos.DrawCube(wallDetectorLeft.position, wallDetectorSize);
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(ceilingDetectorTransform.position, ceilingDetectorSize);       
        #endif
    }
}
