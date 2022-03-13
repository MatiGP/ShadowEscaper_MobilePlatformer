using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.StateMachine
{
    public abstract class BaseMovementState
    {
        protected CharacterController playerController;
        protected MovementStateMachine stateMachine;
        protected Animator animator;
        protected Transform playerTransform;
        protected Vector3 movementVector;

        public BaseMovementState(CharacterController controller, MovementStateMachine stateMachine, Animator animator)
        {
            playerController = controller;
            this.stateMachine = stateMachine;
            this.animator = animator;
            playerTransform = playerController.transform;
        }

        public abstract void Enter();
        public abstract void HandleInput();
        public abstract void HandleAnimator();
        public abstract void HandleLogic();
        public abstract void Exit();
    }
}
