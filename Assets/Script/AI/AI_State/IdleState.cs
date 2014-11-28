using UnityEngine;
using System.Collections;

public class IdleState : AIState
{
    protected AIEntity m_entity;

    private float counter;
    private int timer;

    public IdleState(AIEntity entity)
    {
        this.m_entity = entity;

        timer = Random.Range(2, 4);
    }

    public void Enter()
    {
        Debug.Log("enter idle state.");

        m_entity.Idle();

        m_entity.StartCoroutine("StateExecutor", this.Execute());
    }

    public IEnumerator Execute()
    {
        Debug.Log("idling");

        while (counter < timer)
        {
            counter += Time.deltaTime;
            yield return null;
        }

        this.ExitToNextState();
    }

    public void Exit()
    {
        m_entity.StateTerminator(this.Execute());
        Debug.Log("exit idle state");
    }

    public void ExitToNextState()
    {
        m_entity.FSM.ChangeState(new PatrolState(m_entity, m_entity.patrolPoint), false);
    }

}
