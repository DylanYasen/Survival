using UnityEngine;
using System.Collections;

public abstract class Entity : MonoBehaviour
{
    public string entityName;
    public int ID;
    public EntityStats m_stats;
    public Transform floatTextSpawnPoint;

    public Animator m_anim { get; private set; }

    public PhotonView m_photonView { get; private set; }

    public GameObject collidedObj { get; protected set; }
    public Entity collidedEntity { get; protected set; }
    public string collidedObjTag { get; protected set; }
    public LayerMask collidedObjLayer { get; protected set; }

    protected Player player { get; private set; }

    protected virtual void Awake()
    {
        m_stats.Init(this);

        m_anim = GetComponent<Animator>();

        // if (!PhotonNetwork.offlineMode)
        //   m_photonView = gameObject.AddComponent<PhotonView>();

        m_photonView = gameObject.GetComponent<PhotonView>();

        floatTextSpawnPoint = gameObject.transform.FindChild("floatTextPoint");

    }

    protected virtual void Start()
    {
        player = Player.instance;
    }

    public virtual void Die()
    {
        Destroy(gameObject);

        // disable collider box
        // play anim & sfx
        // give back to object pool
    }

}
