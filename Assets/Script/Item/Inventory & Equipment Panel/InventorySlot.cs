using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IDragHandler
{
    public enum SlotType
    {
        inventorySlot,
        charSlot
    }

    public SlotType slotType;
    public Inventory inventory { get; set; }
    public int slotNum { get; set; }

    Image itemIcon;
    Text itemAmountGUI;

    void Awake()
    {
        itemIcon = gameObject.transform.GetChild(0).GetComponent<Image>();
        itemAmountGUI = itemIcon.transform.GetChild(0).GetComponent<Text>();
    }

    void Update()
    {
        if (inventory == null)
            return;

        if (ContainsItem())
        {
            // update item icon
            itemIcon.enabled = true;
            itemIcon.sprite = inventory.items[slotNum].itemIcon;

            // show item amount if stackable
            if (inventory.items[slotNum] is ConsumableItem)
            {
                itemAmountGUI.enabled = true;
                itemAmountGUI.text = inventory.items[slotNum].itemAmount.ToString();
            }
        }
        else
            itemIcon.enabled = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (inventory == null)
            return;

        // left click
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (ContainsItem())
            {
                // swap dragged item
                if (inventory.isDraggingItem)
                    SwapDraggedItem();
            }
            else
            {
                // place dragged item
                if (inventory.isDraggingItem)
                {
                    inventory.items[slotNum] = inventory.draggedItem;
                    inventory.HideDraggedItem();
                }
            }
        }

        // right click
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            // use consumalbe items
            if (inventory.items[slotNum] is ConsumableItem)
            {
                ConsumableItem CastItem = (ConsumableItem)inventory.items[slotNum];
                CastItem.ActiveItemEffect();

                // decrease item amount
                inventory.items[slotNum].itemAmount--;

                // if use up
                if (inventory.items[slotNum].itemAmount == 0)
                {
                    // delete item
                    inventory.items[slotNum] = new Item();

                    // hide item amout gui
                    itemAmountGUI.enabled = false;

                    // hide item description
                    inventory.HideItemDescription();
                }
            }
        }
    }

    private void SwapDraggedItem()
    {
        // put the item in this slot to the dragged item's slot
        inventory.items[inventory.draggedItemSlotNum] = inventory.items[slotNum];

        // put dragged item in this slot
        inventory.items[slotNum] = inventory.draggedItem;

        // hide dragged item
        inventory.HideDraggedItem();

        // hide item amount
        itemAmountGUI.enabled = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (inventory == null)
            return;

        // show item description
        if (ContainsItem() && !inventory.isDraggingItem)
            inventory.ShowItemDescription(inventory.items[slotNum]);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (inventory == null)
            return;

        // hide item description
        if (ContainsItem())
            inventory.HideItemDescription();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (inventory == null)
            return;

        if (ContainsItem())
        {
            // display dragged item icon at mouse position
            inventory.ShowDraggedItem(inventory.items[slotNum], slotNum);

            // delete the dragged item
            inventory.items[slotNum] = new Item();

            // hide item amount
            itemAmountGUI.enabled = false;
        }
    }

    bool ContainsItem()
    {
        if (inventory.items[slotNum].itemName != null)
            return true;

        return false;
    }
}