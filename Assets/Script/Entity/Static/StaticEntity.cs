using UnityEngine;
using System.Collections;

public abstract class StaticEntity : Entity
{
    protected virtual void OnTriggerEnter(Collider other)
    {
        collidedObj = other.gameObject;
        collidedObjTag = collidedObj.tag;
        collidedObjLayer = collidedObj.layer;
        collidedEntity = collidedObj.GetComponent<Entity>();

        //Debug.Log("static entity base on trigger enter");
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        // Debug.Log("static entity base on trigger exit");
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        //Debug.Log("static entity base on trigger stay");
    }

}
