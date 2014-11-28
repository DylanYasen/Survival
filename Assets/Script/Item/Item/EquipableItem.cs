using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EquipableItem : Item
{
    public enum EquipType
    {
        // order is the same as charslot order
        Head,
        Body,
        Legs,
        Weapon,
        Arms,
        Accessory
    }

    public EquipType equipType { get; set; }

    public List<OnEquipItemEffect> itemEffects = new List<OnEquipItemEffect>();

    delegate void EquipEffectDelegate();
    EquipEffectDelegate itemEffectDelegate;

    public void InitEffect()
    {
        for (int i = 0; i < itemEffects.Count; i++)
        {
            itemEffectDelegate += itemEffects[i].Equip;
        }
    }

    public void ActiveItemEffect()
    {
        itemEffectDelegate();
    }

    public void DeactivateItemEffect()
    {

        for (int i = 0; i < itemEffects.Count; i++)
        {
            itemEffectDelegate -= itemEffects[i].Equip;
            itemEffectDelegate += itemEffects[i].UnEquip;
        }

        itemEffectDelegate();
    }
}