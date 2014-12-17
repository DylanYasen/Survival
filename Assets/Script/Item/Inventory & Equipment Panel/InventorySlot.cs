using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
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
    Image slotBackgroundImage;
    Text itemAmountGUI;

    private Color defaultColor;

    void Awake()
    {
        itemIcon = gameObject.transform.GetChild(0).GetComponent<Image>();
        itemAmountGUI = itemIcon.transform.GetChild(0).GetComponent<Text>();

        slotBackgroundImage = GetComponent<Image>();
        defaultColor = slotBackgroundImage.color;
    }

    void Start()
    {
        Debug.Log(inventory.items[slotNum].itemName);
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
        // select item
        if (eventData.button == PointerEventData.InputButton.Left)
        {

            // ***************
            // change to just swap
            // contain or not doesn't really matter
            // ***************
            if (inventory.hasSelectedItem)
            {
                SwapSelectedItem();
            }

            else if (ContainsItem())
            {
                inventory.SelectItem(slotNum);

                HighlightSlot(true);

                Debug.Log("selected");
            }
        }

        // right click
        // to use item
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

    private void SwapSelectedItem()
    {
        // put the item in this slot
        // to the selected item's slot
        inventory.items[inventory.selectedItemNum] = inventory.items[slotNum];

        // put dragged item in this slot
        inventory.items[slotNum] = inventory.selectedItem;

        // hide item amount
        itemAmountGUI.enabled = false;


        inventory.UnselectItem();
    }

    public void HighlightSlot(bool selected)
    {
        if (selected)
            slotBackgroundImage.color = Color.red;
        else
            slotBackgroundImage.color = defaultColor;
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

    /*
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
    */

    bool ContainsItem()
    {
        if (inventory.items[slotNum].itemName != null)
            return true;

        return false;
    }
}