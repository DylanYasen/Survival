using UnityEngine;
using System.Collections;

public class OnUseRecoverSP : OnUseItemEffect
{
    public OnUseRecoverSP(int amt)
    {
        effectAmt = amt;
    }

    public override void Use()
    {
        Debug.Log("heal player SP by " + effectAmt);
    }
}
