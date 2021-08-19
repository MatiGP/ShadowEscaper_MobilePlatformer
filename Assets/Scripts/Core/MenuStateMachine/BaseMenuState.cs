using Code.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMenuState
{
    public EMenuState State => stateType;
    private EMenuState stateType;
    public BaseMenuState(EMenuState menuStateType)
    {
        stateType = menuStateType;
        
    }
    public abstract void EnterState();
    public abstract void LeaveState();
    public abstract void UpdateState();
}

public enum EMenuState
{
    MainMenu,
    Game
}
