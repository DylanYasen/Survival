using UnityEngine;
using System.Collections;

public abstract class DynamicEntity : Entity
{
    /* abstract out a generic controller class later */
    // public DynamicEntityController m_controller { get; private set; } 

    public PhotonView m_photonView;

    protected override void Awake()
    {
        base.Awake();


        // m_controller = GetComponent<DynamicEntityController>();
    }

    protected virtual void OnCollisionEnter(Collision other)
    {
        Debug.Log("dynamic entity base collision enter");

        collidedObj = other.gameObject;
        collidedObjTag = collidedObj.tag;
        collidedObjLayer = collidedObj.layer;
        collidedEntity = collidedObj.GetComponent<Entity>();
    }

    protected virtual void OnCollisionExit(Collision other)
    {
        Debug.Log("dynamic entity base collision exit");
    }
}
