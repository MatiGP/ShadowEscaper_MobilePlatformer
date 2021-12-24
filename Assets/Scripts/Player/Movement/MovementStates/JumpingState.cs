﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : BaseMovementState
{
    private const long VIBRATION_DURATION = 100;

    public JumpingState(PlayerController controller, StateMachine stateMachine, Animator animator) : base(controller, stateMachine, animator)
    {

    }

    public override void Enter()
    {
        movementVector.x = 0;
        movementVector.y = playerController.JumpHeight;
        Vibration.Vibrate(VIBRATION_DURATION);
        
    }

    public override void Exit()
    {
        movementVector.x = 0;
        movementVector.y = 0;
    }

    public override void HandleAnimator()
    {
        animator.Play("IncreasingHeight");
    }

    public override void HandleInput()
    {
        movementVector.x = playerController.Direction * playerController.FootSpeed;
        movementVector.y -= playerController.Gravity * Time.deltaTime;

        playerTransform.position += movementVector * Time.deltaTime;

        playerController.Flip();
    }

    public override void HandleLogic()
    {       
        if(playerController.IsTouchingLeftWall || playerController.IsTouchingRightWall)
        {
            playerController.FixPlayerWallPosition();
            movementVector.x = 0;
        }
        
        if (playerController.IsTouchingCeiling)
        {
            playerController.SetJumpRemainingForce(movementVector.y);
            stateMachine.ChangeState(playerController.FallingState);
        }
      
        if(movementVector.y <= 0)
        {
            playerController.SetNormalFallMultiplier();
            stateMachine.ChangeState(playerController.FallingState);
        }

        if((movementVector.y > 0) && !playerController.IsJumping)
        {
            playerController.SetQuickFallMultiplier();
            playerController.SetJumpRemainingForce(movementVector.y);            
            stateMachine.ChangeState(playerController.FallingState);
        }
        
    }

    
}
