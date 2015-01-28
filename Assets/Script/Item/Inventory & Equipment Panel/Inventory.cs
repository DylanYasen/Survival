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

    public bool isHoldingBuilding { get; private set; }

    public Item interactingItem { get; private set; }

    public List<InventorySlot> slots = new List<InventorySlot>();

    public InventoryPanel panel { get; private set; }
    private RectTransform panelRectrans;
    private RectTransform dragItemIconRectrans;

    Dictionary<int, RecipeData> recipes;
    
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


        recipes = CraftRecipeIO.instance.craftRecipes;
        //AddItemByID(0);

        // deactivate inventory gui
        //panel.gameObject.SetActive(false);

        // testing area
        AddItemByID(5);
    }


    void Update()
    {
        // drag item 
        if (isDraggingItem)
        {
            //Vector3 pos = Input.mousePosition;
            //pos.x -= 25;
            //pos.y -= 25;
            //dragItemIconRectrans.position = new Vector2(pos.x, pos.y);
            //dragItemIconRectrans.position = pos;

            dragItemIconRectrans.position = Input.mousePosition;
        }
    }

    // add item by itemID
    public bool AddItemByID(int id)
    {
        // guard
        if (ItemDatabase.instance == null)
        {
            Debug.Log("itemdatabase is null");
            return false;
        }

        return addItemToEmptySlot(LookUpItem(id));
    }

    bool addItemToEmptySlot(Item item)
    {
        bool successfullyAdded = false;

        // loop through itemlist to find empty slot
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemName == null)
            {
                items[i] = item;
                successfullyAdded = true;
                break;
            }
        }
        return successfullyAdded;
    }

    Item LookUpItem(int id)
    {
        // search through database
        for (int i = 0; i < ItemDatabase.instance.items.Count; i++)
        {
            // if find matched item 
            if (ItemDatabase.instance.items[i].itemID == id)
            {
                // get item data
                Item item = ItemDatabase.instance.items[i];

                return item;
            }
        }

        Debug.Log("not found in database");
        return null;
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

    public void SwapItemAtSlotWithNewItem(int slotNum, int newItemID)
    {
        items[slotNum] = LookUpItem(newItemID);
    }

    public void RemoveItemAtSlot(int slot)
    {
        if (slot < 0 || slot > items.Count)
        {
            Debug.Log("remove item at " + slot + " failed. || index out of bounds");
            return;
        }

        items[slot] = new Item();
    }

    public void SelectedItemInteract()
    {
        Debug.Log("pass");

        Debug.Log(selectedItem.isIgniteable);
        // only equipment can be ignit right now
        // so need to


        // 2. get the new item
        // * start work timer
        // 
        if (selectedItem.isIgniteable)
        {
            int igniteToItemID = selectedItem.Ignite();
            Debug.Log("new item ID " + igniteToItemID.ToString());

            interactingItem = LookUpItem(igniteToItemID);
            Debug.Log("interacting item: " + igniteToItemID);

            Player.instance.m_interactController.Work(interactingItem.workTimeNeeded);
        }
    }

    public void DragItem(int slotNum)
    {
        draggedItem = items[slotNum];
        panel.dragItemIcon.enabled = true;
        isDraggingItem = true;
        panel.dragItemIcon.sprite = draggedItem.itemIcon;
        draggedItemSlotNum = slotNum;

        HideItemDescription();
    }

    public void HideDraggedItem()
    {
        isDraggingItem = false;
        panel.dragItemIcon.enabled = false;
        draggedItem = null;
    }

    // * work done
    // 1. unequip old item
    // 3. equip the new item
    // 4. cancel selection
    public void OnFinishInteraction()
    {
        UnEquipItem(selectedItemNum);

        // swap with new item
        items[selectedItemNum] = interactingItem;

        EquipItem(selectedItemNum);

        UnselectItem();
    }

    public void SelectItem(int slotNum)
    {
        hasSelectedItem = true;
        selectedItem = items[slotNum];
        selectedItemNum = slotNum;
        slots[slotNum].HighlightSlot();
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
        List<int> avaliableRecipe = new List<int>(); // all recipes whose first item = first item in inventory
        List<int> componentIDs;

        foreach (KeyValuePair<int, RecipeData> entry in recipes)
        {
            componentIDs = entry.Value.componentsID;
            Debug.Log("Component IDs: " + componentIDs);

            // first item in recipe = first item Player has
            if (items[0].itemID == componentIDs[0])
            {
                Debug.Log("Recipe Found. First Item ID is: " + componentIDs[0]);

                bool matched = true; // default by true. If not matched change to false
                int componentCount = componentIDs.Count;

                Debug.Log("Component Count: " + componentCount);

                // check if match
                // inventory : recipe
                for (int i = 1; i < componentCount; i++)
                    if (items[i].itemID != componentIDs[i])
                    {
                        Debug.Log(i + "th slot not matched.");
                        Debug.Log("Break");

                        matched = false;
                        break;
                    }

                // not matched 
                // move to next
                if (!matched)
                {
                    Debug.Log("Items Not Matched. Continue.");
                    continue;
                }

                // ####### Matched #######
                int resultItemAmount = entry.Value.resultItemAmount;
                int resultItemID = entry.Key;
                Debug.Log("Matched. \n" + " Result Item ID " + resultItemID + "\nResult Item Amount: " + resultItemAmount);

                // remove componnent item in inventory
                for (int i = 0; i < componentCount; i++)
                    items[i] = new Item();

                Debug.Log("All Item Removed");

                // add result item
                for (int i = 0; i < resultItemAmount; i++)
                    AddItemByID(resultItemID);

                Debug.Log("Added " + resultItemAmount + " Item:" + resultItemID);
            }
            else
            {
                Debug.Log("Recipe First Item ID not Matched. Continue.");
                continue;
            }
        }
    }
    //Debug.Log("result item ID: " + entry.Key);

    //RecipeData value = entry.Value;

    //Debug.Log("result item amount: " + value.resultItemAmount);

    //foreach (int i in value.componentsID)
    //    Debug.Log("component item id: " + i);

    //Debug.Log("*************");


    //Dictionary<int, List<RecipeItem>> recipes = CraftRecipeIO.instance.craftRecipes;
    //List<RecipeItem> avaliableRecipes = new List<RecipeItem>();
    /*
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
         
          
    */

    // ******* Redesign ********
    // maybe combine
    public void EquipItem(int slotNum)
    {
        EquipableItem gear = (EquipableItem)items[slotNum];

        //GameObject model = ItemPoolManager.instance.GetItemModel(gear.itemID);

        if (PhotonNetwork.offlineMode)
        {
            // instantiate directely
            //GameObject obj

            Player.instance.m_gearController.Equip((int)gear.equipType, gear.itemName);
        }
        else
        {
            //itemObj = PhotonNetwork.Instantiate("Prefab/Items/" + gear.itemName, Vector3.zero, Quaternion.identity, 0);
            //itemObj.GetComponent<Pickup>().enabled = false;

            //Player.instance.m_controller.GetComponent<PhotonView>().RPC("Equip", PhotonTargets.All, gear.equipType, itemObj.transform);
            Player.instance.m_photonView.RPC("Equip", PhotonTargets.All, (int)gear.equipType, gear.itemName);
        }
    }

    public void UnEquipItem(int slotNum)
    {
        EquipableItem gear = (EquipableItem)items[slotNum];

        if (PhotonNetwork.offlineMode)
        {
            // instantiate directely
            //GameObject obj

            Player.instance.m_gearController.UnEquip((int)gear.equipType);
        }
        else
            Player.instance.m_photonView.RPC("UnEquip", PhotonTargets.All, (int)gear.equipType);

    }

    // TODO: Drop recipe for some items
    // such as building recipe
    public void DropItem(Item item)
    {
        HideDraggedItem();

        // create item on floor
        if (PhotonNetwork.offlineMode)
        {
            Debug.Log("local game item drop");
            // instantiate directly
        }

        else
        {
            Debug.Log(draggedItemSlotNum);
            Debug.Log(item.itemName);
            Debug.Log(Constants.itemPrefabPathPrefix + item.itemName);
            PhotonNetwork.Instantiate(Constants.itemPrefabPathPrefix + item.itemName, Player.instance.m_trans.position, Quaternion.identity, 0);
        }
    }


    // ********* Building Placement Trigger  ********* //
    public void TriggerBuildingPlacement()
    {
        //BuildingManager.instance.HoldModel(ItemPoolManager.instance.GetItemModel(selectedItem.itemID));
    }
}