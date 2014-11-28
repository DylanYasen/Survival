using UnityEngine;
using System.Collections;

[RequireComponent(typeof(DynamicEntity))]

public class DynamicEntityController : MonoBehaviour
{
    public bool m_isFacingLeft { get; protected set; }
    public bool m_isMoving { get; protected set; }
    public bool m_isAttacking { get; protected set; }

    protected Vector2 m_velocity { get; set; }
    protected Animator m_anim { get; private set; }
    //protected Rigidbody2D m_body { get; private set; }
    protected Transform m_trans { get; private set; }
    protected DynamicEntity m_entity { get; private set; }
    protected EntityStats m_stats { get; private set; }

    protected virtual void Awake()
    {
        m_entity = GetComponent<DynamicEntity>();
        m_anim = m_entity.m_anim;
        //m_body = m_entity.m_body;
        m_stats = m_entity.m_stats;
        m_trans = transform;

        m_isFacingLeft = true;
        m_isMoving = false;
        m_isAttacking = false;

    }

    protected void FlipSide()
    {
        Vector3 scale = m_trans.localScale;
        scale.x *= -1;
        m_trans.localScale = scale;

        m_isFacingLeft = !m_isFacingLeft;
    }

}
