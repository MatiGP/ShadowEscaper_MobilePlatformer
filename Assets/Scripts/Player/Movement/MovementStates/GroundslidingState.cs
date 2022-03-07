using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundslidingState : BaseMovementState
{
    private float m_Direction = 0;
    private float currentSpeed = 0;
    private const float MIN_SLIDE_SPEED = 2f;
    private const float MAX_SLIDE_SPEED = 90f;

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
        currentSpeed -= Mathf.Clamp(playerController.SlideSpeedFallOff * Time.deltaTime, MIN_SLIDE_SPEED, MAX_SLIDE_SPEED);

        movementVector.x = currentSpeed * m_Direction * Time.deltaTime;

        playerTransform.position += movementVector;

        Vibration.Vibrate();
    }

    public override void HandleLogic()
    {
        if (playerController.IsTouchingCeiling)
        {
            return;
        }

        if (!playerController.IsSliding)
        {
            stateMachine.ChangeState(playerController.MovementStates[EMovementStateType.Idle]);
        }

        if(playerController.IsTouchingWallWhileSliding)
        {
            stateMachine.ChangeState(playerController.MovementStates[EMovementStateType.Idle]);
        }      

        if (!playerController.IsTouchingGround)
        {
            stateMachine.ChangeState(playerController.MovementStates[EMovementStateType.Falling]);
        }
    }
}
