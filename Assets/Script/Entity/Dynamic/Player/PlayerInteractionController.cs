using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerInteractionController : MonoBehaviour
{
    public bool isNearHeatSource { get; set; }

    public bool isWorking { get; private set; }
    public bool workDone { get; private set; }
    public float workTimer { get; private set; }

    public float workTimeNeededRealTime { get; private set; }
    public float workTimeNeededTotal { get; private set; }
    public float workTimeCurrent { get; private set; }

    private Inventory inventory;

    // should be multiple buttons 
    // instead of changing the name of one button
    public GameObject button;

    public GameObject ProgressBar; // progress bar for working 
    public Scrollbar progressScrollBar;
    public RectTransform progressBarRect;
    public Text progressBarText;

    void Awake()
    {
        progressScrollBar = ProgressBar.GetComponent<Scrollbar>();
        progressBarText = ProgressBar.transform.GetChild(1).GetComponent<Text>();
        progressBarRect = ProgressBar.GetComponent<RectTransform>();

        isWorking = false;
        workDone = false;
    }

    void Start()
    {
        inventory = InventoryPanel.instance.inventory;

        ToggleWorkBar();
    }

    void Update()
    {
        NearFire();

        if (isWorking)
            OnWork();

    }

    private void NearFire()
    {
        if (!isNearHeatSource)
        {
            button.SetActive(false);
            return;
        }

        if (inventory.hasSelectedItem)
        {
            // don't update when gui is already showing
            if (button.activeSelf)
                return;

            // equipment
            // interact only when equipped
            if (inventory.selectedItem is EquipableItem && inventory.slots[inventory.selectedItemNum].isEquipped)
            {
                button.transform.GetChild(0).GetComponent<Text>().text = "Ignite";  // cache this

                // show interaction gui botton
                button.SetActive(true);
            }
        }
        else
            button.SetActive(false);
    }

    private void OnWork()
    {
        workTimer += TimeManager.deltaTime;

        if (workTimer >= TimeManager.GAME_SECOND_IN_SECOND)
        {
            workTimer -= TimeManager.GAME_SECOND_IN_SECOND;
            workTimeCurrent += TimeManager.GAME_SECOND_IN_SECOND;
            workTimeNeededRealTime--;
        }

        progressScrollBar.size = workTimeCurrent / workTimeNeededTotal;
        progressBarText.text = workTimeNeededRealTime.ToString() + " s";

        // finish working
        if (workTimeCurrent >= workTimeNeededTotal)
        {
            isWorking = false;
            workDone = true;
            InventoryPanel.instance.inventory.OnFinishInteraction();
            ToggleWorkBar();
        }
    }

    public void Work(float time)
    {
        Debug.Log(time);


        workDone = false;
        isWorking = true;
        workTimeNeededTotal = time * TimeManager.GAME_SECOND_IN_SECOND;
        workTimeCurrent = 0;
        workTimeNeededRealTime = time;

        ToggleWorkBar(true);
    }

    private void ToggleWorkBar(bool b = false)
    {
        ProgressBar.SetActive(b);
    }

    // gui accessor
    public void TriggerSelectedItemInteraction()
    {
        progressBarRect.position = Camera.main.WorldToScreenPoint(Player.instance.floatTextSpawnPoint.position);
        inventory.SelectedItemInteract();
    }
}