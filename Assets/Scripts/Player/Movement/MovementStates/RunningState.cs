using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : BaseMovementState
{
    bool isRunning;

    public RunningState(PlayerController controller, StateMachine stateMachine, Animator animator) : base(controller, stateMachine, animator)
    {
    }

    public override void Enter()
    {
        Debug.Log("Entering Running State");
        movementVector.x = 0;
    }

    public override void HandleAnimator()
    {
        animator.Play("PlayerRun");
    }

    public override void HandleInput()
    {
        movementVector.x = playerController.FootSpeed * playerController.Direction * Time.deltaTime;

        playerTransform.position += movementVector;

        isRunning = playerController.Direction != 0;

        playerController.Flip();
    }

    public override void HandleLogic()
    {
        if (!isRunning || playerController.IsTouchingLeftWall || playerController.IsTouchingRightWall)
        {
            stateMachine.ChangeState(playerController.idleState);
        }     

        if (playerController.IsJumping)
        {
            stateMachine.ChangeState(playerController.jumpingState);
        }
        else if (!playerController.IsGrounded)
        {
            stateMachine.ChangeState(playerController.fallingState);
        }
    }
}
