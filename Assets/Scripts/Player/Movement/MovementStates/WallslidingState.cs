using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.StateMachine { 
public class WallslidingState : BaseMovementState
{
    public WallslidingState(CharacterController controller, MovementStateMachine stateMachine, Animator animator) : base(controller, stateMachine, animator)
    {
    }

    public override void Enter()
    {
        playerController.FixPlayerWallPosition();
        playerController.AdjustPlayerDirection();
        playerController.PersistantParticles.SetEnabledParticleForMovementState(EMovementStateType.Wallsliding, true);
    }

    public override void Exit()
    {
        movementVector.y = 0;
        playerController.ClearDirection();
        playerController.PersistantParticles.SetEnabledParticleForMovementState(EMovementStateType.Wallsliding, false);
    }

    public override void HandleAnimator()
    {
        animator.Play("WallSlide");
    }

    public override void HandleInput()
    {
        movementVector.y = -playerController.WallSlideSpeed;

        playerTransform.position += movementVector * Time.deltaTime;
    }

    public override void HandleLogic()
    {
        if (playerController.IsJumping)
        {
            stateMachine.ChangeState(playerController.MovementStates[EMovementStateType.Walljumping]);
        }

        if (!playerController.IsTouchingGround && !(playerController.IsTouchingLeftWall || playerController.IsTouchingRightWall))
        {

            stateMachine.ChangeState(playerController.MovementStates[EMovementStateType.Falling]);
        }

        if (playerController.IsTouchingGround)
        {
            playerController.FixPlayerGroundPosition();
            stateMachine.ChangeState(playerController.MovementStates[EMovementStateType.Idle]);
        }


    }

}

}
