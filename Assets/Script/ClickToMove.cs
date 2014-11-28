using UnityEngine;
using System.Collections;

public class ClickToMove : MonoBehaviour
{

    public float moveSpeed = 5;
    public float rotateSpeed = 5;

    private Vector3 clickPos;
    private Transform m_trans;
    private CharacterController m_controller;

    public AnimationClip run;
    public AnimationClip idle;

    public static bool isAttacking = false;

    void Awake()
    {
        m_trans = transform;
        m_controller = GetComponent<CharacterController>();
    }

    // Use this for initialization
    void Start()
    {
        clickPos = m_trans.position;
    }

    void Update()
    {
        if (!isAttacking)
        {
            if (Input.GetMouseButtonDown(0))
            {
                LocatePosition();

            }

            MoveToPos();
        }
    }

    void LocatePosition()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000))
        {
            if (hit.collider.tag != "Player")
                clickPos = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }
    }

    void MoveToPos()
    {
        // not moving
        if (Vector3.Distance(m_trans.position, clickPos) < 0 && !isAttacking)
        {
            animation.CrossFade(idle.name);
            return;
        }

        // rotate
        Quaternion clickRotation = Quaternion.LookRotation(clickPos - m_trans.position, Vector3.forward);

        // lock axis
        clickRotation.x = 0;
        clickRotation.z = 0;

        m_trans.rotation = Quaternion.Slerp(m_trans.rotation, clickRotation, Time.deltaTime * rotateSpeed);

        // move
        m_controller.SimpleMove(transform.forward * moveSpeed);

        animation.CrossFade(run.name);
    }

}
