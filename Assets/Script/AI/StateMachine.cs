using UnityEngine;
using System.Collections;

public interface AIState
{
    void Enter();
    System.Collections.IEnumerator Execute();
    void ExitToNextState();
    void Exit();
}

public class StateMachine
{
    public AIState currentState { get; private set; }

    private AIEntity aiEntity;

    public StateMachine(AIEntity entity)
    {
        this.aiEntity = entity;
    }

    public void ChangeState(AIState newState, bool interruptCurrentState)
    {
        if (interruptCurrentState)
        {
            if (this.currentState != null)
            {
                this.currentState.Exit();
            }
        }

        this.currentState = newState;
        this.currentState.Enter();
    }
}
