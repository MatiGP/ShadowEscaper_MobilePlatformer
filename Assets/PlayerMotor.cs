using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [SerializeField] float runSpeed = 40f;
    [SerializeField] float slideDuration;
    [SerializeField] float minSlideDuration = 0.3f;
    [SerializeField] float slideCooldown = 1;
    float currentSlideCooldown = -1;
    float jumpDuriation;

    [SerializeField] Joystick joystick;

    [SerializeField] Animator animator;

    [SerializeField] CapsuleCollider2D playerCollider;

    CharacterController characterController;

    Vector2 colliderSizeWhenPlayerStands;
    Vector2 colliderSizeWhenPlayerSlides;
    
    float horizontalMove = 0f;

    bool jump = false;
    bool slide = false;
    bool isSliding;
    bool isJumping;
    bool canMove = true;

    // Start is called before the first frame update
    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        jumpDuriation = characterController.JumpDuration;
        colliderSizeWhenPlayerStands = playerCollider.size;
        colliderSizeWhenPlayerSlides = new Vector2(colliderSizeWhenPlayerStands.y, colliderSizeWhenPlayerStands.x);
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove) return;        

        if(joystick.Horizontal > 0)
        {
            horizontalMove = runSpeed;
        }else if(joystick.Horizontal < 0)
        {
            horizontalMove = -runSpeed;
        }
        else
        {
            horizontalMove = 0f;
        }

        if (currentSlideCooldown > 0) currentSlideCooldown -= Time.deltaTime;

    }

    void FixedUpdate()
    {
        characterController.Move(horizontalMove * Time.fixedDeltaTime, jump, slide, canMove);
    }

    public void Jump()
    {
        if (!isJumping)
        {
            StartCoroutine("StartJumping");
        }      
    }

    public void Slide()
    {
        if (!isSliding && currentSlideCooldown <= 0)
        {
            SoundManager.instance.PlaySoundEffect(SoundType.Player_Slide);

            StartCoroutine("StartSliding");
        }
    }

    IEnumerator StartSliding()
    {
        isSliding = true;
        slide = true;

        SetPlayerColliderSize(colliderSizeWhenPlayerSlides, CapsuleDirection2D.Horizontal, -1.69f);

        yield return new WaitForSeconds(slideDuration);

        SetPlayerColliderSize(colliderSizeWhenPlayerStands, CapsuleDirection2D.Vertical, 0f);

        slide = false;
        isSliding = false;
        currentSlideCooldown = slideCooldown;


    }

    public void InterruptSliding()
    {             
        StopCoroutine("StartSliding");

        SetPlayerColliderSize(colliderSizeWhenPlayerStands, CapsuleDirection2D.Vertical, 0f);

        slide = false;
        isSliding = false;
        if (currentSlideCooldown > 0) return;
        currentSlideCooldown = slideCooldown;
    }

    void SetPlayerColliderSize(Vector2 newSize, CapsuleDirection2D direction, float colliderOffsetY)
    {
        playerCollider.size = newSize;
        playerCollider.direction = direction;
        playerCollider.offset = new Vector2(0, colliderOffsetY);
    }

    public void InterruptJumping()
    {
        StopCoroutine("StartJumping");
        characterController.StopJumping();
        jump = false;
        isJumping = false;
    }

    IEnumerator StartJumping()
    {
        isJumping = true;
        jump = true;
        yield return new WaitForSeconds(jumpDuriation);
        characterController.StopJumping();
        jump = false;
        isJumping = false;
    }

    public void DisableControls()
    {
        canMove = false;      
        
    }
}
