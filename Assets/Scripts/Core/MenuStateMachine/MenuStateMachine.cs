using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.StateMachine {
    public class MenuStateMachine
    {
        private Dictionary<EMenuState, BaseMenuState> m_MenuStates;

        private BaseMenuState m_CurrentState = null;

        public void Initialize()
        {
            m_MenuStates = new Dictionary<EMenuState, BaseMenuState>();
        }

        public void AddState(BaseMenuState newState)
        {
            m_MenuStates.Add(newState.State, newState);
        }

        public void UpdateState()
        {
            m_CurrentState.UpdateState();
        }

        public void StartStateMachine(EMenuState startingState)
        {
            m_CurrentState = m_MenuStates[startingState];
            m_CurrentState.EnterState();
        }

        public void ChangeState(EMenuState newState)
        {
            m_CurrentState.LeaveState();
            StartStateMachine(newState);
        }
    }
}
