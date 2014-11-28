using UnityEngine;
using System.Collections;

public class PatrolState : AIState
{
    protected AIEntity m_entity;

    //private Transform patrolPoint;

    private float counter;
    private int timer;

    public PatrolState(AIEntity entity, Transform t)
    {
        this.m_entity = entity;
        //this.patrolPoint = t;

        timer = Random.Range(2, 6);
    }

    public void Enter()
    {
        Debug.Log("enter patrol state");
        
        m_entity.StartCoroutine("StateExecutor", this.Execute());

        m_entity.Patrol();
    }

    public IEnumerator Execute()
    {
        /*
        while (m_entity.transform.position != patrolPoint.position)
        {
            m_entity.transform.position = Vector2.MoveTowards(m_entity.transform.position, patrolPoint.position, Time.deltaTime);
            yield return null;
        }
         */

        while (counter < timer)
        {
            counter += Time.deltaTime;
            yield return null;
        }

        this.ExitToNextState();
        Debug.Log("exit patrol state");
    }

    public void Exit()
    {
        m_entity.StateTerminator(this.Execute());
        Debug.Log("exit patrol state");
    }

    public void ExitToNextState()
    {
        Debug.Log("exit from patrol state to next");
        m_entity.FSM.ChangeState(new IdleState(m_entity), false);
    }
}
