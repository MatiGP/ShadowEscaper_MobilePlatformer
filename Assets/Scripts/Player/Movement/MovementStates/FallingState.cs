using System.Collections;
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
        movementVector.y = 0;
    }

    public override void HandleAnimator()
    {
        animator.Play("Falling");
    }

    public override void HandleInput()
    {
        movementVector.x = playerController.Direction * playerController.FootSpeed;
        movementVector.y -= playerController.Gravity * Time.fixedDeltaTime;

        playerTransform.position += movementVector * Time.fixedDeltaTime;

        playerController.Flip();
    }

    public override void HandleLogic()
    {
        if (playerController.IsGrounded)
        {
            if (!playerController.IsJumping)
            {
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
                stateMachine.ChangeState(playerController.jumpingState);
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
