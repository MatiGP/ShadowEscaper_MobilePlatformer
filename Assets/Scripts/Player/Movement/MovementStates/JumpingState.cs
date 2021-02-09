using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : BaseMovementState
{
    public JumpingState(PlayerController controller, StateMachine stateMachine, Animator animator) : base(controller, stateMachine, animator)
    {

    }

    public override void Enter()
    {
        Debug.Log("Entering Jumping State");
        movementVector.x = 0;
        movementVector.y = playerController.JumpHeight;
    }

    public override void Exit()
    {
        movementVector.x = 0;
        movementVector.y = 0;
    }

    public override void HandleAnimator()
    {
        animator.Play("IncreasingHeight");
    }

    public override void HandleInput()
    {
        movementVector.x = playerController.Direction * playerController.FootSpeed;
        movementVector.y -= playerController.Gravity * Time.deltaTime;

        playerTransform.position += movementVector * Time.deltaTime;

        playerController.Flip();
    }

    public override void HandleLogic()
    {
        if (playerController.IsTouchingGround)
        {
            if (playerController.Direction != 0)
            {
                stateMachine.ChangeState(playerController.runningState);
            }
            else
            {
                stateMachine.ChangeState(playerController.idleState);
            }            
        }

        if(playerController.IsTouchingLeftWall || playerController.IsTouchingRightWall)
        {
            stateMachine.ChangeState(playerController.wallslidingState);
        }
      
        if(movementVector.y <= 0)
        {
            playerController.SetNormalFallMultiplier();
            stateMachine.ChangeState(playerController.fallingState);
        }

        if((movementVector.y > 0) && !playerController.IsJumping)
        {
            playerController.SetQuickFallMultiplier();
            playerController.SetJumpRemainingForce(movementVector.y);
            Debug.Log(movementVector.y);
            stateMachine.ChangeState(playerController.fallingState);
        }
    }

    
}
