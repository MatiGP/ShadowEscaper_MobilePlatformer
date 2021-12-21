using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallslidingState : BaseMovementState
{
    public WallslidingState(PlayerController controller, StateMachine stateMachine, Animator animator) : base(controller, stateMachine, animator)
    {
    }

    public override void Enter()
    {
        playerController.FixPlayerWallPosition();
        Debug.Log("Entering Wallsliding state");
        playerController.AdjustPlayerDirection();
    }

    public override void Exit()
    {
        movementVector.y = 0;
    }

    public override void HandleAnimator()
    {
        animator.Play("WallSlide");
    }

    public override void HandleInput()
    {
        movementVector.y = -playerController.WallSlideSpeed;
        
        playerTransform.position += movementVector * Time.deltaTime;
    }

    public override void HandleLogic()
    {
        if (playerController.IsJumping)
        {
            stateMachine.ChangeState(playerController.WallJumpingState);
        }

        if(!playerController.IsTouchingGround && !(playerController.IsTouchingLeftWall || playerController.IsTouchingRightWall))
        {
            
            stateMachine.ChangeState(playerController.FallingState);
        }

        if(playerController.IsTouchingGround)
        {
            playerController.FixPlayerGroundPosition();
            stateMachine.ChangeState(playerController.IdleState);
        }

        
    }

    
}
