﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingState : BaseMovementState
{
    public FallingState(PlayerController controller, StateMachine stateMachine, Animator animator) : base(controller, stateMachine, animator)
    {
    }

    public override void Enter()
    {
        Debug.Log("Entering Falling State");
        movementVector.y = playerController.JumpForceRemaining;
    }

    public override void Exit()
    {
        movementVector.y = 0;
        movementVector.x = 0;
        
        Debug.Log("Ending Falling State");
    }

    public override void HandleAnimator()
    {
        animator.Play("Falling");
    }

    public override void HandleInput()
    {
        movementVector.x = playerController.Direction * playerController.FootSpeed;
        movementVector.y -= playerController.Gravity * playerController.FallMultiplier * Time.deltaTime;

        playerTransform.position += movementVector * Time.deltaTime;

        playerController.Flip();
    }

    public override void HandleLogic()
    {
        if (playerController.IsTouchingGround)
        {           
            playerController.FixPlayerPosition();
            if (playerController.Direction == 0)
            {
                stateMachine.ChangeState(playerController.idleState);
            }
            else
            {
                stateMachine.ChangeState(playerController.runningState);
            }                   
        }
        else
        {
            if(playerController.IsTouchingLeftWall || playerController.IsTouchingRightWall)
            {
                stateMachine.ChangeState(playerController.wallslidingState);
            }
        }

        
    }
}
