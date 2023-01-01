using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.StateMachine
{
    public class IdleState : BaseMovementState
    {
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
        }

        public override void HandleLogic()
        {
            if (playerController.IsJumping)
            {
                stateMachine.ChangeState(playerController.MovementStates[EMovementStateType.Jumping]);
                return;
            }
            
            if (playerController.IsRunning && playerController.CanMakeMove)
            {
                stateMachine.ChangeState(playerController.MovementStates[EMovementStateType.Running]);
                return;
            }

            if (playerController.IsSliding)
            {
                stateMachine.ChangeState(playerController.MovementStates[EMovementStateType.Groundsliding]);
                return;
            }

            if (!playerController.IsTouchingGround)
            {
                stateMachine.ChangeState(playerController.MovementStates[EMovementStateType.Falling]);
            }
        }
    }
}
