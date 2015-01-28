using UnityEngine;
using System.Collections;

public class HeatSource : Building
{
    public int heatAmout = 1;
    public float heatRate;

    protected int decayRate = 0;

    protected Light light { get; private set; }

    // TODO: partical decay to indicate hp

    protected virtual void Awake()
    {
        base.Awake();
        light = transform.GetChild(1).GetChild(0).GetComponent<Light>();
    }

    protected virtual void Update()
    {
        m_stats.LoseHP(decayRate);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (collidedObjTag == "Player")
            player.m_interactController.isNearHeatSource = true;
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (collidedObjTag == "Player")
            player.m_interactController.isNearHeatSource = false;
    }

    protected override void OnTriggerStay(Collider other)
    {
        if (collidedObjTag == "Player")
            player.m_statController.AddWarmth(heatAmout, heatRate);
    }

    [RPC]
    public void AddFuel(int amt)
    {
        m_stats.AddHP(amt);
    }

}
