﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalljumpingState : BaseMovementState
{
    float walljumpDirection;
    float currentWallJumpDuration;

    public WalljumpingState(PlayerController controller, StateMachine stateMachine, Animator animator) : base(controller, stateMachine, animator)
    {
        
    }

    public override void Enter()
    {       
        movementVector.x = 0;
        movementVector.y = playerController.WallJumpHeight;
        playerController.FlipDirection();
        currentWallJumpDuration = 0;
        walljumpDirection = playerController.IsTouchingLeftWall ? 1 : -1;
    }

    public override void Exit()
    {
        movementVector.y = 0;
        movementVector.x = 0;
        
    }

    public override void HandleAnimator()
    {
        animator.Play("WallJump");
    }

    public override void HandleInput()
    {
        currentWallJumpDuration += Time.deltaTime;

        if (currentWallJumpDuration <= playerController.WallJumpDuration)
        {
            movementVector.x = walljumpDirection * playerController.JumpOffWallForce;
        }
        else
        {
            movementVector.x = playerController.Direction * playerController.FootSpeed;
        }
       
        movementVector.y -= playerController.WallJumpGravity * Time.deltaTime;

        playerTransform.position += movementVector * Time.deltaTime;
    }

    public override void HandleLogic()
    {
        if (playerController.IsTouchingCeiling)
        {
            playerController.ClearDirection();
            stateMachine.ChangeState(playerController.FallingState);
        }       

        if(movementVector.y < 0)
        {
            playerController.ClearDirection();
            stateMachine.ChangeState(playerController.FallingState);
        }

        if (currentWallJumpDuration <= playerController.WallJumpDuration)
        {
            return;
        }

        if (!playerController.IsTouchingGround && (playerController.IsTouchingLeftWall || playerController.IsTouchingRightWall))
        {
            stateMachine.ChangeState(playerController.WallSlidingState);
        }
    } 
}
