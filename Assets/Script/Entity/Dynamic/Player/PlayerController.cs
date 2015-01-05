using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public CNJoystick virtualController;

    public AnimationClip walkClip;
    public AnimationClip runClip;
    public AnimationClip idleClip;

    private Player m_entity;
    private CharacterController m_controller;
    private EntityStats m_stats;
    private Transform m_trans;
    private Vector3 moveDir;
    //private Rigidbody2D m_body;

    // status tags
    public bool isWalking { get; private set; }
    public bool isIdling { get; private set; }
    //public bool isRunning { get; private set; }

    public Animator anim;

    void Awake()
    {

#if UNITY_IPHONE || UNITY_ANDROID || UNITY_WP8

        if (m_photonView.isMine)
            virtualController = GameObject.FindWithTag("JoyStick").GetComponent<CNJoystick>();
#endif

    }

    void Start()
    {
        m_entity = GetComponent<Player>();
        m_trans = m_entity.transform;
        m_controller = GetComponent<CharacterController>();

        if (m_entity.m_photonView.isMine)
        {
            m_stats = m_entity.m_stats;
        }

        //m_body = m_entity.m_body;
        walkClip = animation.GetClip("walk");
        runClip = animation.GetClip("run");
        idleClip = animation.GetClip("idle");
    }

    void FixedUpdate()
    {
        if (!m_entity.m_photonView.isMine)
            return;

#if UNITY_IPHONE || UNITY_ANDROID || UNITY_WP8

        moveDir.Set(virtualController.GetAxis("Horizontal"), m_controller.velocity.y, virtualController.GetAxis("Vertical"));

#elif UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER

        moveDir.Set(Input.GetAxisRaw("Horizontal"), m_controller.velocity.y, Input.GetAxisRaw("Vertical"));
#endif

#if UNITY_EDITOR
        //Debug.Log(moveDir);
#endif
        moveDir.Normalize();

        Move();

        //Debug.Log(isIdling);
    }

    void Move()
    {

        if (moveDir != Vector3.zero)
        {
            RotateToward(moveDir);

            m_controller.SimpleMove(moveDir * m_stats.MoveSpeed);

            // send others anim update
            if (isIdling)
            {
                if (!PhotonNetwork.offlineMode)
                    m_entity.m_photonView.RPC("UpdateAnim", PhotonTargets.All, runClip.name);
                else
                    UpdateAnim(runClip.name);
            }

            //isRunning = true;
            isIdling = false;
        }

        else
        {
            if (!isIdling)
            {
                // send others anim update
                if (!PhotonNetwork.offlineMode)
                    m_entity.m_photonView.RPC("UpdateAnim", PhotonTargets.All, idleClip.name);
                else
                    UpdateAnim(idleClip.name);
            }

            //isRunning = false;
            isIdling = true;
        }

        // m_controller.SimpleMove(Vector3.forward * moveDir * m_stats.MoveSpeed);
        //Debug.Log("move " + m_stats.MoveSpeed);
    }

    void RotateToward(Vector3 dir)
    {
        // look at position
        Quaternion rotation = Quaternion.LookRotation(dir, Vector3.up);
        rotation.x = 0; // lock x axis
        rotation.z = 0; // lock z axis
        m_trans.rotation = Quaternion.Lerp(m_trans.rotation, rotation, Time.deltaTime * 10);
    }

    [RPC]
    void UpdateAnim(string clipName)
    {
        animation.CrossFade(clipName);
    }

}

