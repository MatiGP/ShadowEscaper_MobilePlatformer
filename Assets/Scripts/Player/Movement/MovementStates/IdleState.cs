using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.StateMachine
{
    public class IdleState : BaseMovementState
    {
        bool IsRunning;

        public IdleState(CharacterController controller, MovementStateMachine stateMachine, Animator animator) : base(controller, stateMachine, animator)
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
                stateMachine.ChangeState(playerController.MovementStates[EMovementStateType.Jumping]);
            }

            bool IsNotTouchingAnyWall = (playerController.Direction < -0.1 && !playerController.IsTouchingLeftWall)
                || (playerController.Direction > 0.1 && !playerController.IsTouchingRightWall);

            if (IsRunning && IsNotTouchingAnyWall)
            {
                stateMachine.ChangeState(playerController.MovementStates[EMovementStateType.Running]);
            }

            if (playerController.IsSliding)
            {
                stateMachine.ChangeState(playerController.MovementStates[EMovementStateType.Groundsliding]);
            }
        }
    }
}
