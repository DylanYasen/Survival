using UnityEngine;
using System.Collections;

public class AttackState : AIState
{
    protected AIEntity m_entity;

    private Transform m_trans;
    private Transform targetTrans;

    public AttackState(AIEntity entity, Transform t)
    {
        this.m_entity = entity;
        this.m_trans = m_entity.transform;
        this.targetTrans = t;
    }

    public void Enter()
    {
        Debug.Log("enter attack state");

        m_entity.StartCoroutine("StateExecutor", this.Execute());
    }

    public IEnumerator Execute()
    {

        while (Vector2.Distance(m_trans.position, targetTrans.position) < m_entity.AttackRange)
        {
            m_entity.Attack();
            Debug.Log("bite!" + Time.time);

            // or set attack cool down here
            yield return new WaitForSeconds(m_entity.AttackCoolDown);

            //yield return null;
        }

        this.ExitToNextState();
    }

    public void Exit()
    {
        m_entity.StateTerminator(this.Execute());
        Debug.Log("exit attack state");
    }

    public void ExitToNextState()
    {
        Debug.Log("exit to idle");

        m_entity.FSM.ChangeState(new IdleState(m_entity), false);
    }
}
