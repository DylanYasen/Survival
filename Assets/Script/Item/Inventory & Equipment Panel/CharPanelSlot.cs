using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class CharPanelSlot : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public int index { get; set; }
    public CharPanel charPanel { get; set; }
    public SpriteRenderer itemAttachPoint;

    private Inventory inventory;
    private InventoryPanel panel;
    private Image itemIcon;

    void Awake()
    {
        itemIcon = transform.GetChild(0).GetComponent<Image>();
        panel = charPanel.panel;
        inventory = panel.inventory;
    }

    void Update()
    {
        // **** this is shitty **** //
        // **** change later **** //
        if (ContainsItem())
        {
            itemIcon.enabled = true;
            itemIcon.sprite = charPanel.items[index].itemIcon;
        }
        else
            itemIcon.enabled = false;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        // right click
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            // unequip
            if (ContainsItem())
            {
                // give item back to inventory
                inventory.AddItemByID(charPanel.items[index].itemID);

                UnequipInSlotItem();
            }
        }

        // left click
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            // equip item
            if (inventory.isDraggingItem)
            {
                Item draggedItem = inventory.draggedItem;

                if (draggedItem is EquipableItem)
                {
                    EquipableItem equipment = (EquipableItem)draggedItem;

                    if (EquipToSlot(equipment))
                    {
                        AttachWeaponToPlayer(equipment);
                        equipment.ActiveItemEffect();
                    }
                }
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // show item description
        if (ContainsItem() && !charPanel.isDraggingItem)
            charPanel.ShowItemDescription(charPanel.items[index]);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // hide item description
        if (ContainsItem())
            charPanel.HideItemDescription();
    }

    /*
   public void OnDrag(PointerEventData eventData)
   {
       if (ContainsItem())
       {
           // display dragged item icon at mouse position
           charPanel.ShowDraggedItem(charPanel.items[index], index);

           // delete the dragged item
           charPanel.items[index] = new Item();
       }
   }
    */

    bool EquipToSlot(EquipableItem draggedItem)
    {
        // * important
        // * slots index has the some order as 
        // * weapon equip type enum's order

        if (index == (int)draggedItem.equipType)
        {
            // slot occupied
            if (ContainsItem())
            {
                // swap old -> new item
                // give old item back to inventory
                Item tempItem = charPanel.items[index];
                charPanel.items[index] = draggedItem;
                inventory.items[inventory.draggedItemSlotNum] = tempItem;

                draggedItem.ActiveItemEffect();
            }

            // slot empty
            else
                charPanel.items[index] = draggedItem;

            inventory.HideDraggedItem();

            return true;
        }

        Debug.Log("wrong slot!");
        return false;
    }

    void AttachWeaponToPlayer(EquipableItem equipItem)
    {
        if (charPanel.items[index].itemSprite != null && itemAttachPoint != null)
            itemAttachPoint.sprite = charPanel.items[index].itemSprite;

        //add collider for weapon
        if (equipItem.equipType == EquipableItem.EquipType.Weapon)
        {
            // clear previous wepon collider
            if (itemAttachPoint.GetComponent<PolygonCollider2D>() != null)
                Destroy(itemAttachPoint.GetComponent<PolygonCollider2D>());

            // attach new collider
            itemAttachPoint.gameObject.AddComponent<PolygonCollider2D>();
        }
    }

    void UnequipInSlotItem()
    {
        if (charPanel.items[index].itemSprite != null && itemAttachPoint != null)
            itemAttachPoint.sprite = null;

        EquipableItem CastItem = (EquipableItem)charPanel.items[index];
        CastItem.DeactivateItemEffect();
        charPanel.items[index] = new Item();
    }

    bool ContainsItem()
    {
        if (charPanel.items[index].itemName != null)
            return true;

        return false;
    }
}
    /*
     * 
     * old script in equipToSlot
    switch (index)
    {
        case 0:
            if (draggedItem.equipType == EquipableItem.EquipType.Head)
            {
                // slot not empty
                if (ContainsItem())
                {
                    Item tempItem = charPanel.items[index];
                    charPanel.items[index] = draggedItem;
                    //InventoryPanel.instance.inventory.draggedItem = tempItem;
                    InventoryPanel.instance.inventory.items[InventoryPanel.instance.inventory.draggedItemSlotNum] = tempItem;
                    // InventoryPanel.instance.inventory.ShowDraggedItem(tempItem, -1);

                    draggedItem.ActiveItemEffect();
                }

                // slot empty
                else
                    charPanel.items[index] = draggedItem;

                InventoryPanel.instance.inventory.HideDraggedItem();

                return true;
            }
            break;

        case 1:
            if (draggedItem.equipType == EquipableItem.EquipType.Body)
            {
                // slot not empty
                if (ContainsItem())
                {
                    Item tempItem = charPanel.items[index];
                    charPanel.items[index] = draggedItem;
                    //InventoryPanel.instance.inventory.draggedItem = tempItem;
                    InventoryPanel.instance.inventory.items[InventoryPanel.instance.inventory.draggedItemSlotNum] = tempItem;

                    // InventoryPanel.instance.inventory.ShowDraggedItem(tempItem, -1);
                }

                // slot empty
                else
                    charPanel.items[index] = draggedItem;

                InventoryPanel.instance.inventory.HideDraggedItem();

                return true;
            }
            break;

        case 2:
            if (draggedItem.equipType == EquipableItem.EquipType.Legs)
            {
                // slot not empty
                if (ContainsItem())
                {
                    Item tempItem = charPanel.items[index];
                    charPanel.items[index] = draggedItem;
                    //InventoryPanel.instance.inventory.draggedItem = tempItem;
                    InventoryPanel.instance.inventory.items[InventoryPanel.instance.inventory.draggedItemSlotNum] = tempItem;

                    // InventoryPanel.instance.inventory.ShowDraggedItem(tempItem, -1);
                }

                // slot empty
                else
                    charPanel.items[index] = draggedItem;

                InventoryPanel.instance.inventory.HideDraggedItem();

                return true;
            }
            break;

        case 3:
            if (draggedItem.equipType == EquipableItem.EquipType.Weapon)
            {
                // slot not empty
                if (ContainsItem())
                {
                    // add item to slot
                    Item tempItem = charPanel.items[index];
                    charPanel.items[index] = draggedItem;
                    //InventoryPanel.instance.inventory.draggedItem = tempItem;
                    InventoryPanel.instance.inventory.items[InventoryPanel.instance.inventory.draggedItemSlotNum] = tempItem;
                    // InventoryPanel.instance.inventory.ShowDraggedItem(tempItem, -1);
                }

                // slot empty
                else
                    charPanel.items[index] = draggedItem;

                InventoryPanel.instance.inventory.HideDraggedItem();

                print("testing!!!!!!!!!!!!" + " index is " + index + " weapon state is " + (int)draggedItem.equipType);

                return true;
            }
            break;

        case 4:
            if (draggedItem.equipType == EquipableItem.EquipType.Accessory)
            {
                // slot not empty
                if (ContainsItem())
                {
                    Item tempItem = charPanel.items[index];
                    charPanel.items[index] = draggedItem;
                    //InventoryPanel.instance.inventory.draggedItem = tempItem;
                    InventoryPanel.instance.inventory.items[InventoryPanel.instance.inventory.draggedItemSlotNum] = tempItem;

                    // InventoryPanel.instance.inventory.ShowDraggedItem(tempItem, -1);
                }

                // slot empty
                else
                    charPanel.items[index] = draggedItem;

                InventoryPanel.instance.inventory.HideDraggedItem();

                return true;
            }
            break;

        case 5:
            if (draggedItem.equipType == EquipableItem.EquipType.Accessory)
            {
                // slot not empty
                if (ContainsItem())
                {
                    Item tempItem = charPanel.items[index];
                    charPanel.items[index] = draggedItem;
                    //InventoryPanel.instance.inventory.draggedItem = tempItem;
                    InventoryPanel.instance.inventory.items[InventoryPanel.instance.inventory.draggedItemSlotNum] = tempItem;

                    // InventoryPanel.instance.inventory.ShowDraggedItem(tempItem, -1);
                }

                // slot empty
                else
                    charPanel.items[index] = draggedItem;

                InventoryPanel.instance.inventory.HideDraggedItem();

                return true;
            }
            break;
     */


