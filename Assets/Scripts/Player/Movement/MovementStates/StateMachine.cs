﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public BaseMovementState CurrentState { get; private set; }

    public void Initialize(BaseMovementState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(BaseMovementState state)
    {
        Debug.Log("Exiting : " + CurrentState.ToString());

        CurrentState.Exit();

        CurrentState = state;
        CurrentState.Enter();

        Debug.Log("Entering : " + CurrentState.ToString());
    }
}
