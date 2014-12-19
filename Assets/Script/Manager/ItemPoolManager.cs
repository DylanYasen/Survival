using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
[System.Serializable]
public struct PooledItem
{
    public GameObject model;
    public int itemID;
}
 */

public class ItemPoolManager : MonoBehaviour
{
    public GameObject torchPrefab;

    public static ItemPoolManager instance;

    // seperate pools might be better to loop through
    public List<GameObject> itemModelPool;

    //public List<GameObject> EpuipableItemModelPool { get; private set; }
    //public List<GameObject> MaterialItemModelPool { get; private set; }

    void Awake()
    {
        instance = this;


        itemModelPool = new List<GameObject>();

        // plan : 2
        // Instantiate all pick ups here
        // deactivate
        // when generate items get it from here
        // when items has been picked up / craft /.. return back here



        CreateItems();
    }


    private void CreateItems()
    {
        GameObject g = Instantiate(torchPrefab) as GameObject;
        g.name = torchPrefab.name;
        AddToPool(g);
    }

    public void AddToPool(GameObject g)
    {
        itemModelPool.Add(g);
        g.SetActive(false);
    }

    public void ReturnPool(GameObject gear)
    {
        int index = itemModelPool.IndexOf(gear);
        itemModelPool[index].SetActive(false);
    }

    public GameObject GetItemModel(int id)
    {
        for (int i = 0; i < itemModelPool.Count; i++)
        {
            int modelItemID = ItemDatabase.instance.GetItemIDFromName(itemModelPool[i].name);

            if (modelItemID == id)
            {
                itemModelPool[i].SetActive(true);
                return itemModelPool[i];
            }
        }


        Debug.Log("item model not found in pool");
        return null;
    }





}
