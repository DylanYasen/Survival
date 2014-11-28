using UnityEngine;
using System.Collections;

public abstract class OnEquipItemEffect : ItemEffect
{
    protected Player player = Player.instance;

    abstract public void Equip();
    abstract public void UnEquip();
}
