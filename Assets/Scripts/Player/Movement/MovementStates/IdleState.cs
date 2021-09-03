using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseMovementState
{
    bool IsRunning;

    public IdleState(PlayerController controller, StateMachine stateMachine, Animator animator) : base(controller, stateMachine, animator)
    {
    }

    public override void Enter()
    {

    }

    public override void Exit()
    {

    }

    public override void HandleAnimator()
    {
        animator.Play("PlayerIdle");
    }

    public override void HandleInput()
    {
        IsRunning = playerController.Direction != 0;
    }

    public override void HandleLogic()
    {
        if (playerController.IsJumping)
        {
            stateMachine.ChangeState(playerController.jumpingState);
        }

        bool IsNotTouchingAnyWall = (playerController.Direction < -0.1 && !playerController.IsTouchingLeftWall)
            || (playerController.Direction > 0.1 && !playerController.IsTouchingRightWall);
        
        if (IsRunning && IsNotTouchingAnyWall)
        {
            stateMachine.ChangeState(playerController.runningState);
        }
    }
}
