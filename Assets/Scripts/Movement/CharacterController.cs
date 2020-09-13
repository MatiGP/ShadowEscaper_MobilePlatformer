using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] float jumpForce = 400f;
    [SerializeField] float wallSlideSpeed = 5;
    [SerializeField] float wallJumpXForce = 500f;
    [SerializeField] float wallJumpYForce = 700f;
    [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;
    [SerializeField] LayerMask groundMask;
    [SerializeField] LayerMask wallMask;
    [SerializeField] Transform groundCheck;
    [SerializeField] Transform wallCheck;
    [SerializeField] Animator animator;
    [SerializeField] Transform playerModel;
    [SerializeField] Transform detectorsTransform;

    float groundCheckRadius = 0.2f;
    float wallCheckRadius = 0.3f;
    float limitFallSpeed = 50f;

    bool isGrounded;
    bool facingRight = true;
    bool canDoubleJump = false;
    bool isTouchingTheWall = false;
    bool isWallsliding = false;
    bool wasWallslidingBefore = false;
    bool canMove = true;

    Vector2 velocity = Vector2.zero;

    Rigidbody2D rbody;

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
        bool wasGrounded = isGrounded;
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
            Collider2D[] collidersWall = Physics2D.OverlapCircleAll(wallCheck.position, wallCheckRadius, wallMask);
            if(collidersWall.Length > 0)
            {
                isTouchingTheWall = true;
            }
        }

        animator.SetFloat("velocityY", rbody.velocity.y);
       
        
    }

    public void Move(float direction, bool jump, bool slide)
    {
        if (rbody.velocity.y < -limitFallSpeed) rbody.velocity = new Vector2(rbody.velocity.x, -limitFallSpeed);

        Vector2 targetVelocity = new Vector2(direction * 10f, rbody.velocity.y);
        rbody.velocity = Vector2.SmoothDamp(rbody.velocity, targetVelocity, ref velocity, movementSmoothing);

        if(direction > 0 && !facingRight && !isWallsliding)
        {
            Flip();
        }
        else if(direction < 0 && facingRight && !isWallsliding)
        {
            Flip();
        }

        if(isGrounded && jump)
        {
            isGrounded = false;
            rbody.AddForce(new Vector2(0f, jumpForce));
            canDoubleJump = true;
        }
        else if (!isGrounded && jump && canDoubleJump && !isWallsliding)
        {
            canDoubleJump = false;
            rbody.velocity = new Vector2(rbody.velocity.x, 0f);
            rbody.AddForce(new Vector2(0f, jumpForce / 1.2f));
        }
        else if(isTouchingTheWall && !isGrounded)
        {
            if(!wasWallslidingBefore && rbody.velocity.y < 0f)
            {
                isWallsliding = true;
                
                animator.SetBool("isWallsliding", true);
            }

            if (isWallsliding)
            {
                wasWallslidingBefore = true;
                rbody.velocity = new Vector2(transform.localScale.x * 2, -wallSlideSpeed);
            }

            if(jump && isWallsliding)
            {
                animator.SetBool("isWallsliding", false);
                animator.SetTrigger("wallJump");
                rbody.velocity = new Vector2(0f, 0f);
                rbody.AddForce(new Vector2(-transform.localScale.x * wallJumpXForce, wallJumpYForce));
                canDoubleJump = true;
                isWallsliding = false;
                wasWallslidingBefore = false;
                animator.ResetTrigger("wallJump");


            }
        }else if(isWallsliding && !isTouchingTheWall)
        {
            isWallsliding = false;
            wasWallslidingBefore = false;
            
            animator.SetBool("isWallsliding", false);
            
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(groundCheck.position, groundCheckRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(wallCheck.position, wallCheckRadius);
    }
}
