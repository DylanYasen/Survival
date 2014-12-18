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

    public bool hasSelectedItem { get; private set; }
    public Item selectedItem { get; private set; }
    public int selectedItemNum { get; private set; }

    List<InventorySlot> slots = new List<InventorySlot>();

    public InventoryPanel panel { get; private set; }
    private RectTransform panelRectrans;
    private RectTransform dragItemIconRectrans;

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

        panelRectrans = panel.GetComponent<RectTransform>();
        dragItemIconRectrans = panel.dragItemIcon.GetComponent<RectTransform>();

        //AddItemByID(0);

        // deactivate inventory gui
        //panel.gameObject.SetActive(false);
    }


    void Update()
    {
        // drag item 

        /*
        if (isDraggingItem)
        {
            Vector3 pos = (Input.mousePosition - panelRectrans.position);
            dragItemIconRectrans.localPosition = new Vector2(pos.x - 25, pos.y - 25);
        }
         */
    }

    // add item by itemID
    public void AddItemByID(int id)
    {
        // guard
        if (ItemDatabase.instance == null)
        {
            Debug.Log("itemdatabase is null");
            return;
        }

        // search through database
        for (int i = 0; i < ItemDatabase.instance.items.Count; i++)
        {
            // if find matched item 
            if (ItemDatabase.instance.items[i].itemID == id)
            {
                // get item data
                Item item = ItemDatabase.instance.items[i];

                Debug.Log(item.itemName);

                // check for consumable
                //if (item is ConsumableItem)
                // stack if exists
                // create new if not
                //  CheckIfItemExist(id, item);

                // not consumable, add it normally
                //else
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
                Debug.Log("exist");
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

    public void SelectItem(int slotNum)
    {
        hasSelectedItem = true;
        selectedItem = items[slotNum];
        selectedItemNum = slotNum;
    }

    public void UnselectItem()
    {
        hasSelectedItem = false;

        // cancel highlight
        slots[selectedItemNum].HighlightSlot(false);
    }

    public void ShowItemDescription(Item item)
    {
        //panel.itemDesGUI.text = item.itemDes;
    }

    public void HideItemDescription()
    {
        //panel.itemDesGUI.text = "";
    }

    /////
    /////
    /////
    // crafting
    /////
    /////
    public void Craft()
    {
        Dictionary<int, List<RecipeItem>> recipes = CraftRecipeIO.instance.craftRecipes;

        //List<RecipeItem> avaliableRecipes = new List<RecipeItem>();

        int resultID = -1;

        bool itemsMatched = true;

        // look for recipes contain this first item in inventory
        for (int j = 0; j <= ItemDatabase.instance.ItemAmout; j++)
        {
            if (!recipes.ContainsKey(j))
                continue;

            //if(items[i].itemID == recipes[])
            if (items[0].itemID == recipes[j][0].ItemID)
            {
                resultID = j;

                Debug.Log("get result id ");
                Debug.Log(resultID);

                break;
                // add to a queue or sth
                // dont break;
            }
        }

        if (resultID == -1)
            return;

        // how many components
        int recipeComponentCount = recipes[resultID].Count;

        // check item matching
        for (int i = 0; i < recipeComponentCount; i++)
        {
            if (items[i].itemID != recipes[resultID][i].ItemID)
            {
                itemsMatched = false;
                break;
            }
        }

        // all matched
        if (itemsMatched)
        {
            // remove all items 
            for (int i = 0; i < recipeComponentCount; i++)
            {
                items[i] = new Item();
            }

            // result
            AddItemByID(resultID);
        }






    }


}