using UnityEngine;
using System.Collections;

public class PlayerStatsController : MonoBehaviour
{
    public float dropHungerRate = 4;
    public float dropHydrationRate = 1;
    public float dropEnergyRate = 0.01f;
    public float dropHealthRate = 1;
    public float dropWarmthRate = 1;

    private float hungerTimer;
    private float hydrationTimer;
    private float energyTimer;
    private float healthTimer;
    private float warmthTimer;

    private Player m_entity;
    private EntityStats m_stats;
    private PlayerController m_entityController;

    void Start()
    {
        m_entity = Player.instance;
        m_stats = m_entity.m_stats;
        m_entityController = m_entity.m_controller;
    }

    void Update()
    {
        UpdateHunger();
        UpdateHydration();
        UpdateEnergy();
        UpdateWarmth();
    }

    private void UpdateWarmth()
    {
        //if(!aroundFire)

        // don't drop when near heat
        if (m_entityController.isNearHeat)
            return;

        warmthTimer += Time.deltaTime;

        if (warmthTimer >= dropWarmthRate)
        {
            m_stats.LoseWarmth();
            warmthTimer -= dropWarmthRate;
        }

        // lost health if too low
        // maybe better to a threshold
        if (m_stats.cur_warmth <= 0)
            UpdateHealth();
    }

    public void AddWarmth(int amt = 1, float addWarmthRate = 1)
    {
        warmthTimer += Time.deltaTime;

        if (warmthTimer >= addWarmthRate)
        {
            m_stats.AddWarmth(amt);
            warmthTimer -= addWarmthRate;

            // talk
            /*if (m_stats.cur_warmth != m_stats.MaxWarmth)
            {
                HudTextManager.instance.CreateFloatText(m_entity.floatTextSpawnPoint.position, "", Color.green);
                //HudTextManager.instance.CreateFloatText(m_entity.floatTextSpawnPoint.position, "温暖 ++", Color.green);
            }
             */
        }
    }

    private void UpdateHunger()
    {
        hungerTimer += Time.deltaTime;

        if (hungerTimer >= dropHungerRate)
        {
            m_stats.LoseHunger();
            hungerTimer -= dropHungerRate;
        }

        // lost health if too low
        // maybe better to a threshold

        // talk
        /*
        if (m_stats.cur_warmth != m_stats.MaxWarmth)
        {
            HudTextManager.instance.CreateFloatText(m_entity.floatTextSpawnPoint.position, "I'm so hungry", Color.green);
            //HudTextManager.instance.CreateFloatText(m_entity.floatTextSpawnPoint.position, "温暖 ++", Color.green);
        }
         */

        if (m_stats.cur_hunger <= 0)
            UpdateHealth();
    }

    private void UpdateHydration()
    {
        hydrationTimer += Time.deltaTime;

        if (hydrationTimer >= dropHydrationRate)
        {
            m_stats.LoseHydration();
            hydrationTimer -= dropHydrationRate;
        }

        // lost health if too low
        // maybe better to a threshold
        if (m_stats.cur_hydration <= 0)
            UpdateHealth();
    }

    private void UpdateEnergy()
    {
        // lose energy if not standing still
        if (!m_entityController.isIdling)
        {
            energyTimer += Time.deltaTime;

            if (energyTimer >= dropEnergyRate)
            {
                m_stats.LoseEnergy();
                energyTimer -= dropEnergyRate;
            }
        }
        else
            energyTimer = 0;

        // lost health if too low
        // maybe better to a threshold
        if (m_stats.cur_energy <= 0)
            UpdateHealth();
    }

    private void UpdateHealth()
    {
        healthTimer += Time.deltaTime;

        if (healthTimer > dropHealthRate)
        {
            //HudTextManager.instance.CreateFloatText(m_entity.floatTextSpawnPoint.position, "Health --", Color.red);

            m_stats.LoseHP();
            healthTimer -= dropHealthRate;

        }
    }
}


 