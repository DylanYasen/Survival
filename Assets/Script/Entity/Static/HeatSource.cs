using UnityEngine;
using System.Collections;

public class HeatSource : StaticEntity
{
    public int heatAmout = 1;
    public float heatRate;

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (collidedObjTag == "Player")
            player.m_controller.isNearHeat = true;
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (collidedObjTag == "Player")
            player.m_controller.isNearHeat = false;
    }

    protected override void OnTriggerStay(Collider other)
    {
        if (collidedObjTag == "Player")
            player.m_statController.AddWarmth(heatAmout, heatRate);
    }
}
