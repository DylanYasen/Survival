using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerStatsGUI : MonoBehaviour
{
    private EntityStats stats;

    public Text healthText;
    public Text energyText;
    public Text hydrationText;
    public Text hungerText;
    public Text warmthText;

    void Awake()
    {

    }

    void Start()
    {
        stats = Player.instance.m_stats;
    }

    void Update()
    {
        /*
        healthText.text = "Health: " + stats.cur_hp + "%";
        energyText.text = "Energy: " + stats.cur_energy + "%";
        warmthText.text = "Warmth: " + stats.cur_warmth + "%";
        hungerText.text = "Hunger : " + stats.cur_hunger + "%";
        hydrationText.text = "Hydrated: " + stats.cur_hydration + "%";
        */


        healthText.text = "健康: " + stats.cur_hp + "%";
        energyText.text = "精力: " + stats.cur_energy + "%";
        hydrationText.text = "水分: " + stats.cur_hydration + "%";
        hungerText.text = "饱腹 : " + stats.cur_hunger + "%";
        warmthText.text = "温暖: " + stats.cur_warmth + "%";

    }
}
