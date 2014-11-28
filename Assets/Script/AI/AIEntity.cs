using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Enemy))]

public class AIEntity : MonoBehaviour
{
    public StateMachine FSM { get; private set; }

    public Transform target { get; set; }
    public Transform patrolPoint { get; set; }

    public float AlertRange = 5;
    public float AttackRange = 1;
    public float AttackCoolDown = 1;

    public Enemy m_entity { get; private set; }
    public EntityStats m_stat { get; private set; }
    public EnemyController m_controller { get; private set; }
    public float disToTarget { get; private set; }
    // public Animator m_anim { get; private set; }

    protected void Awake()
    {
        FSM = new StateMachine(this);
        m_entity = GetComponent<Enemy>();
        m_stat = m_entity.m_stats;
        // m_anim = m_entity.m_anim;
        m_controller = m_entity.m_controller;

        Debug.Log(m_entity);
        // Debug.Log(m_anim);
    }

    protected virtual void Start()
    {
        FSM.ChangeState(new IdleState(this), true);
    }

    protected virtual void Update()
    {
        // no target
        if (target == null)
        {
            if (!(FSM.currentState is IdleState) && !(FSM.currentState is PatrolState))
                FSM.ChangeState(new IdleState(this), true);

            return;
        }

        disToTarget = Vector2.Distance(transform.position, target.position);

        // trigger alert state
        if (disToTarget < AlertRange)
        {
            if (!(FSM.currentState is AlertState) && !(FSM.currentState is AttackState) && !(FSM.currentState is ChaseState))
            {
                FSM.ChangeState(new AlertState(this, target), true);
            }
        }

        // trigger idle state
        if (disToTarget > AlertRange)
        {
            if (!(FSM.currentState is IdleState) && !(FSM.currentState is PatrolState))
            {
                FSM.ChangeState(new IdleState(this), true);
            }
        }
    }

    public IEnumerator StateExecutor(IEnumerator state)
    {
        while (state.MoveNext())
            yield return state.Current;
    }

    public void StateTerminator(IEnumerator state)
    {
        // change this later
        // this will mess up other irrelevant coroutines
        StopAllCoroutines();
    }

    public virtual void Idle()
    {
        Debug.Log("ai base idle");
    }

    public virtual void Attack()
    {
        Debug.Log("ai base attack");
    }

    public virtual void UnderAttack()
    {
        Debug.Log("ai base under attack");
    }

    public virtual void Alert()
    {
        Debug.Log("ai base alert");
    }

    public virtual void Patrol()
    {
        Debug.Log("ai base patrol");
    }

    public virtual void Chase(Transform target)
    {
        Debug.Log("ai base Chase");
    }
}
