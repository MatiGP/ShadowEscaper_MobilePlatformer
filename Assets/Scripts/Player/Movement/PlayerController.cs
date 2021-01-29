﻿using System.Collections;
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

    public bool IsGrounded { get => isGrounded; }
    public bool IsJumping { get => isJumping; }
    public bool IsTouchingRightWall { get => isTouchingRightWall; }
    public bool IsTouchingLeftWall { get => isTouchingLeftWall; }
    public bool IsFacingRight { get => facingRight; }


    [Header("Ground Movement")]
    [SerializeField] float footSpeed;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundDetectorPosition;
    [SerializeField] Vector2 groundDetectorSize;
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

    Vector3 movementVector;
    
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
    }

    void Start()
    {
        gravity = (2 * jumpHeight) / (timeToJumpApex * timeToJumpApex);
        jumpVelocity = gravity * timeToJumpApex;
    }

    void Update()
    {    
        
        stateMachine.currentState.HandleLogic();
        stateMachine.currentState.HandleAnimator();
        stateMachine.currentState.HandleInput();
       
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


    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapBox(groundDetectorPosition.position, groundDetectorSize, 0f, groundLayer);
        isTouchingRightWall = Physics2D.BoxCast(wallDetectorRight.position, wallDetectorSize, 0f, Vector2.right, 0f, wallLayer);
        isTouchingLeftWall = Physics2D.BoxCast(wallDetectorLeft.position, wallDetectorSize, 0f, Vector2.left, 0f, wallLayer);
    }

    public void Jump(bool isButtonHeld)
    {
        if (isButtonHeld)
        {
            if (isGrounded)
            {
                //movementVector.y = jumpVelocity;
                isJumping = true;
            }

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
        Gizmos.DrawCube(groundDetectorPosition.position, groundDetectorSize);
        Gizmos.color = Color.green;
        Gizmos.DrawCube(wallDetectorRight.position, wallDetectorSize);
        Gizmos.color = Color.green;
        Gizmos.DrawCube(wallDetectorLeft.position, wallDetectorSize);

    }
}
