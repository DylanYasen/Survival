using UnityEngine;
using System.Collections;

public class EnemyController : DynamicEntityController
{
    public void MoveInRandDir()
    {
        m_anim.SetBool("walking", true);
        //m_body.velocity = Utility.GetRandUnitVec(Utility.GetRandomDegInRad()) * m_stats.MoveSpeed;
    }

    public void Idle()
    {
        m_anim.SetBool("walking", false);
        //m_body.velocity = Vector2.zero;
    }

    public void TurnAround()
    {
        Debug.Log("turn");

        if (!m_isAttacking)
            MoveInRandDir();
    }

    public void MeleeAttack()
    {
        // player attack anim
        // deal damage
        // refresh status
        // toggle isAttacking in anim frame

        m_isAttacking = true;

        // change to attack anim
        m_anim.SetBool("walking", true);

        // 1. might be better to use anim frame to trigger
        // 2. need to randomize damage
        Player.instance.m_stats.LoseHP(m_stats.DMG);
    }

    public void Chase(Transform target, int speedMultiplier = 1)
    {
        Vector3 dir = (target.position - m_trans.position).normalized;

        //m_body.MovePosition(target.position * m_stats.MoveSpeed * Time.deltaTime);

        //m_body.velocity = dir * m_stats.MoveSpeed * speedMultiplier;

        m_anim.SetBool("walking", true);
    }

}
