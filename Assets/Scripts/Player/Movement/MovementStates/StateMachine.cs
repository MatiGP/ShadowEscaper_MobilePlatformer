using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public BaseMovementState currentState;

    public void Initialize(BaseMovementState startingState)
    {
        currentState = startingState;
        currentState.Enter();
    }

    public void ChangeState(BaseMovementState state)
    {      
        currentState = state;
        currentState.Enter();
    }
}
