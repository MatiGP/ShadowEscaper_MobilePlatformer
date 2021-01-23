using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Ground Movement")]
    [SerializeField] float footSpeed;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundDetectorPosition;
    [SerializeField] Vector2 groundDetectorSize;
    [Header("Wall Detectors")]
    [SerializeField] Transform wallDetectorRight;
    [SerializeField] Transform wallDetectorLeft;
    [SerializeField] Vector2 wallDetectorSize;
    [SerializeField] LayerMask wallLayer;
    [Header("Vertical Movement")]
    [Space(1f)]
    [SerializeField] float jumpHeight;
    [SerializeField] float timeToJumpApex;
    [SerializeField] float normalJumpMultiplier;
    [SerializeField] float lowJumpMultiplier;
    [Space(1f)]
    [SerializeField] float wallslideSpeed;
    [Header("Player Model")]
    [SerializeField] SpriteRenderer playerCharacterSR;

    float gravity;
    float jumpVelocity;
    float direction;

    Vector3 movementVector;
    public Vector3 MovementVector { get => movementVector; private set => movementVector = value; }

    bool isGrounded;
    public bool IsGrounded { get => isGrounded; }
    bool wasGrounded;
    bool isJumping;
    bool isTouchingRightWall;
    bool isTouchingLeftWall;
    bool isWallSliding;
    public bool IsWallSliding { get => isWallSliding; }

    private void Awake()
    {
        gravity = (2 * jumpHeight) / (timeToJumpApex * timeToJumpApex);
        jumpVelocity = gravity * timeToJumpApex;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.BoxCast(groundDetectorPosition.position, groundDetectorSize, 0f, Vector2.down, 0f, groundLayer);

        #region Horizontal Movement
        movementVector.x = direction * footSpeed;

        if (isTouchingRightWall && movementVector.x > 0)
        {
            movementVector.x = 0;
        }
        else if (isTouchingLeftWall && movementVector.x < 0)
        {
            movementVector.x = 0;
        }
        #endregion

        #region Vertical Movement

        movementVector.y -= gravity * Time.deltaTime;

        if (isJumping)
        {
            Debug.Log("Normal jump");
            movementVector.y -= gravity * (normalJumpMultiplier - 1) * Time.deltaTime;
        }
        else if (movementVector.y > 0 && !isJumping && !(isTouchingRightWall || isTouchingLeftWall))
        {
            Debug.Log("Low jump");
            movementVector.y -= gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        if((isTouchingRightWall || isTouchingLeftWall) && movementVector.y < 0 )
        {
            Debug.Log("Should slide");
            movementVector.y = -wallslideSpeed * Time.deltaTime ;
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }

        if (movementVector.y < 0 && IsGrounded)
        {
            movementVector.y = 0;
        }

        #endregion

        //Debug.Log(movementVector);
        transform.position += movementVector * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        isTouchingRightWall = Physics2D.BoxCast(wallDetectorRight.position, wallDetectorSize, 0f, Vector2.right, 0f, wallLayer);
        isTouchingLeftWall = Physics2D.BoxCast(wallDetectorLeft.position, wallDetectorSize, 0f, Vector2.left, 0f, wallLayer);
    }

    public void Jump(bool isButtonHeld)
    {
        if (isButtonHeld)
        {
            if (IsGrounded)
            {
                movementVector.y = jumpVelocity;
                isJumping = true;
            }
        }
        else if (!isButtonHeld)
        {
            isJumping = false;
        }
    }

    public void ReadMoveInput(float newDirection)
    {
        direction = newDirection;

        if (newDirection > 0)
        {
            playerCharacterSR.flipX = false;
        }
        else if(newDirection < 0)
        {
            playerCharacterSR.flipX = true;
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
