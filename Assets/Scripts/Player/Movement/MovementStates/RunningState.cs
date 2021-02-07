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
        movementVector.x = 0;

    }

    public override void Exit()
    {
        movementVector.x = 0;

    }

    public override void HandleAnimator()
    {
        animator.Play("PlayerRun");
    }

    public override void HandleInput()
    {
        movementVector.x = playerController.FootSpeed * playerController.Direction;

        playerTransform.position += movementVector * Time.deltaTime;

        isRunning = playerController.Direction != 0;

        playerController.Flip();
    }

    public override void HandleLogic()
    {
        if (isRunning && (playerController.IsTouchingLeftWall || playerController.IsTouchingRightWall))
        {
            stateMachine.ChangeState(playerController.idleState);
        }
        else if (!isRunning)
        {
            stateMachine.ChangeState(playerController.idleState);
        }     

        if (playerController.IsJumping)
        {
            stateMachine.ChangeState(playerController.jumpingState);
        }
        else if (!playerController.IsTouchingGround)
        {
            stateMachine.ChangeState(playerController.fallingState);
        }
    }
}
