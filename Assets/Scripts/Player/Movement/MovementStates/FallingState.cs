using UnityEngine;

public class FallingState : BaseMovementState
{
    public FallingState(PlayerController controller, StateMachine stateMachine, Animator animator) : base(controller, stateMachine, animator)
    {
    }

    public override void Enter()
    {
        movementVector.y = playerController.RemainingJumpForce;
        playerController.InterruptJumping();
    }

    public override void Exit()
    {
        movementVector.y = 0;
        movementVector.x = 0;
        playerController.SetJumpRemainingForce(0);
    }

    public override void HandleAnimator()
    {
        animator.Play("Falling");
    }

    public override void HandleInput()
    {
        movementVector.x = playerController.Direction * playerController.FootSpeed;
        movementVector.y -= playerController.Gravity * playerController.FallMultiplier * Time.deltaTime;

        movementVector.y = Mathf.Clamp(movementVector.y, -playerController.FallingSpeedLimit, 900);

        playerTransform.position += movementVector * Time.deltaTime;

        playerController.Flip();
    }

    public override void HandleLogic()
    {
        if (playerController.IsTouchingGround)
        {
            playerController.FixPlayerGroundPosition();

            if (playerController.Direction == 0)
            {
                stateMachine.ChangeState(playerController.IdleState);
            }
            else
            {
                stateMachine.ChangeState(playerController.RunningState);
            }

        }

        if (playerController.IsTouchingLeftWall || playerController.IsTouchingRightWall)
        {
            playerController.FixPlayerWallPosition();
            movementVector.x = 0;
            stateMachine.ChangeState(playerController.WallSlidingState);
        }
    }
}
