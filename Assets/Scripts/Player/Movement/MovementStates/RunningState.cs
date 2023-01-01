using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.StateMachine
{ 
public class RunningState : BaseMovementState
{
    public RunningState(CharacterController controller, MovementStateMachine stateMachine, Animator animator) : base(controller, stateMachine, animator)
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

        playerController.Flip();
    }

        public override void HandleLogic()
        {
            if (playerController.IsRunning && (playerController.IsTouchingLeftWall || playerController.IsTouchingRightWall))
            {
                stateMachine.ChangeState(playerController.MovementStates[EMovementStateType.Idle]);
                return;
            }
            
            if (!playerController.IsRunning)
            {
                stateMachine.ChangeState(playerController.MovementStates[EMovementStateType.Idle]);
                return;
            }

            if (playerController.IsSliding)
            {
                stateMachine.ChangeState(playerController.MovementStates[EMovementStateType.Groundsliding]);
                return;
            }

            if (playerController.IsJumping)
            {
                stateMachine.ChangeState(playerController.MovementStates[EMovementStateType.Jumping]);
                return;
            }
            
            if (!playerController.IsTouchingGround)
            {
                stateMachine.ChangeState(playerController.MovementStates[EMovementStateType.Falling]);
            }

        }
    }
}
