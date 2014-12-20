using UnityEngine;
using System.Collections;

public class PlayerInteractionController : MonoBehaviour
{
    public bool isNearHeatSource { get; set; }

    private Inventory inventory;

    // should be multiple buttons 
    // instead of changing the name of one button
    public GameObject button;

    void Start()
    {
        inventory = InventoryPanel.instance.inventory;
    }

    void Update()
    {
        NearFire();
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
                // show interaction gui botton
                button.SetActive(true);

        }
        else
            button.SetActive(false);

    }

    // gui accessor
    public void TriggerSelectedItemInteraction()
    {
        inventory.SelectedItemInteract();
    }

}