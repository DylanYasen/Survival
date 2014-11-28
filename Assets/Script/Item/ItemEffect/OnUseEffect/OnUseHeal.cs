using UnityEngine;
using System.Collections;

public class OnUseHeal : OnUseItemEffect
{
    public OnUseHeal(int def)
    {
        effectAmt = def;
    }

    public override void Use()
    {
        Debug.Log("heal player health by " + effectAmt);
    }
}
