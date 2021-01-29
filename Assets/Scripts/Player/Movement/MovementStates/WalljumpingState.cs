using System.Collections;
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
        Debug.Log("Entering Walljumping State");
        movementVector.x = 0;
        movementVector.y = playerController.WallJumpHeight;
        wallJumpXDirection = playerController.IsFacingRight ? -1 : 1;
    }

    public override void HandleAnimator()
    {
        animator.Play("WallJump");
    }

    public override void HandleInput()
    {
        
        movementVector.x = wallJumpXDirection * playerController.FootSpeed;
        movementVector.y -= playerController.Gravity * Time.deltaTime;

        playerTransform.position += movementVector * Time.deltaTime;

        playerController.Flip();
    }

    public override void HandleLogic()
    {
        if(movementVector.y < 0)
        {
            stateMachine.ChangeState(playerController.fallingState);
        }

        if(!playerController.IsGrounded && (playerController.IsTouchingLeftWall || playerController.IsTouchingRightWall))
        {
            stateMachine.ChangeState(playerController.wallslidingState);
        }
    }
}
