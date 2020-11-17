using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("Jump Settings")]
    [SerializeField] float jumpHeight = 400f;
    [SerializeField] float fallMultiplier = 2.5f;
    [SerializeField] float lowJumpMultiplier = 2f;
    [SerializeField] float jumpDuration = 0.2f;
    public float JumpDuration { get { return jumpDuration; } }
    float currentJumpDuration;
    [Header("Wall Jump Settings")]
    [SerializeField] float wallSlideSpeed = 5;
    [SerializeField] float wallJumpXForce = 500f;
    [SerializeField] float wallJumpYForce = 700f;
    [Header("Slide Settings")]
    [SerializeField] float slideSpeed;
    [SerializeField] float slideSpeedFallOff;
    [Header("Movement Settings")]
    [Range(0, .3f)]
    [SerializeField] float movementSmoothing = .05f;

    [Header("Ground Detector Settings")]
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    [Header("Wall Jump Detector Settings")]
    [SerializeField] LayerMask wallMask;
    [SerializeField] Transform wallJumpDetectorTransform;
    [SerializeField] Vector2 wallJumpDetectorSize = new Vector2(0.1f, 3f);
    [HideInInspector]
    [SerializeField] Transform playerModel;
    [HideInInspector]
    [SerializeField] Transform detectorsTransform;
    [HideInInspector]
    [SerializeField] Animator animator;
    

    float groundCheckRadius = 0.2f;
    float wallCheckRadius = 0.3f;
    float limitFallSpeed = 25f;

    bool canMove = true;
    bool facingRight = true;

    bool isGrounded;
    bool wasGrounded;   
    bool canDoubleJump;
    bool isTouchingTheWall;
    bool isWallsliding;
    bool wasWallslidingBefore;   
    bool lockAnimation;
    bool isJumping;

    Vector2 velocity = Vector2.zero;

    Rigidbody2D rbody;

    string currentAnimationState;

    //Animation states names;
    const string JUMP = "Jump";
    const string IN_AIR = "InAir";
    const string WALL_JUMP = "WallJump";
    const string JUMP_INCREASING_HEIGHT = "IncreasingHeight";
    const string WALL_SLIDE = "WallSlide";
    const string FALLING = "Falling";
    const string LANDING = "Landing";
    const string RUN = "PlayerRun";
    const string IDLE = "PlayerIdle";
    const string JUMP_PREP = "JumpPrep";   
    const string SLIDE_START = "SlideStart";
    const string GROUND_SLIDE = "GroundSlide";
    const string SLIDE_END = "SlideEnd";

    // Velocity threshholds for animations;
    const float HEIGHT_INCREASING_THRESHHOLD = 26f;
    const float IN_AIR_THRESHHOLD = 18f;
    const float FALLING_THRESHHOLD = -1f;
    const float LANDING_THRESHHOLD = -25f;

    float currentSlideSpeed;

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
        if (isJumping)
        {
            currentJumpDuration -= Time.deltaTime;
        }       
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
            ChangeAnimationState(FALLING, 0f);

        }else if(rbody.velocity.y < IN_AIR_THRESHHOLD && !isGrounded && !isWallsliding)
        {
            ChangeAnimationState(IN_AIR, 0f);

        }else if(rbody.velocity.y < HEIGHT_INCREASING_THRESHHOLD && !isGrounded && !isWallsliding)
        {
            ChangeAnimationState(JUMP_INCREASING_HEIGHT, 0f);
        }

        if (!isWallsliding)
        {
            if (rbody.velocity.y < 0)
            {
                rbody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier) * Time.deltaTime;
            }
            else if (rbody.velocity.y > 0 && !isJumping)
            {
                rbody.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
        }
    }

    public void Move(float direction, bool jump, bool isSliding, bool canMove)
    {
        isJumping = jump;

        if (!canMove)
        {
            rbody.velocity = Vector2.zero;
            rbody.bodyType = RigidbodyType2D.Static;
            
            return;
        }

        if (rbody.velocity.y < -limitFallSpeed) rbody.velocity = new Vector2(rbody.velocity.x, -limitFallSpeed);

        Vector2 targetVelocity = new Vector2(direction, rbody.velocity.y);
        rbody.velocity = Vector2.SmoothDamp(rbody.velocity, targetVelocity, ref velocity, movementSmoothing);

        FlipCharacterBasedOnDirection(direction);

        if(direction != 0 && isGrounded && !isSliding)
        {
            ChangeAnimationState(RUN, 0f);
        }
        else if(direction == 0 && isGrounded && !isSliding)
        {
            ChangeAnimationState(IDLE, 0f);
        }

        if (!isSliding) currentSlideSpeed = slideSpeed;

        if(isGrounded && isSliding)
        {
            if (facingRight)
            {
                rbody.velocity = new Vector2(currentSlideSpeed, 0f);
            }
            else
            {
                rbody.velocity = new Vector2(-currentSlideSpeed, 0f);
            }

            CalculateSlidingSpeed();

            ChangeAnimationState(GROUND_SLIDE, 0f);


        }
        else if (isGrounded && jump)
        {
            isGrounded = false;
            currentJumpDuration = jumpDuration;
            rbody.velocity = new Vector2(rbody.velocity.x, jumpHeight);

            ChangeAnimationState(JUMP_PREP, 0f);
            
        }
        else if (!isGrounded && jump && !isWallsliding && currentJumpDuration > 0)
        {           
            rbody.velocity = new Vector2(rbody.velocity.x, jumpHeight);
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
                rbody.velocity = new Vector2(detectorsTransform.localScale.x * 2, -wallSlideSpeed);

                ChangeAnimationState(WALL_SLIDE, 0f);
            }

            if (jump && isWallsliding)
            {
                ChangeAnimationState(WALL_JUMP, animationLockDuration: 0.3f);

                rbody.velocity = new Vector2(0f, 0f);
                rbody.AddForce(new Vector2(-detectorsTransform.localScale.x * wallJumpXForce, wallJumpYForce));
                Flip();
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

    private void CalculateSlidingSpeed()
    {
        if (slideSpeed * 0.6f < currentSlideSpeed)
        {
            currentSlideSpeed = Mathf.Clamp(currentSlideSpeed - 0.8f * slideSpeedFallOff * Time.deltaTime, 0, slideSpeed);
        }
        else
        {
            currentSlideSpeed = Mathf.Clamp(currentSlideSpeed - 2f * slideSpeedFallOff * Time.deltaTime, 0, slideSpeed);
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

    void ChangeAnimationState(string nameOfNewAnimationState, float animationLockDuration)
    {
        if (currentAnimationState == nameOfNewAnimationState) return;

        if (!lockAnimation)
        {
            animator.Play(nameOfNewAnimationState);
            StartCoroutine(LockAnimationState(animationLockDuration));
        }
        

        currentAnimationState = nameOfNewAnimationState;
        
    }

    private void OnDrawGizmos()
    {
        //Ground detector;
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(groundCheck.position, groundCheckRadius);
        //Wall detector;
        Gizmos.color = Color.green;
        Gizmos.DrawCube(wallJumpDetectorTransform.position, wallJumpDetectorSize);
    }

    IEnumerator LockAnimationState(float lockDuration)
    {
        lockAnimation = true;
        yield return new WaitForSeconds(lockDuration);
        lockAnimation = false;
    }

    public void StopJumping()
    {
        currentJumpDuration = -1f;
    }
}
