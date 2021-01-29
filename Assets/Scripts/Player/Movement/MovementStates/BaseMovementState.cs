using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMovementState
{
    protected PlayerController playerController;
    protected StateMachine stateMachine;
    protected Animator animator;
    protected Transform playerTransform;
    protected Vector3 movementVector;

    public BaseMovementState(PlayerController controller, StateMachine stateMachine, Animator animator)
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
    

}
