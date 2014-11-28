using UnityEngine;
using System.Collections;

public class Enemy : DynamicEntity
{
    public EnemyController m_controller { get; private set; }

    public bool m_isCollidingPlayer { get; private set; }

    protected AIEntity m_aiEntity { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        m_controller = gameObject.AddComponent<EnemyController>();
        m_aiEntity = GetComponent<AIEntity>();
        gameObject.tag = "Enemy";
    }

    protected virtual void Start()
    {
        m_aiEntity.target = Player.instance.transform;
    }

    protected override void OnCollisionEnter(Collision other)
    {
        // maybe move to base class?
        /*collidedObj = other.gameObject;
        collidedObjTag = collidedObj.tag;
        collidedObjLayer = collidedObj.layer;
        collidedEntity = collidedObj.GetComponent<Entity>();
        */

        if (collidedObjLayer == LayerMask.NameToLayer("obstacle"))
        {
            Debug.Log("hit obstacle");

            m_controller.TurnAround();
        }

        if (collidedObjTag == "Player")
            m_isCollidingPlayer = true;
    }

    protected override void OnCollisionExit(Collision other)
    {
        // maybe move to base class?
        collidedObj = other.gameObject;
        collidedObjTag = collidedObj.tag;
        collidedObjLayer = collidedObj.layer;
        collidedEntity = collidedObj.GetComponent<Entity>();

        if (collidedObjTag == "Player")
            m_isCollidingPlayer = false;
    }
}
