using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Traits
{
    Miserly,
    Knowledgeable,
    Hardy,
    Hasty,
    FastMetabolism,
    Prodigy
}

/*
[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PlayerStatsController))]
[RequireComponent(typeof(PlayerEquipmentController))]
[RequireComponent(typeof(PlayerInteractionController))]
[RequireComponent(typeof(PhotonView))]
*/

public class Player : DynamicEntity
{
    public static Player instance;

    public Traits trait;
    // multi-traits
    // public List<Traits> traits = new List<Traits>();

    // move to dynamic entity class might be better
    public PlayerController m_controller { get; private set; }
    public PlayerStatsController m_statController { get; private set; }
    public PlayerEquipmentController m_gearController { get; private set; }
    public PlayerInteractionController m_interactController { get; private set; }


    protected override void Awake()
    {
        // maybe move to generic class
        m_photonView = GetComponent<PhotonView>();

        if (m_photonView.isMine)
        {
            instance = this;

            gameObject.tag = "Player";

            base.Awake();

            m_statController = gameObject.AddComponent<PlayerStatsController>();
            m_gearController = gameObject.AddComponent<PlayerEquipmentController>();
            m_interactController = gameObject.AddComponent<PlayerInteractionController>();
        }

        m_controller = gameObject.AddComponent<PlayerController>();

    }
}
