using UnityEngine;

public class FallingState : BaseMovementState
{
    private const long VIBRATION_DURATION = 100; 

    public FallingState(CharacterController controller, StateMachine stateMachine, Animator animator) : base(controller, stateMachine, animator)
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
        Vibration.Vibrate(VIBRATION_DURATION);
        ShadowRunApp.Instance.SoundManager.PlaySoundEffect(ESoundType.PLAYER_LANDING);
    }

    public override void HandleAnimator()
    {
        animator.Play("Falling");
    }

    public override void HandleInput()
    {
        movementVector.x = playerController.Direction * playerController.FootSpeed;
        movementVector.y -= playerController.Gravity * playerController.FallMultiplier * Time.deltaTime;

        movementVector.y = Mathf.Clamp(movementVector.y, -playerController.FallingSpeedLimit, playerController.JumpHeight);

        playerTransform.position += movementVector * Time.deltaTime;

        playerController.Flip();
    }

    public override void HandleLogic()
    {
        if (playerController.IsTouchingGround)
        {
            Debug.Log("SHOULD FIX!");

            playerController.FixPlayerGroundPosition();

            if (playerController.Direction == 0)
            {
                stateMachine.ChangeState(playerController.MovementStates[EMovementStateType.Idle]);
            }
            else
            {
                stateMachine.ChangeState(playerController.MovementStates[EMovementStateType.Running]);
            }

        }
        else
        {
            Debug.Log("Player is not touching ground");
        }

        if (playerController.IsTouchingLeftWall || playerController.IsTouchingRightWall)
        {
            playerController.FixPlayerWallPosition();
            movementVector.x = 0;
            stateMachine.ChangeState(playerController.MovementStates[EMovementStateType.Wallsliding]);
        }
    }
}
