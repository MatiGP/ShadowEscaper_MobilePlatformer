using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [SerializeField] Joystick joystick;

    CharacterController characterController;
    PlayerController playerController;


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
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        playerController.ReadMoveInput(joystick.Horizontal);
        
    }


    public void Jump()
    {       
        playerController.Jump(isButtonHeld: true);
    }

    public void Slide()
    {
        
    }

    IEnumerator StartSliding()
    {
        yield return null;
    }

    public void InterruptSliding()
    {             
       
    }

    void SetPlayerColliderSize(Vector2 newSize, CapsuleDirection2D direction, float colliderOffsetY)
    {
       
    }

    public void InterruptJumping()
    {       
        playerController.Jump(isButtonHeld: false);
    }

    IEnumerator StartJumping()
    {
        isJumping = true;
        jump = true;
        yield return new WaitForSeconds(1f);
        //characterController.StopJumping();
        jump = false;
        isJumping = false;
    }

    public void DisableControls()
    {
        canMove = false;      
        
    }
}
