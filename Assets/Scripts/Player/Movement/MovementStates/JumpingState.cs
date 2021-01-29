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

    public override void HandleAnimator()
    {
        animator.Play("IncreasingHeight");
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
            if (playerController.Direction != 0)
            {
                stateMachine.ChangeState(playerController.runningState);
            }
            else
            {
                stateMachine.ChangeState(playerController.idleState);
            }
            
        }
      
        if(movementVector.y < 0)
        {
            stateMachine.ChangeState(playerController.fallingState);
        }

    }
}
