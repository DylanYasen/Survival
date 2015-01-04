using UnityEngine;
using System.Collections;

public class Pickup : StaticEntity
{
    public string itemName;
    public int itemID = -1;
    public int itemAmount;

    private PhotonView triggerPhotonView;

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        triggerPhotonView = other.GetComponent<PhotonView>();

        if (other.tag == "Player")
        {
            // if I collide with item
            if (triggerPhotonView.isMine || PhotonNetwork.offlineMode)
                if (other.tag == "Player")
                    PickUp();
        }
    }

    private void PickUp()
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

        //HudTextManager.instance.CreateFloatText(floatTextSpawnPoint.position, hud, Color.black, 10, 40, 2);


        // add to item pool right now for testing
        // it should be already in the pool
        ItemPoolManager.instance.AddToPool(gameObject);
        gameObject.SetActive(false);

        // broadcast
        if (!PhotonNetwork.offlineMode)
            m_photonView.RPC("HideItem", PhotonTargets.Others);
    }

    [RPC]
    public void HideItem()
    {
        gameObject.SetActive(false);
    }

}
