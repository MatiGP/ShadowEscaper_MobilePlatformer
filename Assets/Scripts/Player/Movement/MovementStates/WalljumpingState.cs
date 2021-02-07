﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalljumpingState : BaseMovementState
{
    int wallJumpXDirection;

    public WalljumpingState(PlayerController controller, StateMachine stateMachine, Animator animator) : base(controller, stateMachine, animator)
    {

    }

    public override void Enter()
    {
        movementVector.x = 0;
        movementVector.y = playerController.WallJumpHeight;
        wallJumpXDirection = playerController.IsFacingRight ? -1 : 1;
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
        
        movementVector.x = wallJumpXDirection * playerController.JumpOffWallForce;
        movementVector.y -= playerController.Gravity * Time.deltaTime;

        playerTransform.position += movementVector * Time.deltaTime;
    }

    public override void HandleLogic()
    {
        if(movementVector.y < 0)
        {
            stateMachine.ChangeState(playerController.fallingState);
        }

        if(!playerController.IsTouchingGround && (playerController.IsTouchingLeftWall || playerController.IsTouchingRightWall))
        {
            stateMachine.ChangeState(playerController.wallslidingState);
        }
    } 
}
