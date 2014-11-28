using UnityEngine;
using System.Collections;

public class OnEquipAddDmg : OnEquipItemEffect
{
    public OnEquipAddDmg(int dmg)
    {
        this.effectAmt = dmg;
    }

    public override void Equip()
    {
        Debug.Log("add damage by " + this.effectAmt);
    }

    public override void UnEquip()
    {
        Debug.Log("unequip");
    }
}
