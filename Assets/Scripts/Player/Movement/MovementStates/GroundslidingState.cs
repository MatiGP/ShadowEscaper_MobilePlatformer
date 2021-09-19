using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundslidingState : BaseMovementState
{
    float m_Direction = 0;
    float currentSpeed = 0;

    public GroundslidingState(PlayerController controller, StateMachine stateMachine, Animator animator) : base(controller, stateMachine, animator)
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
    }

    public override void Exit()
    {
        animator.Play("SlideEnd");
        movementVector = Vector3.zero;

        playerController.SetNormalCollisionSize();
        playerController.SetSlideCooldown();

        playerController.InterruptSliding();
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


    }

    public override void HandleLogic()
    {
        if (!playerController.IsSliding)
        {
            stateMachine.ChangeState(playerController.IdleState);
        }

        if(playerController.IsTouchingWallWhileSliding)
        {
            stateMachine.ChangeState(playerController.IdleState);
        }

        if(currentSpeed <= 0.5f)
        {
            stateMachine.ChangeState(playerController.IdleState);
        }

        if (!playerController.IsTouchingGround)
        {
            stateMachine.ChangeState(playerController.FallingState);
        }
    }
}
