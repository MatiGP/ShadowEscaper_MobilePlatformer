using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code
{
    public class TutorialController : CharacterController
    {
        public void SetDirection(float dir)
        {
            direction = dir;
        }

        protected override void Awake()
        {
            base.Awake();
            playerAnimator.Play("Spawn");
        }

        protected override void SetUpStates()
        {
            base.SetUpStates();
            stateMachine.Initialize(MovementStates[EMovementStateType.Idle]);
        }

        public void EnableAnimations()
        {
            stateMachine.Initialize(MovementStates[EMovementStateType.Idle]);
        }
    }
}
