using UnityEngine;
using System.Collections;

public class OnEquipAddDef : OnEquipItemEffect
{
    public OnEquipAddDef(int def)
    {
        this.effectAmt = def;
    }

    public override void Equip()
    {
        Debug.Log("add player defence by " + effectAmt);
    }


    public override void UnEquip()
    {
        Debug.Log("unequip");
    }
}
