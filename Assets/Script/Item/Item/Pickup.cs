using UnityEngine;
using System.Collections;

public class Pickup : StaticEntity
{
    public string itemName;
    public int itemID = -1;
    public int itemAmount;


    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.tag == "Player")
        {
            if (itemID < 0)
            {
                Debug.Log("item id not assigned");
                return;
            }

            // add item to the Inventory.
            InventoryPanel.instance.inventory.AddItemByID(itemID);

            // ****************
            // add effects here
            // ****************

            string hud = ItemDatabase.instance.GetItemData(itemID).itemDes;

            HudTextManager.instance.CreateFloatText(floatTextSpawnPoint.position, hud, Color.black, 10, 40, 2);

            Destroy(gameObject);
        }

    }
}
