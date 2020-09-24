using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] float jumpForce = 400f;
    [SerializeField] float wallSlideSpeed = 5;
    [SerializeField] float wallJumpXForce = 500f;
    [SerializeField] float wallJumpYForce = 700f;

    [Range(0, .3f)]
    [SerializeField] private float movementSmoothing = .05f;

    [SerializeField] LayerMask groundMask;
    [SerializeField] LayerMask wallMask;
    [SerializeField] Transform groundCheck;
    [SerializeField] Transform wallJumpDetectorTransform;
    [SerializeField] Animator animator;
    [SerializeField] Transform playerModel;
    [SerializeField] Transform detectorsTransform;
    [SerializeField] Vector2 wallJumpDetectorSize = new Vector2(0.1f, 3f);

    float groundCheckRadius = 0.2f;
    float wallCheckRadius = 0.3f;
    float limitFallSpeed = 50f;

    bool isGrounded;
    bool wasGrounded;
    bool facingRight = true;
    bool canDoubleJump = false;
    bool isTouchingTheWall = false;
    bool isWallsliding = false;
    bool wasWallslidingBefore = false;
    bool canMove = true;

    Vector2 velocity = Vector2.zero;

    Rigidbody2D rbody;

    string currentAnimationState;

    const string PLAYER_JUMP = "Jump";
    const string IN_AIR = "InAir";
    const string WALL_JUMP = "WallJump";
    const string JUMP_INCREASING_HEIGHT = "IncreasingHeight";
    const string WALL_SLIDE = "WallSlide";
    const string FALLING = "Falling";
    const string LANDING = "Landing";
    const string PLAYER_RUN = "PlayerRun";
    const string PLAYER_IDLE = "PlayerIdle";
    const string PLAYER_JUMP_PREP = "JumpPrep";

    // Velocity threshholds for animations;
    const float HEIGHT_INCREASING_THRESHHOLD = 26f;
    const float IN_AIR_THRESHHOLD = 18f;
    const float FALLING_THRESHHOLD = -1f;
    const float LANDING_THRESHHOLD = -25f;
    

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        wasGrounded = isGrounded;
        isGrounded = false;

        Collider2D[] collidersGround = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, groundMask);
        
        if(collidersGround.Length > 0)
        {
            isGrounded = true;

            if (!wasGrounded)
            {
                Debug.Log("Efekt lądowania, cząsteczki itp");
            }
        }

        isTouchingTheWall = false;

        if (!isGrounded)
        {
            Collider2D[] collidersWall = Physics2D.OverlapBoxAll(wallJumpDetectorTransform.position, wallJumpDetectorSize, 0f, wallMask);
            if(collidersWall.Length > 0)
            {
                isTouchingTheWall = true;
            }
        }

        if(rbody.velocity.y < FALLING_THRESHHOLD && !isGrounded && !isWallsliding)
        {
            ChangeAnimationState(FALLING);

        }else if(rbody.velocity.y < IN_AIR_THRESHHOLD && !isGrounded && !isWallsliding)
        {
            ChangeAnimationState(IN_AIR);

        }else if(rbody.velocity.y < HEIGHT_INCREASING_THRESHHOLD && !isGrounded && !isWallsliding)
        {
            ChangeAnimationState(JUMP_INCREASING_HEIGHT);
        }
       
        
    }

    public void Move(float direction, bool jump, bool slide)
    {
        if (rbody.velocity.y < -limitFallSpeed) rbody.velocity = new Vector2(rbody.velocity.x, -limitFallSpeed);

        Vector2 targetVelocity = new Vector2(direction, rbody.velocity.y);
        rbody.velocity = Vector2.SmoothDamp(rbody.velocity, targetVelocity, ref velocity, movementSmoothing);

        FlipCharacterBasedOnDirection(direction);

        if(direction != 0 && wasGrounded)
        {
            ChangeAnimationState(PLAYER_RUN);
        }
        else if(direction == 0 && wasGrounded)
        {
            ChangeAnimationState(PLAYER_IDLE);
        }

        if (isGrounded && jump)
        {
            isGrounded = false;
            rbody.AddForce(new Vector2(0f, jumpForce));
            canDoubleJump = true;

            ChangeAnimationState(PLAYER_JUMP_PREP);
            
        }
        else if (!isGrounded && jump && canDoubleJump && !isWallsliding)
        {
            canDoubleJump = false;
            rbody.velocity = new Vector2(rbody.velocity.x, 0f);
            rbody.AddForce(new Vector2(0f, jumpForce / 1.2f));
        }
        else if (isTouchingTheWall && !isGrounded)
        {
            if (!wasWallslidingBefore && rbody.velocity.y < 0f)
            {
                isWallsliding = true;               
            }

            if (isWallsliding)
            {
                wasWallslidingBefore = true;
                rbody.velocity = new Vector2(transform.localScale.x * 2, -wallSlideSpeed);

                ChangeAnimationState(WALL_SLIDE);
            }

            if (jump && isWallsliding)
            {
                ChangeAnimationState(WALL_JUMP);

                rbody.velocity = new Vector2(0f, 0f);
                rbody.AddForce(new Vector2(-transform.localScale.x * wallJumpXForce, wallJumpYForce));
                canDoubleJump = true;
                isWallsliding = false;
                wasWallslidingBefore = false;

            }
        }
        else if (isWallsliding && !isTouchingTheWall)
        {
            isWallsliding = false;
            wasWallslidingBefore = false;
        }


    }

    private void FlipCharacterBasedOnDirection(float direction)
    {
        if (direction > 0 && !facingRight && !isWallsliding)
        {
            Flip();
        }
        else if (direction < 0 && facingRight && !isWallsliding)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = playerModel.localScale;
        theScale.x *= -1;
        playerModel.localScale = theScale;
        detectorsTransform.localScale = theScale;
    }

    void ChangeAnimationState(string nameOfNewAnimationState)
    {
        if (currentAnimationState == nameOfNewAnimationState) return;

        animator.Play(nameOfNewAnimationState);

        currentAnimationState = nameOfNewAnimationState;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(groundCheck.position, groundCheckRadius);       
        Gizmos.color = Color.green;
        Gizmos.DrawCube(wallJumpDetectorTransform.position, wallJumpDetectorSize);
    }
}
