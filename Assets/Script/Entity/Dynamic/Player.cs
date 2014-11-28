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

public class Player : DynamicEntity
{
    public static Player instance;

    public Traits trait;
    // multi-traits
    // public List<Traits> traits = new List<Traits>();

    // move to dynamic entity class might be better
    public PlayerController m_controller;
    public PlayerStatsController m_statController;

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
    }
}
