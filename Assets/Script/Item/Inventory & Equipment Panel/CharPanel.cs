using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CharPanel : MonoBehaviour
{
    public List<CharPanelSlot> charSlots = new List<CharPanelSlot>();
    public List<Item> items = new List<Item>();

    public bool isDraggingItem { get; set; }
    public Item draggedItem { get; set; }
    public int draggedItemSlotNum { get; set; }
    public InventoryPanel panel { get; private set; }

    void Awake()
    {
        panel = InventoryPanel.instance;

        for (int i = 0; i < charSlots.Count; i++)
        {
            items.Add(new Item());

            charSlots[i].index = i;
            charSlots[i].charPanel = this;
        }
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