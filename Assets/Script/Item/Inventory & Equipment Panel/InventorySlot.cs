using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IDragHandler
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

    Image equippedMark;
    public bool isEquipped;// { get; private set; }

    Image slotBackgroundImage;
    private Color defaultColor;

    void Awake()
    {
        itemIcon = gameObject.transform.GetChild(0).GetComponent<Image>();
        itemAmountGUI = itemIcon.transform.GetChild(0).GetComponent<Text>();

        equippedMark = itemIcon.transform.GetChild(1).GetComponent<Image>();

        slotBackgroundImage = GetComponent<Image>();
        defaultColor = slotBackgroundImage.color;
    }

    void Start()
    {
        isEquipped = false;
    }

    void Update()
    {
        if (inventory == null)
            return;

        // chanage this 
        // really bad
        // move to SwitchItemIcon
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

    public void SwitchItemIcon()
    {
        //
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // left click
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            // slot not empty 
            if (ContainsItem())
            {
                // swap dragged item if is dragging //
                if (inventory.isDraggingItem)
                {
                    SwapDraggedItem();
                    return;
                }

                // item selection
                if (inventory.hasSelectedItem)
                    inventory.UnselectItem();
                else
                    inventory.SelectItem(slotNum);
            }

            // slot empty
            else
            {
                // place dragged item if dragging
                if (inventory.isDraggingItem)
                {
                    inventory.items[slotNum] = inventory.draggedItem;
                    inventory.HideDraggedItem();

                    return;
                }

                // unselect item if selected
                if (inventory.hasSelectedItem)
                    inventory.UnselectItem();
            }
        }

        // ********
        // TODO: combine with above.   contains item/ not contain
        // ********

        // double click to use / equip
        if (eventData.clickCount == 2)
        {
            if (inventory.items[slotNum] is EquipableItem)
            {
                if (!isEquipped)
                {
                    inventory.EquipItem(slotNum);
                }
                else
                {
                    inventory.UnEquipItem(slotNum);
                }

                isEquipped = !isEquipped;
                equippedMark.enabled = isEquipped;
            }
        }

        /* if same item -> unselect it 
       if (inventory.selectedItemNum == slotNum)
          inventory.UnselectItem();

       if different -> unselect old -> select new
       else
       {
          inventory.UnselectItem();
           inventory.SelectItem(slotNum);
       }
   */
        // not selected item
        //else
        //    inventory.SelectItem(slotNum);




    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (inventory == null)
            return;

        // left click 
        // select item
        /*
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
        */

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

    public void HighlightSlot(bool selected = true)
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

    public void OnDrag(PointerEventData eventData)
    {
        if (inventory == null)
            return;

        if (ContainsItem())
        {
            // unselect selected item
            if (inventory.hasSelectedItem)
                inventory.UnselectItem();

            // display dragged item icon at mouse position
            inventory.ShowDraggedItem(inventory.items[slotNum], slotNum);

            // delete the dragged item
            inventory.items[slotNum] = new Item();

            // hide item amount
            itemAmountGUI.enabled = false;
        }
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