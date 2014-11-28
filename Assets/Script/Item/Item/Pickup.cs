using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour
{
    public string itemName;
    public int itemID = -1;
    public int itemAmount;

    void OnTriggerEnter2D(Collider2D other)
    {
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

            Destroy(gameObject);
        }
    }
}
