using UnityEngine;
using System.Collections;

public class ChaseState : AIState
{
    protected AIEntity m_entity;

    private Transform targetTrans;
    private Transform m_trans;

    public ChaseState(AIEntity entity, Transform target)
    {
        this.m_entity = entity;
        this.m_trans = m_entity.transform;
        this.targetTrans = target;
    }

    public void Enter()
    {
        Debug.Log("enter ChaseState state");

        m_entity.StartCoroutine("StateExecutor", this.Execute());
    }

    public IEnumerator Execute()
    {
        Debug.Log("chasing!!");

        while (Vector2.Distance(m_trans.position, targetTrans.position) > m_entity.AttackRange)
        {
            m_entity.Chase(targetTrans);
            yield return null;
        }

        this.ExitToNextState();
    }

    public void ExitToNextState()
    {
        m_entity.FSM.ChangeState(new AttackState(m_entity, targetTrans), false);
    }

    public void Exit()
    {
        m_entity.StateTerminator(this.Execute());
        Debug.Log("exit Chase state");
    }

}
