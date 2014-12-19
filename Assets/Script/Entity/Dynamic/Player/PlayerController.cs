using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public CNJoystick virtualController;

    public AnimationClip walkClip;
    public AnimationClip runClip;
    public AnimationClip idleClip;

    private DynamicEntity m_entity;
    private CharacterController m_controller;
    private EntityStats m_stats;
    private Transform m_trans;
    private Vector3 moveDir;
    //private Rigidbody2D m_body;

    // status tags
    public bool isWalking { get; private set; }
    //public bool isRunning { get; private set; }
    public bool isIdling { get; private set; }

    void Start()
    {
        m_entity = GetComponent<DynamicEntity>();
        m_stats = m_entity.m_stats;
        m_controller = GetComponent<CharacterController>();
        //m_body = m_entity.m_body;
        m_trans = m_entity.transform;
    }

    void FixedUpdate()
    {
        moveDir.Set(virtualController.GetAxis("Horizontal"), m_controller.velocity.y, virtualController.GetAxis("Vertical"));

        //Debug.Log(moveDir);

        Move();
    }

    void Move()
    {
        if (moveDir != Vector3.zero)
        {
            //isRunning = true;
            isIdling = false;

            // look at position
            Quaternion rotation = Quaternion.LookRotation(moveDir, Vector3.forward);
            rotation.x = 0; // lock x axis
            rotation.z = 0; // lock z axis
            m_trans.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);

            m_controller.SimpleMove(moveDir * m_stats.MoveSpeed);

            animation.CrossFade(runClip.name);
        }

        else
        {
            //isRunning = false;
            isIdling = true;
            animation.CrossFade(idleClip.name);
        }

        // m_controller.SimpleMove(Vector3.forward * moveDir * m_stats.MoveSpeed);
        //Debug.Log("move " + m_stats.MoveSpeed);
    }
}


