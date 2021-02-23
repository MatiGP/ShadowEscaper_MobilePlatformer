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
        playerController.Jump();
    }

    public void Slide()
    {
        
    }

    IEnumerator StartSliding()
    {
        yield return null;
    }   

    public void DisableControls()
    {
        canMove = false;             
    }
}
