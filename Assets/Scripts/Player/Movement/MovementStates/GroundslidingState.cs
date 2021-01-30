using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundslidingState : BaseMovementState
{
    public GroundslidingState(PlayerController controller, StateMachine stateMachine, Animator animator) : base(controller, stateMachine, animator)
    {
    }

    public override void Enter()
    {
        Debug.Log("Entering Ground Sliding State");
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }

    public override void HandleAnimator()
    {
        throw new System.NotImplementedException();
    }

    public override void HandleInput()
    {
        throw new System.NotImplementedException();
    }

    public override void HandleLogic()
    {
        throw new System.NotImplementedException();
    }
}
