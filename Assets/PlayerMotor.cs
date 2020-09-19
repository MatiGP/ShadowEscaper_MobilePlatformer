﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [SerializeField] float runSpeed = 40f;
    [SerializeField] Joystick joystick;
    [SerializeField] Animator animator;
    CharacterController characterController;

    
    float horizontalMove = 0f;
    bool jump = false;

    // Start is called before the first frame update
    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        /*

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

        */
        if(Input.GetAxis("Horizontal") > 0)
        {
            horizontalMove = runSpeed;
        }else if(Input.GetAxis("Horizontal") < 0)
        {
            horizontalMove = -runSpeed;
        }
        else
        {
            horizontalMove = 0f;
        }



        //animator.SetBool("isRunning", joystick.Horizontal != 0);
        animator.SetBool("isRunning", Input.GetAxis("Horizontal") != 0);

    }

    void FixedUpdate()
    {
        characterController.Move(horizontalMove * Time.fixedDeltaTime, jump, false);
        jump = false;
    }

    public void Jump()
    {
        jump = true;
        animator.SetTrigger("jump");
    }
}
