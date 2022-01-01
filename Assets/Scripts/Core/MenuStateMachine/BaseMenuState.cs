using Code.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMenuState
{
    public EMenuState State => m_StateType;
    private EMenuState m_StateType;
    public BaseMenuState(EMenuState menuStateType)
    {
        m_StateType = menuStateType;
        
    }
    public abstract void EnterState();
    public abstract void LeaveState();
    public abstract void UpdateState();
}

public enum EMenuState
{
    MainMenu,
    Game,
    Tutorial
}
