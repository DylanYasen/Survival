using UnityEngine;
using System.Collections;

public class AlertState : AIState
{
    protected AIEntity m_entity;

    private Transform target;

    public AlertState(AIEntity entity, Transform t)
    {

        this.m_entity = entity;

        this.target = t;
    }

    public void Enter()
    {
        Debug.Log("enter alert state");
        m_entity.StartCoroutine("StateExecutor", this.Execute());
    }

    public IEnumerator Execute()
    {
        Debug.Log("alert!!");

        yield return null;

        this.ExitToNextState();
    }

    public void Exit()
    {
        m_entity.StateTerminator(this.Execute());
        Debug.Log("exit alert state.");
    }

    public void ExitToNextState()
    {
        m_entity.FSM.ChangeState(new ChaseState(m_entity, target), false);
    }

}
