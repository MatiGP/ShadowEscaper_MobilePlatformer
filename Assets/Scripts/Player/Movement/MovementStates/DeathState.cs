using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.StateMachine
{
    public class DeathState : BaseMovementState
    {
        public DeathState(CharacterController controller, MovementStateMachine stateMachine, Animator animator) : base(controller, stateMachine, animator)
        {
        }

        public override void Enter()
        {
            playerController.SetJumpRemainingForce(0);
        }

        public override void Exit()
        {

        }

        public override void HandleAnimator()
        {
            animator.Play("Death");
        }

        public override void HandleInput()
        {

        }

        public override void HandleLogic()
        {

        }
    }
}
