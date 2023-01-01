using UnityEngine;

namespace Code.StateMachine
{
    public class FallingState : BaseMovementState
    {
        private const long VIBRATION_DURATION = 50;

        public FallingState(CharacterController controller, MovementStateMachine stateMachine, Animator animator) : base(controller, stateMachine, animator)
        {
        }

        public override void Enter()
        {
            movementVector.y = playerController.RemainingJumpForce;
        }

        public override void Exit()
        {
            movementVector.y = 0;
            movementVector.x = 0;
            playerController.SetJumpRemainingForce(0);
            playerController.ResetCoyoteTime();
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
            movementVector.y -= playerController.Gravity * playerController.FallMultiplier;

            movementVector.y = Mathf.Clamp(movementVector.y, -playerController.FallingSpeedLimit, playerController.JumpHeight);

            playerTransform.position += movementVector * Time.deltaTime;

            playerController.Flip();
        }

        public override void HandleLogic()
        {
            if (playerController.CanCoyoteJump && movementVector.y < 0f)
            {
                stateMachine.ChangeState(playerController.MovementStates[EMovementStateType.Jumping]);
                return;
            }
            
            if (playerController.IsTouchingGround)
            {
                playerController.FixPlayerGroundPosition();

                if (playerController.Direction == 0)
                {
                    stateMachine.ChangeState(playerController.MovementStates[EMovementStateType.Idle]);
                }
                else
                {
                    stateMachine.ChangeState(playerController.MovementStates[EMovementStateType.Running]);
                }

                return;
            }
           
            if (playerController.IsTouchingLeftWall || playerController.IsTouchingRightWall)
            {
                playerController.FixPlayerWallPosition();
                movementVector.x = 0;
                stateMachine.ChangeState(playerController.MovementStates[EMovementStateType.Wallsliding]);
            }
        }
    }
}
