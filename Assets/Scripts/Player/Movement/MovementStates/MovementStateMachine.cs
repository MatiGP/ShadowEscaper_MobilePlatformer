using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.StateMachine
{
    public class MovementStateMachine
    {
        public BaseMovementState CurrentState { get; private set; }

        public void Initialize(BaseMovementState startingState)
        {
            CurrentState = startingState;
            CurrentState.Enter();
        }

        public void ChangeState(BaseMovementState state)
        {
            CurrentState.Exit();

            CurrentState = state;
            CurrentState.Enter();
        }
    }
}
