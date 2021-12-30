using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundslidingState : BaseMovementState
{
    float m_Direction = 0;
    float currentSpeed = 0;

    public GroundslidingState(CharacterController controller, StateMachine stateMachine, Animator animator) : base(controller, stateMachine, animator)
    {
    }

    public override void Enter()
    {
        animator.Play("SlideStart");

        playerController.ModifySlidingDetectorPosition();
        playerController.SetSlidingCollisionSize();
        
        m_Direction = playerController.IsFacingRight ? 1 : -1;
        movementVector = Vector3.zero;

        currentSpeed = playerController.SlideSpeed;

        ShadowRunApp.Instance.SoundManager.PlaySoundEffect(ESoundType.PLAYER_SLIDE);
        playerController.PersistantParticles.SetEnabledParticleForMovementState(EMovementStateType.Groundsliding, true);
    }

    public override void Exit()
    {
        animator.Play("SlideEnd");
        movementVector = Vector3.zero;

        playerController.SetNormalCollisionSize();
        playerController.SetSlideCooldown();

        playerController.InterruptSliding();

        playerController.PersistantParticles.SetEnabledParticleForMovementState(EMovementStateType.Groundsliding, false);
    }

    public override void HandleAnimator()
    {
        animator.Play("SlideLoop");
    }

    public override void HandleInput()
    {
        currentSpeed -= playerController.SlideSpeedFallOff * Time.deltaTime;

        movementVector.x = currentSpeed * m_Direction * Time.deltaTime;

        playerTransform.position += movementVector;

        Vibration.Vibrate();
    }

    public override void HandleLogic()
    {
        if (!playerController.IsSliding)
        {
            stateMachine.ChangeState(playerController.MovementStates[EMovementStateType.Idle]);
        }

        if(playerController.IsTouchingWallWhileSliding)
        {
            stateMachine.ChangeState(playerController.MovementStates[EMovementStateType.Idle]);
        }

        if(currentSpeed <= 0.5f)
        {
            stateMachine.ChangeState(playerController.MovementStates[EMovementStateType.Idle]);
        }

        if (!playerController.IsTouchingGround)
        {
            stateMachine.ChangeState(playerController.MovementStates[EMovementStateType.Falling]);
        }
    }
}
