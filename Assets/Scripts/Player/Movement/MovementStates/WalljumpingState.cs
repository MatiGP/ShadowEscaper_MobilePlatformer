using System.Collections;
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

        movementVector.x += walljumpDirection * playerController.JumpOffWallForce;
        movementVector.y -= playerController.WallJumpGravity * Time.deltaTime;

        playerTransform.position += movementVector * Time.deltaTime;
    }

    public override void HandleLogic()
    {
        if (playerController.IsTouchingCeiling)
        {
            stateMachine.ChangeState(playerController.fallingState);
        }       

        if(movementVector.y < 0)
        {
            stateMachine.ChangeState(playerController.fallingState);
        }

        if (currentWallJumpDuration <= playerController.WallJumpDuration) return;

        if(!playerController.IsTouchingGround && (playerController.IsTouchingLeftWall || playerController.IsTouchingRightWall))
        {
            stateMachine.ChangeState(playerController.wallslidingState);
        }
    } 
}
