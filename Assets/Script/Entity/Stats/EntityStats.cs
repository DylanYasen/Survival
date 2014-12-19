using UnityEngine;
using System.Collections;

[System.Serializable]
public class EntityStats
{
    [SerializeField]
    public int MaxHP;
    public int MaxEnergy;
    public int MaxHydration;
    public int MaxHunger;
    public int MaxWarmth;

    public int DMG;
    public int DEF;
    public int extra_DMG;
    public int extra_DEF;

    public int MoveSpeed = 1;

    public int cur_hp { get; private set; }
    public int cur_energy { get; private set; }
    public int cur_hydration { get; private set; }
    public int cur_hunger { get; private set; }
    public int cur_warmth { get; private set; }

    private Entity m_entity;

    public void Init(Entity e)
    {
        m_entity = e;

        cur_hp = MaxHP;
        cur_energy = MaxEnergy;
        cur_hydration = MaxHydration;
        cur_hunger = MaxHunger;
        cur_warmth = MaxWarmth;
    }

    public void LoseHP(int amt = 1)
    {
        if (amt > 0 && cur_hp > 0)
            cur_hp -= amt;

        if (cur_hp <= 0)
            m_entity.Die();

        Debug.Log("lost " + amt + " hp, " + cur_hp + " left.");
    }

    public void AddHP(int amt = 1)
    {
        // limit hp addition
        int addAmt = Mathf.Clamp(amt, 0, MaxHP - cur_hp);

        cur_hp += addAmt;
    }

    public bool LoseEnergy(int amt = 1)
    {
        if (cur_energy >= amt)
        {
            cur_energy -= amt;
            return true;
        }

        Debug.Log("not enough sp");
        return false;
    }

    public void AddEnergy(int amt = 1)
    {
        int addAmt = Mathf.Clamp(amt, 0, MaxEnergy - cur_energy);

        cur_energy += addAmt;
    }

    public bool LoseHydration(int amt = 1)
    {
        if (cur_hydration >= amt)
        {
            cur_hydration -= amt;
            return true;
        }

        Debug.Log("not enough hydration");
        return false;
    }

    public void AddHydration(int amt = 1)
    {
        int addAmt = Mathf.Clamp(amt, 0, MaxHydration - cur_hydration);

        cur_hydration += addAmt;
    }

    public bool LoseHunger(int amt = 1)
    {
        if (cur_hunger >= amt)
        {
            cur_hunger -= amt;
            return true;
        }

        Debug.Log("not enough hunger");
        return false;
    }

    public void AddHunger(int amt = 1)
    {
        int addAmt = Mathf.Clamp(amt, 0, MaxHunger - cur_hunger);

        cur_hunger += addAmt;
    }

    public bool LoseWarmth(int amt = 1)
    {
        if (cur_warmth >= amt)
        {
            cur_warmth -= amt;
            return true;
        }

        Debug.Log("not enough hunger");
        return false;
    }

    public void AddWarmth(int amt = 1)
    {
        int addAmt = Mathf.Clamp(amt, 0, MaxWarmth - cur_warmth);

        cur_warmth += addAmt;

        if (cur_warmth != MaxWarmth)
        {
            HudTextManager.instance.CreateFloatText(Player.instance.floatTextSpawnPoint.position, "Warmth ++", Color.magenta);
            //HudTextManager.instance.CreateFloatText(Player.instance.floatTextSpawnPoint.position, "温暖 ++", Color.magenta);
        }

    }
}
