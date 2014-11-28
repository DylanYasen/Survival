using UnityEngine;
using System.Collections;

public class MeleeSimpleAI : AIEntity
{
    protected override void Update()
    {
        base.Update();

        // add type-specific behaviors
    }

    public override void Alert()
    {
        Debug.Log("this is melee simple alert");
    }

    public override void Attack()
    {
        //Debug.Log("this is melee simple attack");

        // change later
        //m_anim.SetBool("walking", true);

        m_controller.MeleeAttack();

        // remove anim reference from generic class
        // should update anim in controller
    }

    public override void UnderAttack()
    {
        Debug.Log("this is melee simple under attack");
    }

    public override void Patrol()
    {
        Debug.Log("this is melee simple patrol");
        m_controller.MoveInRandDir();
    }

    public override void Idle()
    {
        Debug.Log("this is melee simple idle");
        m_controller.Idle();
    }

    public override void Chase(Transform target)
    {
        Debug.Log("this is melee simple chase");
        m_controller.Chase(target, 2);
    }
}
