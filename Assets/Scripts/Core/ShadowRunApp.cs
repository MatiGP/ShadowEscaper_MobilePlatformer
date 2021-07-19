using Code.StateMachine;
using Code.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowRunApp : MonoBehaviour
{
    public static ShadowRunApp Instance = null;

    [SerializeField] private EMenuState m_StartingState;
    [SerializeField] private UIManager m_UIManager = null;

    private MenuStateMachine m_StateMachnie = null;

    private SaveSystem m_SaveSystem = null;
    

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        m_StateMachnie = new MenuStateMachine();
        m_StateMachnie.Initialize();
        m_StateMachnie.AddState(new MainMenuState());

        InitializeOtherSystems();
    }

    private void Start()
    {
        m_StateMachnie.StartStateMachine(m_StartingState);
    }

    private void Update()
    {
        m_StateMachnie.UpdateState();
    }

    private void InitializeOtherSystems()
    {
        m_SaveSystem = new SaveSystem();
        m_SaveSystem.Initialize();

        m_UIManager.Initialize();
        
    }
}
