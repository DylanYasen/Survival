using UnityEngine;
using System.Collections;

public class Mob : MonoBehaviour
{
    public float moveSpeed;
    public float attackRange;

    public Transform target;

    public AnimationClip run;
    public AnimationClip idle;

    private Transform m_trans;
    private CharacterController m_controller;

    void Awake()
    {
        m_controller = GetComponent<CharacterController>();
        m_trans = transform;
    }


    void Update()
    {
        if (!TargetInRange())
            Chase();
        else
            animation.CrossFade(idle.name);
    }

    bool TargetInRange()
    {
        return (Vector3.Distance(m_trans.position, target.position) < attackRange);
    }

    void Chase()
    {
        m_trans.LookAt(target.position);

        m_controller.SimpleMove(m_trans.forward * moveSpeed);

        animation.CrossFade(run.name);
    }

    void OnMouseOver()
    {
        // show gui

        // Debug.Log("mouse over mob");
    }

    void OnMouseDown()
    {
        Debug.Log("mouse down mob");
        target.GetComponent<Combat>().target = this.gameObject;
    }
}
