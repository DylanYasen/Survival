using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConsumableItem : Item
{
    public List<OnUseItemEffect> itemEffects = new List<OnUseItemEffect>();

    delegate void EquipEffectDelegate();
    EquipEffectDelegate itemEffectDelegate;

    public ConsumableItem(string name)
        : base(name)
    {
    }

    public void InitEffect()
    {
        for (int i = 0; i < itemEffects.Count; i++)
        {
            itemEffectDelegate += itemEffects[i].Use;
        }
    }

    public void ActiveItemEffect()
    {
        itemEffectDelegate();
    }
}
