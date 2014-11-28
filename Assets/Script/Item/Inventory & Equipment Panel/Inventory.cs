using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    public bool isDraggingItem { get; set; }
    public Item draggedItem { get; set; }
    public int draggedItemSlotNum { get; set; }

    List<InventorySlot> slots = new List<InventorySlot>();

    public InventoryPanel panel { get; private set; }

    void Awake()
    {
        panel = InventoryPanel.instance;

        // init slots
        for (int i = 0; i < transform.childCount; i++)
        {
            // fill itemlist with empty item
            items.Add(new Item());

            slots.Add(transform.GetChild(i).GetComponent<InventorySlot>());

            // set slot index
            slots[i].slotNum = i;

            // inventory reference
            slots[i].inventory = this;
        }

        AddItemByID(0);
        AddItemByID(1);
        AddItemByID(2);
        AddItemByID(3);
        AddItemByID(4);
        AddItemByID(5);
        AddItemByID(6);
        AddItemByID(7);
        AddItemByID(7);
        AddItemByID(7);
        AddItemByID(8);
        AddItemByID(8);
        AddItemByID(8);
        AddItemByID(8);

        // deactivate inventory gui
        panel.gameObject.SetActive(false);
    }


    void Update()
    {
        // drag item 
        if (isDraggingItem)
        {
            Vector3 pos = (Input.mousePosition - GameObject.FindGameObjectWithTag("Canvas").GetComponent<RectTransform>().localPosition);
            panel.dragItemIcon.GetComponent<RectTransform>().localPosition = new Vector2(pos.x + 15, pos.y - 15);
        }
    }

    // add item by itemID
    public void AddItemByID(int id)
    {
        // guard
        if (ItemDatabase.instance == null)
            Debug.Log("itemdatabase is null");

        // search through database
        for (int i = 0; i < ItemDatabase.instance.items.Count; i++)
        {
            // if find matched item 
            if (ItemDatabase.instance.items[i].itemID == id)
            {
                // get item data
                Item item = ItemDatabase.instance.items[i];

                // check for consumable
                if (item is ConsumableItem)
                    // stack if exists
                    // create new if not
                    CheckIfItemExist(id, item);

                // not consumable, add it normally
                else
                    addItemToEmptySlot(item);

                break;
            }
        }
    }

    void addItemToEmptySlot(Item item)
    {
        // loop through itemlist to find empty slot
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemName == null)
            {
                items[i] = item;
                break;
            }
        }
    }

    public void CheckIfItemExist(int id, Item item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            // same item exist
            if (items[i].itemID == id)
            {
                // add up amount
                items[i].itemAmount = items[i].itemAmount + 1;
                break;
            }

            // if can't find, create new item
            else if (i == items.Count - 1)
            {
                addItemToEmptySlot(item);
            }
        }
    }


    public void ShowDraggedItem(Item item, int slotNum)
    {
        draggedItem = item;
        panel.dragItemIcon.enabled = true;
        isDraggingItem = true;
        panel.dragItemIcon.sprite = item.itemIcon;
        draggedItemSlotNum = slotNum;

        HideItemDescription();
    }

    public void HideDraggedItem()
    {
        isDraggingItem = false;
        panel.dragItemIcon.enabled = false;
        draggedItem = null;
    }

    public void ShowItemDescription(Item item)
    {
        panel.itemDesGUI.text = item.itemDes;
    }

    public void HideItemDescription()
    {
        panel.itemDesGUI.text = "";
    }

}