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

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PlayerStatsController))]
[RequireComponent(typeof(PlayerEquipmentController))]
[RequireComponent(typeof(PlayerInteractionController))]

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
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        gameObject.tag = "Player";

        base.Awake();

        m_controller = GetComponent<PlayerController>();
        m_statController = GetComponent<PlayerStatsController>();
        m_gearController = GetComponent<PlayerEquipmentController>();
        m_interactController = GetComponent<PlayerInteractionController>();

    }
}
